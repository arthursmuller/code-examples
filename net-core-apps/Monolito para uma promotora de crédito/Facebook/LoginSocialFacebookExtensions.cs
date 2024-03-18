using Microsoft.Extensions.DependencyInjection;

namespace LoginSocialFacebook
{
    public static class LoginSocialFacebookExtensions
    {
        public static IServiceCollection ConfigurarLoginSocialFacebook(this IServiceCollection services, B.Configuracao.Configuracao configuracao)
        {
            var configuracaoFacetec = new ConfiguracaoLoginSocialFacebook
            {
                UrlApi = configuracao.BuscarParametro("configuracaologinsocial_facebook_urlapi"),
                TokenAplicativo = configuracao.BuscarParametro("configuracaologinsocial_facebook_tokenaplicativo")
            };

            services.AddSingleton(configuracaoFacetec);
            services.AddSingleton<IProvedorFacebook, ProvedorFacebook>();

            return services;
        }
    }
}
