namespace Infraestrutura.Providers.Cliente.Dto
{
    public class AgenciaCorreioDto
    {
        public int? IdAgenciaCorreio { get; set; }
        public string NomeAgenciaCorreio { get; set; }
        public string Porte { get; set; }
        public decimal ValorMaximo { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}
