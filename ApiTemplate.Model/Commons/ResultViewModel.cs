using System.Collections.Generic;
using System.Linq;
using ApiTemplate.Common.Enums.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiTemplate.Model.Commons
{
    public class ResultViewModel
    {
        #region Fields
        public CustomStatusCodes StatusCode{ get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }
        #endregion
        #region Constructors

        public ResultViewModel(bool isSuccess,CustomStatusCodes statusCode, string message=null,List<string> errors=null )
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Errors = errors;
            Message = message;
        }
        #endregion
   


        #region Implicit Opertors
        public static implicit operator ResultViewModel(OkResult result)
        {
            return new ResultViewModel(true, CustomStatusCodes.Success);
        }

        public static implicit operator ResultViewModel(BadRequestResult result)
        {
            return new ResultViewModel(false, CustomStatusCodes.BadRequest);
        }
        public static implicit operator ResultViewModel(BadRequestObjectResult result)
        {
            var allErrors = result.Value;
            if (!(result.Value is SerializableError errors))
            {
                return new ResultViewModel(false, CustomStatusCodes.BadRequest, errors: allErrors as List<string>);
            }
            var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct().ToList();
           
            return new ResultViewModel(false, CustomStatusCodes.BadRequest, errors:errorMessages);
        }
        public static implicit operator ResultViewModel(ContentResult result)
        {
            return new ResultViewModel(true, CustomStatusCodes.Success, result.Content);
        }
        public static implicit operator ResultViewModel(NotFoundResult result)
        {
            return new ResultViewModel(false, CustomStatusCodes.NotFound);
        }
        #endregion
    }
    public class ResultViewModel<T> :ResultViewModel where T:class
    {
        #region Fields
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
        #endregion
        #region Constructors
                public ResultViewModel(bool isSuccess, CustomStatusCodes statusCode,T data, string message = null, List<string> errors = null) : base(isSuccess, statusCode, message, errors)
        {
        }
        #endregion

        #region Implicit Operators
        public static implicit operator ResultViewModel<T>(T data)
        {
            return new ResultViewModel<T>(true, CustomStatusCodes.Success, data);
        }

        public static implicit operator ResultViewModel<T>(OkResult result)
        {
            return new ResultViewModel<T>(true, CustomStatusCodes.Success,data:null);
        }

        public static implicit operator ResultViewModel<T>(OkObjectResult result)
        {
            return new ResultViewModel<T>(true, CustomStatusCodes.Success, (T)result.Value);
        }

        public static implicit operator ResultViewModel<T>(BadRequestResult result)
        {
            return new ResultViewModel<T>(false, CustomStatusCodes.BadRequest, null);
        }

        public static implicit operator ResultViewModel<T>(BadRequestObjectResult result)
        {
            var allErrors = result.Value as List<string>;
            if (result.Value is SerializableError errors)
            {
                allErrors = errors.SelectMany(p => (string[])p.Value).Distinct().ToList();
            }
            return new ResultViewModel<T>(false, CustomStatusCodes.BadRequest,data:null, errors: allErrors);
        }
        public static implicit operator ResultViewModel<T>(ContentResult result)
        {
            return new ResultViewModel<T>(true, CustomStatusCodes.Success,data:null,message: result.Content);
        }
        public static implicit operator ResultViewModel<T>(NotFoundObjectResult result)
        {
            return new ResultViewModel<T>(false, CustomStatusCodes.NotFound, (T)result.Value);
        }
        #endregion


    }
}
