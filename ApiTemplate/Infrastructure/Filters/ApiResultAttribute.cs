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
                        var apiResult = new ResultModel<object>(true, CustomStatusCodes.Success, okObjectResult.Value);
                        context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
                        break;
                    }
                case OkResult okResult:
                    {
                        var apiResult = new ResultModel(true, CustomStatusCodes.Success);
                        context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };
                        break;
                    }
                case BadRequestObjectResult badRequestObjectResult:
                    {
                        var message = badRequestObjectResult.Value.ToString();
                        if (badRequestObjectResult.Value is SerializableError errors)
                        {
                            var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                            message = string.Join(" | ", errorMessages);
                        }
                        var apiResult = new ResultModel(false, CustomStatusCodes.BadRequest, message);
                        context.Result = new JsonResult(apiResult) { StatusCode = badRequestObjectResult.StatusCode };
                        break;
                    }
                case ContentResult contentResult:
                    {
                        var apiResult = new ResultModel(true, CustomStatusCodes.Success, contentResult.Content);
                        context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode };
                        break;
                    }
                case NotFoundResult notFoundResult:
                    {
                        var apiResult = new ResultModel(false, CustomStatusCodes.NotFound);
                        context.Result = new JsonResult(apiResult) { StatusCode = notFoundResult.StatusCode };
                        break;
                    }
                case NotFoundObjectResult notFoundObjectResult:
                    {
                        var apiResult = new ResultModel<object>(false, CustomStatusCodes.NotFound, notFoundObjectResult.Value);
                        context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode };
                        break;
                    }
                case ObjectResult objectResult when objectResult.StatusCode == null && !(objectResult.Value is ResultModel):
                    {
                        var apiResult = new ResultModel<object>(true, CustomStatusCodes.NotFound, objectResult.Value);
                        context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode };
                        break;
                    }
            }

            base.OnResultExecuting(context);
        }
    }
}
