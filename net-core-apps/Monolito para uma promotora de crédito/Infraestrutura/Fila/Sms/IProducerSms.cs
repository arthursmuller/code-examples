using SharedKernel.ValueObjects.v2;
using System.Threading.Tasks;

namespace Infraestrutura.Fila.Sms
{
    public interface IProducerSms
    {
        Task Publicar(string codigoReferenciaMensagem, Fone telefone, string mensagem);
    }
}
