using Dominio.Enum.Notificacoes;

namespace Dominio
{
    public class NotificacaoTemplateDominio : EntidadeBase
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string UrlReferencia { get; private set; }
        public NotificacaoSeveridade IdNotificacaoSeveridade { get; private set; }
        public NotificacaoFinalidade IdNotificacaoFinalidade { get; private set; }

        public NotificacaoFinalidadeDominio Finalidade { get; private set; }
        public NotificacaoSeveridadeDominio Severidade { get; private set; }

        public NotificacaoTemplateDominio() { }

        public NotificacaoTemplateDominio(string titulo, string descricao, string urlReferencia, NotificacaoSeveridade severidade, NotificacaoFinalidade finalidade)
        {
            Titulo = titulo;
            Descricao = descricao;
            UrlReferencia = urlReferencia;
            IdNotificacaoSeveridade = severidade;
            IdNotificacaoFinalidade = finalidade;
        }
    }
}
