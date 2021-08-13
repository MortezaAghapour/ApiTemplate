using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTemplate.Model.Commons
{

    public class ReturnModel<T> where T:class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }

}
