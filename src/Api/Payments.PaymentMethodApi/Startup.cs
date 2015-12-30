using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RiotGames.Payments.Api.PaymentMethodApi.Repositories;
using RiotGames.Payments.Api.PaymentMethodApi.Services;
using RiotGames.Payments.Api.ApiKeyAuthentication;
using RiotGames.Payments.Api.ApiKeyAuthentication.Services;

namespace RiotGames.Payments.Api.PaymentMethodApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
//                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFramework()
               .AddNpgsql()
               .AddDbContext<PaymentMethodContext>(
                   options => { options.UseNpgsql(Configuration["Data:ConnectionString"]); });
            services.AddTransient<IPaymentMethodService, PaymentMethodService>();
            services.AddTransient<IPaymentMethodRepo, PaymentMethodRepo>();
            services.AddSingleton<IAuthenticationService, ApiKeyAuthenticationService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseBasicAuthentication();
            app.UseStaticFiles();
            app.UseMvc();
        }

        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
