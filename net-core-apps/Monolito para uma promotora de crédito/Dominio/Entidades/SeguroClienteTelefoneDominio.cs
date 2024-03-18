using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class SeguroClienteTelefoneDominio : EntidadeBase
    {
        public string DDD { get; set; }
        public string Fone { get; set; }
        public bool Deletado { get; set; }
        public bool Principal { get; set; }
        public int? IdCliente { get; private set; }
        public SeguroClienteIcatuDominio Cliente { get; private set; }

        public SeguroClienteTelefoneDominio(string dDD, string fone, bool principal, int? idCliente)
        {
            DDD = dDD;
            Fone = fone;
            Principal = principal;
            IdCliente = idCliente;
        }
    }
}
