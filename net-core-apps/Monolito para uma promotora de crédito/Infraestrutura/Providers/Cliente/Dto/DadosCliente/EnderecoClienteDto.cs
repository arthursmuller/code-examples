namespace Infraestrutura.Providers.Cliente.Dto
{
    public class EnderecoClienteDto
    {
        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Uf { get; set; }

        public int? Numero { get; set; }

        public string Complemento { get; set; }

        public int? CodTipoLogradouro { get; set; }

        public string DescricaoTipoLogradouro { get; set; }

        public string Municipio { get; set; }

        public bool IsValid { get; set; }

        public bool EditarEndereco { get; set; }

        public bool MostrarEndereco { get; set; }
    }
}
