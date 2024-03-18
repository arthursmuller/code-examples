using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Infraestrutura.Providers.IcatuApi.Dto
{
    [ExcludeFromCodeCoverage]
    public class PedidoPagamentoRespostaDto
    {
        public string Identificador { get; set; }
        public string Codigo { get; set; }
        public int Valor { get; set; }
        public bool Fechado { get; set; }
        public string Status { get; set; }
        public string DataCriacao { get; set; }
        public string DataAlteracao { get; set; }
        public List<Checkout> Checkouts { get; set; }
    }

    public class Checkout
    {
        public string Identificador { get; set; }
        public string Url { get; set; }
        public string DataExpiracao { get; set; }
    }
}
