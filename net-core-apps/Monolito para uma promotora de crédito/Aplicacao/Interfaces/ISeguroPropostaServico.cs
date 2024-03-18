using Aplicacao.Model.Documento;
using Aplicacao.Model.SeguroProposta;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface ISeguroPropostaServico
    {
        Task<SeguroPropostaExibicaoModel> Listar();
        Task<string> ConsultarLinkPagamento();
        Task<string> Criar(CriarSeguroPropostaModel model);
        Task<DocumentoModel> BaixarTermo();
        Task<bool> EnviarPropostaIcatu();
        Task<IEnumerable<MeioPagamentoExibicaoModel>> ListarMeioPagamentos();
    }
}
