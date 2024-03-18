using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.IcatuApi
{
    [ExcludeFromCodeCoverage]
    public static class IcatuApiExtensions
    {
        public static IServiceCollection ConfigurarIcatu(this IServiceCollection services, B.Configuracao.Configuracao configuracao)
        {
            var configUnico = new IcatuConfiguracao
            {
                BaseUrl = configuracao.BuscarParametro("configuracao_icatu_baseurl"),
                CodigoEmpresa = configuracao.BuscarParametro("configuracao_icatu_codigoempresa"),
                OcpApimSubscriptionKey = configuracao.BuscarParametro("configuracao_icatu_ocapimsubscription"),
                Empresa = configuracao.BuscarParametro("configuracao_icatu_empresa"),
                LinhaNegocio = configuracao.BuscarParametro("configuracao_icatu_linhanegocio")
            };

            services.AddSingleton(configUnico);
            services.AddSingleton<IProviderIcatu, ProviderIcatu>();

            return services;
        }
    }
}
