using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabitMQTask.Common.Exceptions;
using RabitMQTask.Model.Commons;

namespace RabitMQTask.Middlewares
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
        
              var result = new ResultViewModel<EmptyViewModel>
              {
                  IsSuccess = false,
                  Errors = new List<string>
                  {
                      "آبجکت مورد نظر شما یافت نشد"
                  }
              };
              var json = JsonConvert.SerializeObject(result);
                //log error in database 
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);
            }
            catch (BadRequestException exception)
            {
       
                var result = new ResultViewModel<EmptyViewModel>
                {
                    IsSuccess = false,
                    Errors = new List<string>
                    {
                        "خطایی در پارامترهای ارسالی وجود دارد"
                    }
                };
                var json = JsonConvert.SerializeObject(result);
                //log error in database 
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);
      
            }
            catch (AppException exception)
            {
                
                var result = new ResultViewModel<EmptyViewModel>
                {
                    IsSuccess = false,
                    Errors = new List<string>
                    {
                        "خطایی درپردازش رخ داده است"
                    }
                };
                var json = JsonConvert.SerializeObject(result);
                //log error in database 
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);
            }
            catch (Exception exception)
            {
                var result = new ResultViewModel<EmptyViewModel>
                {
                    IsSuccess = false,
                    Errors = new List<string>
                    {
                        "خطایی نامعلوم وجود دارد"
                    }
                };
                var json = JsonConvert.SerializeObject(result);
                //log error in database 
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);
            }

        }
    }
}
