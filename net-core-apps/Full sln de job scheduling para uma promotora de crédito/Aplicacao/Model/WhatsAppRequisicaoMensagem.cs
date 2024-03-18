using System;
using System.Collections.Generic;

namespace Fila.Model.WhatsApp
{
    public class WhatsAppRequisicaoMensagem
    {
        public string CodigoReferenciaMensagem {get; set;}    
        public string DDD {get; set;}
        public string Telefone {get; set;}
        public Guid IdTemplate { get; set; }
        public Dictionary<string, string> Mensagem { get; set; }
        public int IdWhatsAppFornecedor { get; set; }

       public WhatsAppRequisicaoMensagem(){
            this.Mensagem = new Dictionary<string, string>();
        }
    }
}
