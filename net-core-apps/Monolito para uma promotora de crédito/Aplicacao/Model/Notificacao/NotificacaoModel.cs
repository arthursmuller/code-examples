using System;
using Dominio.Enum.Notificacoes;

namespace Aplicacao.Model.Notificacoes
{
    public class NotificacaoModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string UrlReferencia { get; set; }
        public DateTime DataNotificacao { get; set; }
        public DateTime? DataVisualizacao { get; set; }
        public NotificacaoSeveridade Severidade { get; set; }
    }
}
