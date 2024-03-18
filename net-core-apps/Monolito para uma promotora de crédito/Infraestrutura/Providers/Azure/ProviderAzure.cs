using B.Arquivo;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Infraestrutura.Providers
{
    [ExcludeFromCodeCoverage]
    public class ProviderAzure : IProviderAzure
    {
        private readonly ConfiguracaoArquivo _configuracaoArquivo;
        private readonly IEnumerable<IProvedorArquivo> _provedorArquivo;

        public ProviderAzure(IEnumerable<IProvedorArquivo> provedorArquivo,
            ConfiguracaoArquivo configuracaoArquivo)
        {
            _provedorArquivo = provedorArquivo;
            _configuracaoArquivo = configuracaoArquivo;
        }

        public async Task<string> SalvarArquivo(byte[] arquivo, string extensao)
        {
            var provedorArquivoAzure = FactoryProvedorArquivo();

            var arquivoAzureSalvo = await provedorArquivoAzure.Salvar(arquivo, extensao, _configuracaoArquivo);
            return HttpUtility.UrlDecode(arquivoAzureSalvo.Url);
        }

        public void ExcluirArquivo(string urlArquivo)
        {
            var provedorArquivoAzure = FactoryProvedorArquivo();

            provedorArquivoAzure.Deletar(urlArquivo, _configuracaoArquivo);
        }

        public async Task<string> ObterBase64Azure(string url)
        {
            var provedorAzure = _provedorArquivo.First(pa => pa.GetType() == typeof(ProvedorArquivoAzure));

            if (!string.IsNullOrEmpty(url))
            {
                var downloadAzure = await provedorAzure.Download(url, _configuracaoArquivo);
                return Convert.ToBase64String(downloadAzure);
            }

            return "";
        }

        private IProvedorArquivo FactoryProvedorArquivo()
        {
            var arquivoAzure = _provedorArquivo.First(pa => pa.GetType() == typeof(ProvedorArquivoAzure));

            return arquivoAzure;
        }
    }
}
