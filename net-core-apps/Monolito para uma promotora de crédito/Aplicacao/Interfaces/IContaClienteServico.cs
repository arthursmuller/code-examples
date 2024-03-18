using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacao.Model.ContaCliente;
using Dominio;

namespace Aplicacao.Servico
{
    public interface IContaClienteServico
    {
        Task<IEnumerable<ContaClienteExibicaoModel>> ListarContasAutenticado();

        Task<bool> ExcluirContaAutenticado(int idConta);

        Task<bool> VerificarContaCliente(int idConta, int idCliente);

        Task<ContaClienteDominio> CriarConta(ContaClienteModel requisicao, int idCliente);
    }
}
