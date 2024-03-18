using System.Threading.Tasks;
using Infraestrutura.DTO.ZenviaTorpedoVoz;

namespace Infraestrutura.Providers
{
    public interface IProviderZenviaTotalVoice
    {
        Task<bool> EnviarMensagemVoz(ZenviaTorpedoVozDto requisicao, string _credencialApi);
    }
}