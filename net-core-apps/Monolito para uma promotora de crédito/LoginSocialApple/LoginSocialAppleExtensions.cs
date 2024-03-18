using Microsoft.Extensions.DependencyInjection;

namespace LoginSocialApple
{
    public static class LoginSocialAppleExtensions
    {
        public static IServiceCollection ConfigurarLoginSocialApple(this IServiceCollection services, B.Configuracao.Configuracao configuracao)
        {
            var configuracaoApple = new ConfiguracaoLoginSocialApple
            {
                chavePrivada = configuracao.BuscarParametro("configuracaologinsocial_apple_chaveprivada"),
                idChave = configuracao.BuscarParametro("configuracaologinsocial_apple_idchave"),
                idTime = configuracao.BuscarParametro("configuracaologinsocial_apple_idtime"),
                idCliente = configuracao.BuscarParametro("configuracaologinsocial_apple_idcliente"),
            };

            services.AddSingleton(configuracaoApple);
            services.AddSingleton<IProvedorApple, ProvedorApple>();

            return services;
        }
    }
}
