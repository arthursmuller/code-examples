using Aplicacao.Model.Autenticacao;

namespace Aplicacao.Servico
{
    public interface IAutenticacaoServicoBase
    {
        AutenticacaoModel GerarToken(int idUsuario, string nomeUsuario, string email);
    }
}
