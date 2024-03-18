using Dominio.Enum;

namespace Dominio
{
    public class TipoSolicitacaoConfirmacaoDominio : EntidadeBase
    {
        public new TipoSolicitacaoConfirmacao ID { get; private set; }
        public string Nome { get; private set; }

        public TipoSolicitacaoConfirmacaoDominio() { }

        public TipoSolicitacaoConfirmacaoDominio(TipoSolicitacaoConfirmacao id, string nome)
        {
            ID = id;
            Nome = nome;
        }
    }
}
