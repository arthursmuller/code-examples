using Dominio;

namespace Aplicacao.Model.TipoLogradouro
{
    public class TipoLogradouroModel
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Codigo { get; set; }

        public TipoLogradouroModel() {}

        public TipoLogradouroModel(TipoLogradouroDominio logradouro) 
        {
            Id = logradouro.ID;
            Descricao = logradouro.Descricao;
            Codigo = logradouro.Codigo;
        }
    }
}
