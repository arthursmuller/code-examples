using Aplicacao.Model.RendimentoCliente;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface IRendimentoClienteServico
    {
        Task<IEnumerable<RendimentoClienteExibicaoModel>> BuscarRendimentosPorCliente();

        Task<bool> VerificarSeRendimentoPertenceAoUsuario(int idRendimento, int? idUsuario = null);

        Task<RendimentoClienteExibicaoModel> GravarRendimento(RendimentoClienteModel rendimento);

        Task<RendimentoClienteExibicaoModel> AtualizarRendimento(int idRendimentoCliente, RendimentoClienteModel rendimentoAtualizar);

        Task<bool> RemoverRendimento(int idRendimentoCliente);
    }
}
