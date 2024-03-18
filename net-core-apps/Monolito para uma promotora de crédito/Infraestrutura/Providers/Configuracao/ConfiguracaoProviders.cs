namespace Infraestrutura.Providers.Dto
{
    public class ConfiguracaoProviders
    {
        public string AuthApi { get; set; }

        public string ConsignadoApi { get; set; }

        public string ClienteApi { get; set; }

        public string BemApi { get; set; }
        public string IcatuApi { get; set; }

        public ConfiguracaoProviderPaperless Paperless { get; set; }

        public string ConsignadoUsuario { get; set; }

        public string ConsignadoSenha { get; set; }
    }
}
