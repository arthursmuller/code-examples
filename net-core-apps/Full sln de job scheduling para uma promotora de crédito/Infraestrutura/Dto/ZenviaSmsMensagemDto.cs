using System;

namespace Infraestrutura.DTO.Zenvia
{
    public class ZenviaSmsMensagemDto
    {
        public string Id { get; set; }
        public int AggregateId { get; set; }
        public string To { get; set; }
        public string Msg { get; set; }
        public string From { get; set; }
  }
}
