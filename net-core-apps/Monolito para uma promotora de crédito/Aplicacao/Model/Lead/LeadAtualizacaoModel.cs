using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao
{
    [ExcludeFromCodeCoverage]
    public class LeadAtualizacaoModel
    {
        public string CPF { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public int? IdProduto { get; set; }

        public int? IdConvenio { get; set; }

        [Required]
        public bool DesejaContatoWhatsApp { get; set; }

        public int? IdLoja { get; set; }
    }
}