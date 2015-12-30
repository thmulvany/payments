using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using RiotGames.Payments.Api.ApiKeyAuthentication.Services;

namespace RiotGames.Payments.Api.ApiKeyAuthentication
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, ApiKeyAuthenticationService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseBasicAuthentication();

            app.Run(async context =>
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Hello ASP.NET 5!");
            });
        }
    }
}
