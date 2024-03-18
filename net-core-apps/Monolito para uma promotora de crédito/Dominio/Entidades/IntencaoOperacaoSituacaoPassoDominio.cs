using Dominio.Enum;

namespace Dominio
{
    public class IntencaoOperacaoSituacaoPassoDominio : EntidadeBase
    {
        public Produto IdProduto { get; private set; }
        public TipoOperacao IdTipoOperacao { get; private set; }
        public int IdIntencaoOperacaoSituacao { get; private set; }
        public int? IdProximoPasso { get; private set; }
        public int? IdProximoPassoExcecao { get; private set; }
        public bool PassoInicial { get; private set; }

        public ProdutoDominio Produto { get; private set; }
        public TipoOperacaoDominio TipoOperacao { get; private set; }
        public IntencaoOperacaoSituacaoDominio SituacaoIntencaoOperacao { get; private set; }
        public IntencaoOperacaoSituacaoPassoDominio ProximoPasso { get; private set; }
        public IntencaoOperacaoSituacaoPassoDominio ProximoPassoExcecao { get; private set; }

        public IntencaoOperacaoSituacaoPassoDominio(Produto idProduto, TipoOperacao idTipoOperacao, int idIntencaoOperacaoSituacao, int? idProximoPasso = null, int? idProximoPassoExcecao = null, bool passoInicial = false)
        {
            IdTipoOperacao = idTipoOperacao;
            IdProduto = idProduto;
            IdIntencaoOperacaoSituacao = idIntencaoOperacaoSituacao;
            IdProximoPasso = idProximoPasso;
            IdProximoPassoExcecao = idProximoPassoExcecao;
            PassoInicial = passoInicial;
        }
    }
}
