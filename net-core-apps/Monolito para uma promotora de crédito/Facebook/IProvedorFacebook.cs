using System.Threading.Tasks;

namespace LoginSocialFacebook
{
    public interface IProvedorFacebook
    {
        Task<ValidacaoTokenRetornoDto> ValidarToken(string token);
    }
}
