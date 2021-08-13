using System.Collections.Generic;
using System.Net.Http;

namespace ApiTemplate.Model.Rest
{
    public class SendParameterModel
    {
        public string QueryString { get; set; }
        public string BaseAddress { get; set; }
        public bool HasApiKey { get; set; }
        public bool SendApiKeyByHeader { get; set; }
        public string ApiKeyName { get; set; }
        public string ApiKeyValue { get; set; }
        public string ApiName { get; set; }
        public string BearerToken { get; set; }
        public HttpMethod Method { get; set; } = HttpMethod.Post;
    }
}