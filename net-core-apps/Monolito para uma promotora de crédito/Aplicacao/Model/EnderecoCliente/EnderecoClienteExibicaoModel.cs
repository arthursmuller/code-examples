using Aplicacao.Model.Municipio;
using Aplicacao.Model.TipoLogradouro;
using Dominio;

namespace Aplicacao.Model.EnderecoCliente
{
    public class EnderecoClienteExibicaoModel
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public MunicipioModel Municipio { get; set; }

        public string Bairro { get; set; }

        public TipoLogradouroModel TipoLogradouro { get; set; }

        public string Logradouro { get; set; }

        public int? Numero { get; set; }

        public string Complemento { get; set; }

        public string Cep { get; set; }

        public bool Principal { get; set; }

        public EnderecoClienteExibicaoModel(EnderecoClienteDominio enderecoDominio)
        {
            Id = enderecoDominio.ID;
            Titulo = enderecoDominio.Titulo;
            Municipio = enderecoDominio.Municipio == null ? null : new MunicipioModel(enderecoDominio.Municipio);
            Bairro = enderecoDominio.Bairro;
            TipoLogradouro = enderecoDominio.TipoLogradouro == null ? null :
                new TipoLogradouroModel
                {
                    Id = enderecoDominio.TipoLogradouro.ID,
                    Codigo = enderecoDominio.TipoLogradouro.Codigo,
                    Descricao = enderecoDominio.TipoLogradouro.Descricao
                };
            Logradouro = enderecoDominio.Logradouro;
            Numero = enderecoDominio.Numero;
            Complemento = enderecoDominio.Complemento;
            Cep = enderecoDominio.Cep;
            Principal = enderecoDominio.Principal;
        }
    }
}
