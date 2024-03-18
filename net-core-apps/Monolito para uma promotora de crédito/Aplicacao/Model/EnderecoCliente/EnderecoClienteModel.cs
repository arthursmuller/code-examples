using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Model.EnderecoCliente
{
    public class EnderecoClienteModel
    {
        public int? Id { get; set; }
       
        public string Titulo { get; set; }

        [Required]
        public int IdMunicipio { get; set; }

        public string Bairro { get; set; }

        [Required]
        public int IdTipoLogradouro { get; set; }

        public string Logradouro { get; set; }

        public int? Numero { get; set; }

        public string Complemento { get; set; }

        public string Cep { get; set; }

        public bool Principal { get; set; }
    }
}
