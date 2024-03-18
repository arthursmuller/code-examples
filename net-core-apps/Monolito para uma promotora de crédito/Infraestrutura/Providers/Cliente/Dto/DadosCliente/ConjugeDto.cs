using System;

namespace Infraestrutura.Providers.Cliente.Dto
{
    public class ConjugeDto
    {
        public int? CodigoRegimeCasamento { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Sexo { get; set; }
    }
}
