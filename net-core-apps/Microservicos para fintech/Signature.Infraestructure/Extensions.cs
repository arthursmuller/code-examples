using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Signature.Domain.Services;
using Signature.Infraestructure.Services;
using Signature.Infraestructure.Persistence;
using Signature.Infraestructure.Repositories;
using Signature.Infraestructure.Providers.Storage;
using Signature.Domain.AggregatesModel.SignatureAggregate;
using Signature.Infraestructure.Providers.RabbitMq;
using AddressProvider;
using SignPdfProvider;

namespace Signature.Infraestructure
{
    public static class InfraestructureExtensions
    {
        public static IServiceCollection ConfigureInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SignatureContext>(options =>
            {
                var version = new MariaDbServerVersion(new Version(10, 6, 11));
                options.UseMySql(GetSetting("SIGNATURE_ConnectionString", configuration), version);
            }, ServiceLifetime.Scoped);

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ISignatureRepository, SignatureRepository>();
            services.AddScoped<ISignatureService, SignatureService>();
            services.AddScoped<IIdentityService, IdentityService>();

            services
                .ConfigureRabbitMq(configuration)
                .ConfigureStorage(configuration)
                .ConfigureSignPdfProvider()
                .ConfigureAddressProvider();

            return services;
        }

        private static string GetSetting(string setting, IConfiguration configuration) =>
            Environment.GetEnvironmentVariable(setting) ?? configuration[setting];
    }
}
