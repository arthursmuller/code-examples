using Aplicacao.Model.Loja;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aplicacao
{
    public class LojaCriacaoModel
    {
        public string Nome { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string MensagemApresentacao { get; set; }

        [Required]
        public int IdMunicipio { get; set; }
        public string Bairro { get; set; }
        [Required]
        public int IdTipoLogradouro { get; set; }
        public string Logradouro { get; set; }
        public int? Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }

        public List<TelefoneLojaCriacaoModel> Telefones { get; set; }
    }
}
