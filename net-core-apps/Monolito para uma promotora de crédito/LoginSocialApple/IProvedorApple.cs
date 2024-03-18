using LoginSocialApple.Dto;
using System.Threading.Tasks;

namespace LoginSocialApple
{
    public interface IProvedorApple
    {
        Task<ValidacaoTokenRetornoDto> ValidarToken(string token);
        Task<ValidacaoTokenRetornoDto> ValidarToken(string codigoAutorizacao, string redirectUrl);
    }
}
