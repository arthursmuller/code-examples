using Notifications.Infraestructure;
using Notifications.Infraestructure.Providers.RabbitMq;
using CommomStartupExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PaginationHelper.Responses;
using System;

namespace Notifications.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => (Configuration) = (configuration);

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .ConfigureCommomExtensions(
                    Configuration, 
                    new Cors(
                        "NotificationsService", 
                        new string[] {
                            "https://www.capwise.com.br",
                            "http://localhost",
                            $"{getSetting("FRONT_URL")}",
                            $"{getSetting("FRONT_URL")}:3001",
                            $"{getSetting("FRONT_URL")}:3000",
                            $"{getSetting("USER_API_URL")}:3000"})
                )
                .ConfigureInfraestructure(Configuration)
                .AddSwaggerGen(c =>
                 {
                     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notifications.API", Version = "v1" });
                 });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.CreateLogger<Startup>().LogInformation("Started Logging...");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notifications.API v1"));

                IdentityModelEventSource.ShowPII = true;
            }

            app.UseRouting();

            app.UseCors("NotificationsService");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            var lf = builder.ApplicationServices.GetService<ILoggerFactory>();
                            var logger = lf.CreateLogger("myExceptionHandlerLogger");
                            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                            var exception = exceptionHandlerPathFeature.Error;
                            var response = new Response<string>()
                            {
                                Succeeded = false,
                                Errors = new string[] { exception.Message }
                            };
                            var result = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            });
                            context.Response.ContentType = "application/json";

                            logger.LogInformation("Exception Middleware,error:{ex.Message},stackTrace:{ex.StackTrace}", exception.Message, exception.StackTrace);

                            await context.Response.WriteAsync(result);
                        });
                });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            RabbitMqExtensions.ConfigureMessageBroker(app);
        }
        private static string getSetting(string setting) => Environment.GetEnvironmentVariable(setting);
    }
}
