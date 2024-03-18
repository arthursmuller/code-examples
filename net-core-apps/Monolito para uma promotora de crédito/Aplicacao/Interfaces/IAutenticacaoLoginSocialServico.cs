using Aplicacao.Model.Autenticacao;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface IAutenticacaoLoginSocialServico : IAutenticacaoServicoBase
    {
        Task<AutenticacaoLoginSocialModel> Autenticar(LoginSocialModel loginSocial);
        Task<ValidacaoTokenModel> ValidarToken(LoginSocialModel loginSocial);
    }
}
