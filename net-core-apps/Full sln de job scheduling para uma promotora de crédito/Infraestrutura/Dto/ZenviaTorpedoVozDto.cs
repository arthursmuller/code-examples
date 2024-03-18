using System;
using Newtonsoft.Json;

namespace  Infraestrutura.DTO.ZenviaTorpedoVoz
{
    public class ZenviaTorpedoVozDto
    {
        [JsonProperty("numero_destino")]
        public string NumeroDestino { get; set; }
        
        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }
  }
}
