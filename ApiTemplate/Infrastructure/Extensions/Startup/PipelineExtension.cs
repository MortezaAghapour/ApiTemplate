using ApiTemplate.Infrastructure.Extensions.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace ApiTemplate.Infrastructure.Extensions.Startup
{
    public static class PipelineExtension
    {
        public static  void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            //custom exception handler
            application.UseCustomExceptionHandler();
            application.UseResponseCompression();
            //use static files feature
            application.UseStaticFiles();

            application.UseHsts();
            application.UseCookiePolicy();
            application.UseRouting();
            //authentication
            application.UseAuthentication();
            application.UseAuthorization();
            application.UseCors("CorsPolicy");
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
