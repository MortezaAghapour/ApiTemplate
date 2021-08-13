using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTemplate.Factory.WorkContexts;
using Hangfire.Dashboard;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTemplate.Infrastructure.Filters
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {

            var httpContext = context.GetHttpContext();
           // var cancellationToke = httpContext.RequestAborted;
            var workContext = httpContext.RequestServices.GetService<IWorkContext>();
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            //return httpContext.User.Identity.IsAuthenticated;
            var logged = !(workContext.CurrentUser is null);
            return logged;
        }


    }
}
