using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Common.Enums.Exceptions;
using ApiTemplate.Common.Exceptions;
using ApiTemplate.Model.Commons;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ApiTemplate.Infrastructure.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;
      

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
       
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cancellationToken = context.RequestAborted;
            try
            {
                await _next(context);
            }
            catch (NotFoundException exception)
            {
                var errors = new List<string>
                {
                    "آبجکت مورد نظر شما یافت نشد"
                };
                var result = new ResultModel<EmptyModel>(isSuccess: false, statusCode: CustomStatusCodes.NotFound,
                    errors: errors, data: default);
              //{
              //    IsSuccess = false,
              //    Errors = new List<string>
              //    {
              //        "آبجکت مورد نظر شما یافت نشد"
              //    }
              //};
              var json = JsonConvert.SerializeObject(result);
                //log error in database 
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);
            }
            catch (BadRequestException exception)
            {
                var errors = new List<string>
                {
                    "خطایی در پارامترهای ارسالی وجود دارد"
                };
                var result = new ResultModel<EmptyModel>(isSuccess: false, statusCode: CustomStatusCodes.BadRequest,
                    errors: errors, data: default);
                //var result = new ReturnModel<EmptyModel>
                //{
                //    IsSuccess = false,
                //    Errors = new List<string>
                //    {
                //        "خطایی در پارامترهای ارسالی وجود دارد"
                //    }
                //};
                var json = JsonConvert.SerializeObject(result);
                //log error in database 
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);
      
            }
            catch (AppException exception)
            {

                var errors = new List<string>
                {
                    "خطایی درپردازش رخ داده است"
                };
                var result = new ResultModel<EmptyModel>(isSuccess: false, statusCode: CustomStatusCodes.ServerError,
                    errors: errors, data: default);
                //var result = new ReturnModel<EmptyModel>
                //{
                //    IsSuccess = false,
                //    Errors = new List<string>
                //    {
                //        "خطایی درپردازش رخ داده است"
                //    }
                //};
                var json = JsonConvert.SerializeObject(result);
                //log error in database 
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);
            }
            catch (Exception exception)
            {
                var errors = new List<string>
                {
                    "خطایی نامعلوم وجود دارد"
                };
                var result = new ResultModel<EmptyModel>(isSuccess: false, statusCode: CustomStatusCodes.UnKnown,
                    errors: errors, data: default);
                //var result = new ReturnModel<EmptyModel>
                //{
                //    IsSuccess = false,
                //    Errors = new List<string>
                //    {
                //        "خطایی نامعلوم وجود دارد"
                //    }
                //};
                var json = JsonConvert.SerializeObject(result);
                //log error in database 
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);
            }

        }
    }
}
