using System;

namespace Dominio
{
    public class RegistroClubeBeneficiosDominio : EntidadeBase
    {
        public int IdCliente { get; private set; }
        public ClienteDominio Cliente { get; set; }
        public DateTime DataAcesso { get; private set; } = DateTime.Now;

        public RegistroClubeBeneficiosDominio(int idCliente)
        {
            IdCliente = idCliente;
        }
    }
}
