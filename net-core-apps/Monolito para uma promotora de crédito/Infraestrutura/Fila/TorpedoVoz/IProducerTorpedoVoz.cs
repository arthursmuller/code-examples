using SharedKernel.ValueObjects.v2;
using System.Threading.Tasks;

namespace Infraestrutura.Fila.TorpedoVoz
{
    public interface IProducerTorpedoVoz
    {
        Task Publicar(string codigoReferenciaMensagem, Fone telefone, string mensagem);
    }
}
