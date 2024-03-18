using System;
using SharedKernel.ValueObjects.v2;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infraestrutura.Fila.Whatsapp
{
    public interface IProducerWhatsapp
    {
        Task Publicar(string codigoReferencia, Guid template, Fone telefone, Dictionary<string, string> mensagem);
    }
}
