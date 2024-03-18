using System;
using Newtonsoft.Json;

namespace  Infraestrutura.DTO.ZenviaMensagemVoz
{
    public class ZenviaMensagemVozDto
    {
        [JsonProperty("numero_destino")]
        public string NumeroDestino { get; set; }
        
        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }
  }
}
