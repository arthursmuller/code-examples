using System.Threading.Tasks;
using Infraestrutura.DTO.Email;
using Infraestrutura.Enum;

namespace Infraestrutura.Providers
{
    public interface IProviderEmail
    {
        Task<StatusEnvio> EnviarEmail(EmailMensagemDto requisicao);
    }
}