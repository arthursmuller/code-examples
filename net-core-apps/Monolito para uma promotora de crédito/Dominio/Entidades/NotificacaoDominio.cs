using System;
using Dominio.Enum.Notificacoes;

namespace Dominio
{
    public class NotificacaoDominio : EntidadeBase
    {
        public int IdUsuario { get; private set; }
        public int? IdTemplateNotificacao { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string UrlReferencia { get; private set; }
        public DateTime? DataCriacao { get; private set; }
        public DateTime? DataVisualizacao { get; private set; }
        public bool Completo { get; private set; }
        public NotificacaoSeveridade IdNotificacaoSeveridade { get; private set; }
        public NotificacaoFinalidade IdNotificacaoFinalidade { get; private set; }


        public NotificacaoTemplateDominio Template { get; private set; }
        public NotificacaoFinalidadeDominio Finalidade { get; private set; }
        public NotificacaoSeveridadeDominio Severidade { get; private set; }

        public UsuarioDominio Usuario { get; private set; }

        public NotificacaoDominio() { }

        public NotificacaoDominio(int idUsuario, int? idTemplateNotificacao, string titulo, string descricao, string urlReferencia,
            NotificacaoSeveridade severidade, NotificacaoFinalidade Finalidade)
        {
            IdUsuario = idUsuario;
            IdTemplateNotificacao = idTemplateNotificacao;
            Titulo = titulo;
            Descricao = descricao;
            UrlReferencia = urlReferencia;
            IdNotificacaoSeveridade = severidade;

            DataCriacao = DateTime.Now;
        }

        public void RegistarVisualizacao()
        {
            DataVisualizacao = DateTime.Now;
            setDataAtualizacao();
        }

        public void RegistrarCompletude()
        {
            Completo = true;
            setDataAtualizacao();
        }
    }
}
