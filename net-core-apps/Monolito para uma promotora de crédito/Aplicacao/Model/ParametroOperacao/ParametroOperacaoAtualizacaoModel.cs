namespace Aplicacao.Model.ParametroOperacao
{
    public class ParametroOperacaoAtualizacaoModel
    {
        public string InstituicaoFinanceira { get; set; }

        public int IdConvenio { get; set; }

        public int IdTipoOperacao { get; set; }

        public string QuantidadeParcelas { get; set; }

        public string TaxaPlano { get; set; }

        public bool TentativaRetencao { get; set; }
    }
}
