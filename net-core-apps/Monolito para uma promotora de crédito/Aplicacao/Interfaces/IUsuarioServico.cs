using Aplicacao.Model.Autenticacao;
using Dominio;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface IUsuarioServico
    {
        Task<UsuarioModel> ObterUsuarioAutenticado();

        Task<UsuarioModel> CriarUsuario(UsuarioCriacaoModel usuario);

        Task<UsuarioModel> AtualizarUsuario(UsuarioAtualizacaoModel usuarioAtualizado);

        Task<AutenticacaoModel> AtualizarSenhaUsuarioLogado(UsuarioAtualizacaoSenhaModel requisicao);

        Task<AutenticacaoModel> AtualizarSenhaUsuarioELogar(string token, string novaSenha);

        Task<AutenticacaoModel> ConfirmarEmailELogar(string token, int userId);

        Task<bool> RequisitarTrocaDeSenha(string email);

        Task<bool> ConsultarValidadeTokenSenha(string token);

        Task GravarLogRequisicao(UsuarioRequisicaoLogDominio log);
    }
}
