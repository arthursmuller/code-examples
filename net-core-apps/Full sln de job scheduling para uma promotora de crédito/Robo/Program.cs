using Aplicacao;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Robo.Servicos;
using System;

namespace Robo
{
    class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
          
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (environment == "Development")
            {
                using (var scope = host.Services.CreateScope())
                {
                    var inicializadorDb = scope.ServiceProvider.GetRequiredService<InicializadorDb>();
                    inicializadorDb.Migrate();
                    inicializadorDb.SeedData();
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.ConfigurarServico();
                    services.AddHostedService<RoboServico>();
                });
    }
}
