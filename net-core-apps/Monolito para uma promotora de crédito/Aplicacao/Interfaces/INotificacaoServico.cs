using Aplicacao.Model.Notificacoes;
using Dominio;
using Dominio.Enum.Notificacoes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface INotificacaoServico
    {
        Task<IEnumerable<NotificacaoModel>> BuscarNotificacoesAutenticado();

        Task<NotificacaoModel> MarcarVisualizacao(int idNotificacao);

        Task<NotificacaoModel> CompletarNotificacao(int? idNotificacao, NotificacaoFinalidade? finalidade = null);

        Task<NotificacaoDominio> GerarNotificacao(int idUsuario, NotificacaoFinalidade finalidade, Dictionary<string, object> chaves = null);
    }
}
