using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class AssinaturaDominio : EntidadeBase
    {
        public int Longitude { get; set; }
        public int Latitude { get; set; }
        public string IPOrigem { get; set; }
    }
}
