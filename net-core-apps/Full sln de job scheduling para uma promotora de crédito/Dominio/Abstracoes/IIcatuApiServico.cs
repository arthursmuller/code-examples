using Dominio.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Abstracoes
{
    public interface IIcatuApiServico
    {
        Task<IEnumerable<IcatuParentescoDto>> GetParentescos();
        Task<IEnumerable<IcatuProfissaoDto>> GetProfissoes();
    }
}
