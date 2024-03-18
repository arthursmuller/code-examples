using System.Threading.Tasks;

namespace Infraestrutura.Fila.Email
{
    public interface IProducerEmail
    {
        Task Publicar(string codigoReferencia, string[] destinatarios, string assunto, string corpo);
    }
}
