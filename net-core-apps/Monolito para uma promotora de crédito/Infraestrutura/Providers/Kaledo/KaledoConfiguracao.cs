using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.Kaledo
{
    [ExcludeFromCodeCoverage]
    public class KaledoConfiguracao
    {
        public string UrlBaseApi { get; set; }
        public string ChaveApi { get; set; }
        public string UrlCriarAutenticarUsuario { get; set; }
    }
}
