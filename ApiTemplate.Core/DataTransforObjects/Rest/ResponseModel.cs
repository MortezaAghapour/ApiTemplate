using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ApiTemplate.Core.DataTransforObjects.Rest
{
    public class ResponseModel<T> where T:class
    {
        public string Message { get; set; }
        public HttpStatusCode Code { get; set; }
        public T Data { get; set; }
    }
}
