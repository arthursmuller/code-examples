using System.Diagnostics.CodeAnalysis;

namespace Aplicacao
{
    [ExcludeFromCodeCoverage]
    public class LeadCriacaoModel
    {
        public string CPF { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public int? IdConvenio { get; set; }

        public int? IdProduto { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string OrigemRequisicaoPalavraChave { get; set; }

        public string OrigemRequisicaoMidia { get; set; }

        public string OrigemRequisicaoConteudo { get; set; }

        public string OrigemRequisicaoTermo { get; set; }

        public string OrigemRequisicaoCampanha { get; set; }

        public int? IdLoja { get; set; }

        public bool DesejaContatoWhatsApp { get; set; }

        public bool Quitacao { get; set; }
    }
}