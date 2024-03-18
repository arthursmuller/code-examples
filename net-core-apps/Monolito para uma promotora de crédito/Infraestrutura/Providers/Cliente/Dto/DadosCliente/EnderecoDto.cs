namespace Infraestrutura.Providers.Cliente.Dto
{
    public class EnderecoDto
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

        public bool IsValid => !string.IsNullOrWhiteSpace(Cep);

        /// <summary>
        /// Se Logradouro não informado, os campos "TipoLogradouro", "Logradouro" e "Bairro" podem ser modificados
        /// </summary>
        public bool EditarEndereco => string.IsNullOrWhiteSpace(Logradouro);
    }
}
