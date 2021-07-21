using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ApiTemplate.Common.Markers.DependencyRegistrar;
using ApiTemplate.Common.Resources;
using ApiTemplate.Core.DataTransforObjects.Rest;
using ApiTemplate.Service.Logs;
using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;
using Polly.Retry;

namespace ApiTemplate.Service.Rests
{
    public class RestService : IRestService, IScopedDependency
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
        private readonly AsyncFallbackPolicy<HttpResponseMessage> _fallbackPolicy;
        private readonly ILogService _logService;

        #endregion
        #region Constructors
        public RestService(HttpClient httpClient, IHttpClientFactory httpClientFactory, ILogService logService)
        {
            _httpClientFactory = httpClientFactory;
            _logService = logService;
            _retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(result => !result.IsSuccessStatusCode)
                .WaitAndRetryAsync(25, duration => TimeSpan.FromSeconds(20), (d, c) =>
                   {
                       //Retry
                       //call log service
                       //logService.Log();
                   });


            _fallbackPolicy = Policy.HandleResult<HttpResponseMessage>(result => !result.IsSuccessStatusCode)
                .Or<BrokenCircuitException>()
                .FallbackAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent(typeof(ResponseModel<string>), new ResponseModel<string>
                    {
                        Message = Resource.FailureCallApi,
                        Code = HttpStatusCode.BadGateway,
                        Data = string.Empty
                    }, new JsonMediaTypeFormatter())
                });
        }
        #endregion
        #region Methods
        public async Task<ResponseModel<TResult>> Call<TResult, TParam>(CallParameterModel<TParam> parameter) where TResult : class
        {
            var httpClient = _httpClientFactory.CreateClient();
            StringContent content = null;
            var baseAddress = new Uri(parameter.BaseAddress);
            httpClient.BaseAddress = baseAddress;
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.Timeout = new TimeSpan(0, 10, 0);
            if (parameter.HasApiKey)
            {
                httpClient.DefaultRequestHeaders.Add(parameter.ApiKeyName, parameter.ApiKeyValue);
            }
            if (!string.IsNullOrEmpty(parameter.BearerToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer {parameter.BearerToken}");
            }
            if (!(parameter.Content is null))
            {
                var serializedData = JsonConvert.SerializeObject(parameter.Content);
                content = new StringContent(serializedData, Encoding.UTF8, "application/json");
            }


            //using polly

            var result = await _fallbackPolicy.ExecuteAsync(() => _retryPolicy.ExecuteAsync(() =>
                CircuitBreakerPolicy().ExecuteAsync(() => httpClient.PostAsync(parameter.ApiName, content))));

            if (result.IsSuccessStatusCode)
            {
                var readDataAsString = await result.Content.ReadAsStringAsync();
                var deserializeData = JsonConvert.DeserializeObject<TResult>(readDataAsString);
                return new ResponseModel<TResult>
                {
                    Message = result.ReasonPhrase,
                    Code = result.StatusCode,
                    Data = deserializeData
                };
            }
            else
            {
                return new ResponseModel<TResult>
                {
                    Code = result.StatusCode,
                    Message = result.ReasonPhrase,
                    Data = null
                };
            }
        }


        private AsyncCircuitBreakerPolicy<HttpResponseMessage> CircuitBreakerPolicy()
        {
            return Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode).Or<HttpRequestException>()
                .CircuitBreakerAsync(/*تعداد دفعات برخورد خطا*/15, /**تایمی که اجازه قبول درخواست جدید را نمی دهد**/TimeSpan.FromSeconds(10),
                    (d, c) =>
                    {
                        //Break
                        //وقتی درخواستی رد میشود این متد اجرا میشود
                    },
                    () =>
                    {
                        // Reset
                        // اگر بعد از مدت زمان دلخواه درخواست فرستاده شد و جواب اوکی گرفته شد این متد صدا زده می شود
                    },
                    () =>
                    {
                        //Half
                        // بعد از مدت زمان مورد اگر درخواستی بیاد اول این متد صدا زده می شود
                    });
        }
        #endregion

    }
}
