namespace Infraestrutura.Providers.Consignado.Dto
{
    public class ParametrosContratoClienteDto
    {
        public string CpfCliente { get; set; }

        public string Matricula { get; set; }

        public long? Proposta { get; set; }

        public bool ApenasConveniadasAtivas { get; set; }
    }
}
