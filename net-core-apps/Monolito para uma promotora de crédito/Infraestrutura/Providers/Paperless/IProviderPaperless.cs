using Infraestrutura.Providers.Paperless.Dto;
using Infraestrutura.Providers.Paperless.Dto.AssinaturaDocumento;
using Infraestrutura.Providers.Paperless.Dto.ReenvioToken;
using System.Threading.Tasks;

namespace Infraestrutura.Providers.Paperless
{
    public interface IProviderPaperless
    {
        Task<int?> EnviarDocumentoParaAssinatura(InformacoesEnvioDocumentoParaAssinaturaDto parametros, string tokenAutenticacao);

        Task<byte[]> AssinarDocumento(AssinaturaDocumentoDto parametros, string tokenAutenticacao);

        Task<bool> ReenviarToken(ReenvioTokenParametrosDto parametros, string tokenAutenticacao);
    }
}
