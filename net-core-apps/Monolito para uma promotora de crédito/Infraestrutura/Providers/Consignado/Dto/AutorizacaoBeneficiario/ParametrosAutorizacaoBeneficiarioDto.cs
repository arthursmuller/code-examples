using SharedKernel.ValueObjects.v2;

namespace Infraestrutura.Providers.Consignado.Dto
{
    public class ParametrosAutorizacaoBeneficiarioDto
    {
        public string NomeCliente { get; set; }

        public CPF Cpf { get; set; }
    }
}
