using Aplicacao.Model.Autenticacao;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface IAutenticacaoServico
    {
        Task<AutenticacaoModel> Autenticar(LoginModel model, bool administrador = false);
    }
}