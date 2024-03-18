using Infraestrutura.Providers.Kaledo.DTO;
using System.Threading.Tasks;

namespace Infraestrutura.Providers.Kaledo
{
    public interface IProviderKaledo
    {
        Task<KaledoResultadoCriarAuntenticarUsuarioDTO> CriarAutenticarUsuario(KaledoCriarAutenticarUsuarioDTO dto);
    }
}
