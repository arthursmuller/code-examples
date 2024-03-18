using System;
using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Model.DocumentoIdentificacaoCliente
{
    public class DocumentoIdentificacaoClienteModel
    {
        [Required]
        public int IdTipoDocumento { get; set; }

        public int IdOrgaoEmissor { get; set; }

        public int IdUnidadeFederativa { get; set; }

        public string Numero { get; set; }

        public DateTime DataEmissao { get; set; }
    }
}
