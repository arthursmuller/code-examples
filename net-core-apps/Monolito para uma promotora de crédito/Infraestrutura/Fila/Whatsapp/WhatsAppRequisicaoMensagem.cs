using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Fila.Model.WhatsApp
{
    [ExcludeFromCodeCoverage]
    public class WhatsAppRequisicaoMensagem
    {
        public string CodigoReferenciaMensagem {get; set;}
        public string DDD { get; set; }
        public string Telefone { get; set; }
        public Guid IdTemplate { get; set; }
        public Dictionary<string, string> Mensagem { get; set; }
        public int IdWhatsAppFornecedor { get; set; }

    }

}
