using Infraestrutura.Providers.Unico.DTO;
using System.Threading.Tasks;

namespace Infraestrutura.Providers.Unico
{
    public interface IUnicoProvider
    {
        Task<UnicoResultadoBuscaProcessoDto> BuscarProcesso(string codigoProcesso);
        Task<UnicoResultadoCriarProcessoDto> CriarProcesso(UnicoRequisicaoCriarProcessoDto dto);

    }
}