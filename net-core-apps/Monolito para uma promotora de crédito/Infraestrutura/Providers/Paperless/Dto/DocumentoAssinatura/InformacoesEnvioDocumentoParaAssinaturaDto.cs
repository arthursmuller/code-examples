using Infraestrutura.Providers.Paperless.Enum;
using System.Collections.Generic;

namespace Infraestrutura.Providers.Paperless.Dto
{
    public class InformacoesEnvioDocumentoParaAssinaturaDto
    {
        public Certificadora CodigoDaCertificadora { get; set; }
        public DocumentoParaAssinaturaDto DadosDoDocumento { get; set; }
        public IEnumerable<SignatarioDocumentoDto> Signatarios { get; set; }
    }
}
