using B.Arquivo;

namespace Infraestrutura.Providers
{
    public class ConfiguracaoArquivo : IConfiguracaoArquivo
    {
        public string CaminhoPasta { get; set; }
        public string ContainerName { get; set; }
        public string ConnectionString { get; set; }
        public string Url { get; set; }
        public string Servidor { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}
