using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExternalEntities.Domain.AggregatesModel.BusinessAggregate;
using ExternalEntities.Domain.AggregatesModel.UserAggregate;
using ExternalEntities.Domain.Services;
using ExternalEntities.Infraestructure.Persistence;
using ExternalEntities.Infraestructure.Providers.RabbitMq;
using ExternalEntities.Infraestructure.Repositories;
using ExternalEntities.Infraestructure.Services;
using QuodProvider;
using AuthenticationProvider;
using System.Linq;

namespace ExternalEntities.Infraestructure
{
    public static class InfraestructureExtensions
    {
        public static IServiceCollection ConfigureInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure(configuration);
            services.AddDbContext<ExternalEntitiesContext>(options =>
            {
                var version = new MariaDbServerVersion(new Version(10, 6, 11));
                options.UseMySql(GetSetting("EXTERNALENTITIES_ConnectionString", configuration), version);
            }, ServiceLifetime.Scoped);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBusinessService, BusinessService>();

            services
                .ConfigureQuodProvider()
                .ConfigureAuthenticationProvider()
                .ConfigureRabbitMq(configuration);

            return services;
        }

        public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
        {
            var isProd = GetSetting("ASPNETCORE_ENVIRONMENT", configuration) == "Production";
            bool.TryParse(GetSetting("USE_SCR", configuration), out var useScr);
            var cpfTests = GetSetting("TEST_CPFS", configuration);

            services.AddSingleton(new Configuration(isProd, isProd && useScr, cpfTests?.Split(";")?.ToList()));
            
            Console.WriteLine(isProd && useScr);

            return services;
        }

        private static string GetSetting(string setting, IConfiguration configuration) =>
            Environment.GetEnvironmentVariable(setting) ?? configuration[setting];
    }
}
