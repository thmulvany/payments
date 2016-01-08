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
//              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true); // TODO: setup for use in diff environments
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        /// <summary>
        /// DI registration
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // register mvc 6
            services.AddMvc();

            // setup ef7 to use the ef7 PostgreSQL provider (NpgSQL)
            services.AddEntityFramework()
               .AddNpgsql()
               .AddDbContext<PaymentMethodContext>(
                   options => { options.UseNpgsql(Configuration["Data:ConnectionString"]); });

            // domain services + repo + auth
            services.AddTransient<IPaymentMethodService, PaymentMethodService>();
            services.AddTransient<IPaymentMethodRepo, PaymentMethodRepo>();
            services.AddSingleton<IAuthenticationService, ApiKeyAuthenticationService>();
        }

        /// <summary>
        /// Add middlewares
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApiKeyAuthentication();
            app.UseStaticFiles();
            app.UseMvc();
        }

        /// <summary>
        /// Main entry point to web app
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
