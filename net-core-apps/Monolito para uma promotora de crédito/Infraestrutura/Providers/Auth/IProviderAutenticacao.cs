using Infraestrutura.Providers.Auth.Dto;
using System.Threading.Tasks;

namespace Infraestrutura.Providers.Auth
{
    public interface IProviderAutenticacao
    {
        Task<RetornoAtenticacaoDto> Autenticar(ParametroAutenticacaoDto parametros);
    }
}
