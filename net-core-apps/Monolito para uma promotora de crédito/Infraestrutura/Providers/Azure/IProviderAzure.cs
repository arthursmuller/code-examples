using System.Threading.Tasks;

namespace Infraestrutura.Providers
{
    public interface IProviderAzure
    {
        Task<string> SalvarArquivo(byte[] arquivo, string extensao);
        void ExcluirArquivo(string urlArquivo);
        Task<string> ObterBase64Azure(string url);
    }
}
