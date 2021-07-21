﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTemplate.Core.DataTransforObjects.Rest
{
    public class CallParameterModel<T> 
    {
        public T Content { get; set; }
        public string BaseAddress { get; set; }
        public bool HasApiKey { get; set; }
        public string ApiKeyName { get; set; }
        public string ApiKeyValue { get; set; }
        public string ApiName { get; set; }
        public string BearerToken { get; set; }
    }
}
