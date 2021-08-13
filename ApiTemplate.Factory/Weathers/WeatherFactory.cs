using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Common.Enums.Exceptions;
using ApiTemplate.Common.Enums.RabbitMq;
using ApiTemplate.Common.Exceptions;
using ApiTemplate.Common.Markers.DependencyRegistrar;
using ApiTemplate.Core.Configurations.Weathers;
using ApiTemplate.Core.Entities.Weathers;
using ApiTemplate.Model.Rest;
using ApiTemplate.Model.Weathers;
using ApiTemplate.Service.RabbitMq;
using ApiTemplate.Service.Rests;
using ApiTemplate.Service.Weathers;
using Hangfire;
using Newtonsoft.Json;

namespace ApiTemplate.Factory.Weathers
{
    public class WeatherFactory : IWeatherFactory, IScopedDependency
    {
        #region Fields

        private readonly IWeatherService _weatherService;
        private readonly IRestService _restService;
        private readonly WeatherConfiguration _weatherConfiguration;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        #endregion

        #region Constructors
        public WeatherFactory(IWeatherService weatherService, IRestService restService, WeatherConfiguration weatherConfiguration, IRabbitMqService rabbitMqService, IBackgroundJobClient backgroundJobClient)
        {
            _weatherService = weatherService;
            _restService = restService;
            _weatherConfiguration = weatherConfiguration;
            _rabbitMqService = rabbitMqService;
            _backgroundJobClient = backgroundJobClient;
        }
        #endregion

        #region Methods
        public async Task GetCurrentWeatherInfoByCityName(CurrentWeatherByCityRequestModel model,CancellationToken cancellationToken=default)
        {
            if (_weatherConfiguration is null)
            {
                throw new NullArgumentException($"پارامتر {nameof(_weatherConfiguration)} در کلاس {GetType().Name}  و متد {MethodBase.GetCurrentMethod().Name}  خالی می باشد");
            }

            /**
             *   اطلاعات شهر مورد نظر گرفته می شود
             *   بعد اطلاعات آب و هوایی آن از طریق سرویس واکشی می شود
             * سرویس یکی از سایت هایی که اطلاعات آب و هوایی رو ارائه می کنند
             * بعد از گزفتن اطلاعات بعد یه مدت زمان مشخص اطلاعات آب و هوایی توسط رببیت فرستاده میشه
             * اگر درجه هوا بیشتر از 14 باشد نام شهر و درجه در دیتابیس ذخیره می شود
             */
            var queryString = $"key={_weatherConfiguration.Key}&q={model.City}";
            var parameter=new SendParameterModel
            {
                ApiName = _weatherConfiguration.CurrentWeatherMethodName,
                BaseAddress = _weatherConfiguration.BaseUrl,
                SendApiKeyByHeader = false,
                HasApiKey = false ,
                 QueryString = queryString
            };
            var currentWeather =await 
                _restService.CallSend<CurrentWeatherResponseModel>(parameter,cancellationToken);
            if (currentWeather.IsSuccess)
            {
                _backgroundJobClient.Schedule(
                    () => _rabbitMqService.SendMessage(currentWeather.Data, ExchangeTypes.Fanout),
                    TimeSpan.FromMinutes(_weatherConfiguration.SendWeatherInfoAfterMinute));
                
                 //if temp > 14 insert to database
                if (currentWeather.Data.Current.TempC>14)
                {
                   await _weatherService.AddWeather(new Weather
                    {
                        City = currentWeather.Data.Location.Name,
                        CreateDate = DateTime.Now,
                        Heat = currentWeather.Data.Current.TempC
                    },cancellationToken);
                }
            }
            else
            {
                throw new AppException(JsonConvert.SerializeObject(currentWeather));
            }
        }

        public async Task GetCurrentWeatherInfoByLatLonName(CurrentWeatherByLatLonRequestModel model,
            CancellationToken cancellationToken = default)
        {
            if (_weatherConfiguration is null)
            {
                throw new NullArgumentException($"پارامتر {nameof(_weatherConfiguration)} در کلاس {GetType().Name}  و متد {MethodBase.GetCurrentMethod().Name}  خالی می باشد");
            }

            /**
             *   اطلاعات شهر مورد نظر گرفته می شود
             *   بعد اطلاعات آب و هوایی آن از طریق سرویس واکشی می شود
             * سرویس یکی از سایت هایی که اطلاعات آب و هوایی رو ارائه می کنند
             * بعد از گزفتن اطلاعات بعد یه مدت زمان مشخص اطلاعات آب و هوایی توسط رببیت فرستاده میشه
             * اگر درجه هوا بیشتر از 14 باشد نام شهر و درجه در دیتابیس ذخیره می شود
             */
            var queryString = $"key={_weatherConfiguration.Key}&q={model.Lat},{model.Lon}";
            var parameter = new SendParameterModel
            {
                ApiName = _weatherConfiguration.CurrentWeatherMethodName,
                BaseAddress = _weatherConfiguration.BaseUrl,
                SendApiKeyByHeader = false,
                HasApiKey = false,
                QueryString = queryString
            };
            var currentWeather = await
                _restService.CallSend<CurrentWeatherResponseModel>(parameter, cancellationToken);
            if (currentWeather.IsSuccess)
            {
                _backgroundJobClient.Schedule(
                    () => _rabbitMqService.SendMessage(currentWeather.Data, ExchangeTypes.Fanout),
                    TimeSpan.FromMinutes(_weatherConfiguration.SendWeatherInfoAfterMinute));

                //if temp > 14 insert to database
                if (currentWeather.Data.Current.TempC > 14)
                {
                    await _weatherService.AddWeather(new Weather
                    {
                        City = currentWeather.Data.Location.Name,
                        CreateDate = DateTime.Now,
                        Heat = currentWeather.Data.Current.TempC
                    }, cancellationToken);
                }
            }
            else
            {
                throw new AppException(JsonConvert.SerializeObject(currentWeather));
            }
        }

        #endregion


    }
}