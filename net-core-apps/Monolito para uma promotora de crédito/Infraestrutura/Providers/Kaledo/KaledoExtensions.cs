using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.Kaledo
{
    [ExcludeFromCodeCoverage]
    public static class KaledoExtensions
    {
        public static IServiceCollection ConfigurarKaledo(this IServiceCollection services, B.Configuracao.Configuracao configuracao)
        {
            var configKaledo = new KaledoConfiguracao
            {
                UrlBaseApi = configuracao.BuscarParametro("configuracao_kaledo_base_url"),
                ChaveApi = configuracao.BuscarParametro("configuracao_kaledo_apikey"),
                UrlCriarAutenticarUsuario = configuracao.BuscarParametro("configuracao_kaledo_criarautenticarusuario")
            };

            services.AddSingleton(configKaledo);
            services.AddSingleton<IProviderKaledo, ProviderKaledo>();

            return services;
        }
    }
}
