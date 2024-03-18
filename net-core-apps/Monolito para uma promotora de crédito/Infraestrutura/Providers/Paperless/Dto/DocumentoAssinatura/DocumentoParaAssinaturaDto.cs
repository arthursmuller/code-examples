using Infraestrutura.Providers.Paperless.Enum;

namespace Infraestrutura.Providers.Paperless.Dto
{
    public class DocumentoParaAssinaturaDto
    {
        public MetodoCaptura MetodoDeCaptura { get; set; }
        public string IdentificacaoDocumento { get; set; }
        public string NomeDoDocumento { get; set; }
        public string ExtensaoDoDocumento { get; set; }
        public string SiglaTipoDocumento { get; set; }
    }
}
