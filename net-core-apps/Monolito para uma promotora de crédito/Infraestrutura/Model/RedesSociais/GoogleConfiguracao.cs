using System.Collections.Generic;

namespace Infraestrutura.RedesSociais
{
    public class GoogleConfiguracao
    {
        public string ClientId { get; set; }
        public IEnumerable<string> EmitentesValidos { get; set; }
    }
}
