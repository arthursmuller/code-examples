using Dominio.Enum;

namespace Dominio
{
    public class MeioPagamentoSeguroDominio : EntidadeBase
    {
        public Produto IdProduto { get; private set; }
        public ProdutoDominio Produto { get; set; }
        public MeioPagamentoSeguro IdMeioPagamento { get; private set; }
    }
}
