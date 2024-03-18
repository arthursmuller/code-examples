using Dominio.Enum;

namespace Dominio
{
    public class ParametroOperacaoDominio : EntidadeBase
    {
        public string InstituicaoFinanceira { get; private set; }

        public Convenio IdConvenio { get; private set; }

        public ConvenioDominio Convenio { get; private set; }

        public TipoOperacao IdTipoOperacao { get; private set; }

        public TipoOperacaoDominio TipoOperacao { get; set; }

        public string QuantidadeParcelas { get; private set; }

        public string TaxaPlano { get; private set; }

        public bool TentativaRetencao { get; private set; }

        public void SetPropriedadesAtualizadas(
            string instituicaoFinanceira,
            Convenio idConvenio,
            TipoOperacao idTipoOperacao,
            string quantidadeParcelas,
            string taxaPlano,
            bool tentativaRetencao
        )
        {
            InstituicaoFinanceira = instituicaoFinanceira;
            IdConvenio = idConvenio;
            IdTipoOperacao = idTipoOperacao;
            QuantidadeParcelas = quantidadeParcelas;
            TaxaPlano = taxaPlano;
            TentativaRetencao = tentativaRetencao;
        }

        public ParametroOperacaoDominio() { }

        public ParametroOperacaoDominio(
            string instituicaoFinanceira,
            Convenio idConvenio,
            TipoOperacao idTipoOperacao,
            string quantidadeParcelas,
            string taxaPlano,
            bool tentativaRetencao)
        {
            InstituicaoFinanceira = instituicaoFinanceira;
            IdConvenio = idConvenio;
            IdTipoOperacao = idTipoOperacao;
            QuantidadeParcelas = quantidadeParcelas;
            TaxaPlano = taxaPlano;
            TentativaRetencao = tentativaRetencao;
        }
    }
}
