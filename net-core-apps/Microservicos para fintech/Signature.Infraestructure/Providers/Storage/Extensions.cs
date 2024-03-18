using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StorageProvider;
using System;

namespace Signature.Infraestructure.Providers.Storage
{
    public static class StorageExtensions
    {
        public static IServiceCollection ConfigureStorage(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureStorageVariables(services, configuration);
            services.ConfigureStorageProvider(configuration);
            services.AddScoped<IStorage, StorageProvider>();
            return services;
        }

        private static IServiceCollection ConfigureStorageVariables(IServiceCollection services, IConfiguration configuration)
        {
            var config = new StorageConfiguration
            {
                ContainerNameFinancingProductContractCertificates = GetSetting("STORAGE_CONTAINER_PRODUCT_CONTRACT_CERTIFICATES", configuration),
                ContainerNameUserSignaturePictures = GetSetting("STORAGE_CONTAINER_USER_SIGNATURE_PICTURES", configuration),
                ContainerNameUserSignatureDrawings= GetSetting("STORAGE_CONTAINER_USER_SIGNATURE_DRAWINGS", configuration),
            };

            services.AddSingleton(config);
            return services;
        }

        private static string GetSetting(string setting, IConfiguration configuration) =>
            Environment.GetEnvironmentVariable(setting) ?? configuration[setting];
    }
}

