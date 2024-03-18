namespace Infraestrutura.Providers.Cliente.Dto
{
    public class ClienteContatosDto
    {
        public ClienteContatosDto()
        {
            Telefone1 = new TelefoneDto();
            Telefone2 = new TelefoneDto();
        }

        public TelefoneDto Telefone1 { get; set; }

        public TelefoneDto Telefone2 { get; set; }
    }
}
