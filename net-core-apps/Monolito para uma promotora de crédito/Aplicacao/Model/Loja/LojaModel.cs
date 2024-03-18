using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Aplicacao.Model.Municipio;
using Aplicacao.Model.TipoLogradouro;
using Dominio;

namespace Aplicacao
{
    public class LojaModel
    {
        public int Id { get; set; }
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

        public MunicipioModel Municipio { get; set; }
        public TipoLogradouroModel TipoLogradouro { get; set; }
        public IEnumerable<TelefoneLojaModel> Telefones { get; set; }
        
        public LojaModel() { }

        public LojaModel(LojaDominio loja)
        {
            Id = loja.ID;
            Latitude = loja.Geolocalizacao.Y;
            Longitude = loja.Geolocalizacao.X;

            Bairro = loja.Bairro;
            Cep = loja.Cep;
            Logradouro = loja.Logradouro;
            IdMunicipio = loja.IdMunicipio;
            IdTipoLogradouro = loja.IdTipoLogradouro;

            Municipio = loja.Municipio == null ? null : new MunicipioModel(loja.Municipio);
            TipoLogradouro = loja.TipoLogradouro == null ? null : new TipoLogradouroModel(loja.TipoLogradouro);

            Nome = loja.Nome;
            MensagemApresentacao = loja.MensagemApresentacao;
            Telefones = loja.Telefones == null || !loja.Telefones.Any() 
                ? null 
                : loja.Telefones.Select(x => new TelefoneLojaModel()
                {
                    Id = x.ID,
                    Telefone = x.Telefone,
                    PossuiContaWhatsApp = x.PossuiContaWhatsApp,
                    MensagemApresentacao = x.MensagemApresentacao
                });
        }
    }
}
