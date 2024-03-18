using Infraestrutura.Providers.IcatuApi.Dto;
using System.Threading.Tasks;

namespace Infraestrutura.Providers.IcatuApi
{
    public interface IProviderIcatu
    {
        Task<CriarNumeroPropostaRespostaDto> CriarNumeroProposta(CriarNumeroPropostaDto model);
        Task<PedidoPagamentoRespostaDto> CriarPedidoPagamento(CriarPedidoPagamentoDto model);
        Task<ConsultarPedidoPagamentoDto> ConsultarPedidoPagamento(string idPedidoPagamento);
        Task<CriarPropostaRespostaDto> CriarProposta(CriarPropostaDto model);
    }
}

