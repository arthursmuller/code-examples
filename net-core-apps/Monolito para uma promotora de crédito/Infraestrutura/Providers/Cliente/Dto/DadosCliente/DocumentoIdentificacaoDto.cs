using System;

namespace Infraestrutura.Providers.Cliente.Dto
{
    public class DocumentoIdentificacaoDto
    {
        public string NroDocumentoIdentificacao { get; set; }

        public string UfDocumentoIdentificacao { get; set; }

        public string OrgaoEmissor { get; set; }

        public string DescricaoOrgaoEmissor { get; set; }

        public string CodigoTipoDocumento { get; set; }

        public string DescricaoTipoDocumento { get; set; }

        public DateTime? DataEmissaoDocumentoIdentidade { get; set; }
    }
}
