using System.Net;

namespace ApiTemplate.Model.Rest
{
    public class ResponseModel<T> where T:class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public HttpStatusCode Code { get; set; }
        public T Data { get; set; }
    }
}
