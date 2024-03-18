using Dominio.Enum.Notificacoes;

namespace Dominio
{
    public class NotificacaoFinalidadeDominio : EntidadeBase
    {
        public new NotificacaoFinalidade ID { get; private set; }

        public string Descricao { get; private set; }

        public NotificacaoFinalidadeDominio() { }

        public NotificacaoFinalidadeDominio(NotificacaoFinalidade id, string descricao)
        {
            ID = id;
            Descricao = descricao;
        }
    }
}
