using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.Unico
{
    [ExcludeFromCodeCoverage]
    public static class UnicoExtensions
    {
        public static IServiceCollection ConfigurarUnico(this IServiceCollection services, B.Configuracao.Configuracao configuracao)
        {
            var configUnico = new UnicoConfiguracao
            {
                AuthUrlBaseApi = configuracao.BuscarParametro("configuracaobiometria_unico_authurl"),
                AuthUrlGerarToken = configuracao.BuscarParametro("configuracaobiometria_unico_authurlgerartoken"),
                ChaveApi = configuracao.BuscarParametro("configuracaobiometria_unico_apikey"),
                ChavePrivada = configuracao.BuscarParametro("configuracaobiometria_unico_privatekey"),
                ServiceAccount = configuracao.BuscarParametro("configuracaobiometria_unico_serviceaccount"),
                Tenant = configuracao.BuscarParametro("configuracaobiometria_unico_tenantcode"),
                TokenAcessoValidadeEmSegundos = Int32.Parse(configuracao.BuscarParametro("configuracaobiometria_unico_tempotokenemsegundos")),
                UrlBaseApi = configuracao.BuscarParametro("configuracaobiometria_unico_baseurl"),
                UrlProcessos = configuracao.BuscarParametro("configuracaobiometria_unico_processrote"),
            };

            services.AddSingleton(configUnico);
            services.AddScoped<IUnicoProvider, UnicoProvider>();

            return services;
        }
    }
}
