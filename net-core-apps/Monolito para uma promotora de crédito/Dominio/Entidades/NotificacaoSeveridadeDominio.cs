using Dominio.Enum.Notificacoes;

namespace Dominio
{
    public class NotificacaoSeveridadeDominio : EntidadeBase
    {
        public new NotificacaoSeveridade ID { get; private set; }

        public string Descricao { get; private set; }

        public NotificacaoSeveridadeDominio() { }

        public NotificacaoSeveridadeDominio(NotificacaoSeveridade id, string descricao)
        {
            ID = id;
            Descricao = descricao;
        }
    }
}
