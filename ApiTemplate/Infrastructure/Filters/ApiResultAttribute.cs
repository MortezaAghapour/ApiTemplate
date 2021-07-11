using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTemplate.Common.Enums.Exceptions;
using ApiTemplate.Model.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiTemplate.Infrastructure.Filters
{
    public class ApiResultAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            switch (context.Result)
            {
                case OkObjectResult okObjectResult:
                    {
                        var apiResult = new ResultViewModel<object>(true, CustomStatusCodes.Success, okObjectResult.Value);
                        context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
                        break;
                    }
            }

            base.OnResultExecuting(context);
        }
    }
}
