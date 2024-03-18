using BacenProvider;
using CommomStartupExtensions;
using ExternalEntities.API.Application.Features.Queries;
using ExternalEntities.Infraestructure;
using ExternalEntities.Infraestructure.Providers.RabbitMq;
using MediatR;
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

namespace ExternalEntities.API
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
                        "ExternalEntitiesService", 
                        new string[] {
                            "https://www.capwise.com.br",
                            "http://localhost",
                            $"{getSetting("FRONT_URL")}",
                            $"{getSetting("FRONT_URL")}:3001",
                            $"{getSetting("FRONT_URL")}:3000",
                            $"{getSetting("USER_API_URL")}:3000"})
                )
                .ConfigureBacenProvider()
                .ConfigureInfraestructure(Configuration)
                .AddMediatR(typeof(GetUserScoreQuery).Assembly)
                .AddSwaggerGen(c =>
                 {
                     c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExternalEntities.API", Version = "v1" });
                 });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.CreateLogger<Startup>().LogInformation("Started Logging...");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExternalEntities.API v1"));

                IdentityModelEventSource.ShowPII = true;
            }

            app.UseRouting();

            app.UseCors("ExternalEntitiesService");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            RabbitMqExtensions.ConfigureMessageBroker(app);
        }
        private static string getSetting(string setting) => Environment.GetEnvironmentVariable(setting);
    }
}
