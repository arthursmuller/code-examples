using Aplicacao.Model.Anexo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface IAnexoServico
    {
        Task<AnexoModel> GravarArquivo(AnexoCriacaoModel anexo);

        Task<IEnumerable<AnexoModel>> BuscarAnexosPorUsuarioAutenticado();

        Task<IEnumerable<AnexoModel>> BuscarAnexosPorCpfUsuario(string cpf);

        Task<AnexoModel> BuscarAnexo(int idAnexo);

        Task<bool> DeletarAnexo(int id);

        Task<IEnumerable<TipoDocumentoModel>> ListarTiposDocumentos();

        Task<TipoDocumentoModel> ObterTipoDocumentoPorDescricao(string descricao);

        Task<bool> SolicitarAnexoParaCliente(AnexoSolicitacaoModel solicitacao);
    }
}
