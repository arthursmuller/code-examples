using Aplicacao.Model.Convenio;
using Aplicacao.Model.TipoOperacao;

namespace Aplicacao.Model.ParametroOperacao
{
    public class ParametroOperacaoModel
    {
        public int Id { get; set; }

        public string InstituicaoFinanceira { get; set; }

        public ConvenioModel Convenio { get; set; }

        public TipoOperacaoModel TipoOperacao { get; set; }

        public string QuantidadeParcelas { get; set; }

        public string TaxaPlano { get; set; }

        public bool TentativaRetencao { get; set; }
    }
}
