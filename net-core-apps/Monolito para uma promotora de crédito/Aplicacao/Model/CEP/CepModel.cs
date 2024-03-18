using Aplicacao.Model.Municipio;
using Aplicacao.Model.TipoLogradouro;
using Aplicacao.Model.UnidadeFederativa;
using Dominio;

namespace Aplicacao.CEP
{
    public class CepModel
    {
        public int Id { get; set; }

        public UnidadeFederativaModel Estado { get; set; }

        public MunicipioModel Cidade { get; set; }

        public TipoLogradouroModel TipoLogradouro { get; set; }

        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public string CEP { get; set; }

        public string DescricaoApoio { get; set; }

        public bool PermiteEditarLogradouro { get; set; }

        public CepModel(BaseCepDominio cepBase)
        {
            Id = cepBase.ID;
            Estado = cepBase.UF == null ? null : new UnidadeFederativaModel { Id = cepBase.UF.ID, Nome = cepBase.UF.Nome, Sigla = cepBase.UF.Sigla };
            Cidade = cepBase.Municipio == null ? null : new MunicipioModel(cepBase.Municipio);
            TipoLogradouro = cepBase.TipoLogradouro == null ? null
                : new TipoLogradouroModel { Id = cepBase.TipoLogradouro.ID, Descricao = cepBase.TipoLogradouro.Descricao, Codigo = cepBase.TipoLogradouro.Codigo };
            Logradouro = cepBase.Logradouro;
            Bairro = cepBase.Bairro;
            CEP = cepBase.CEP;
            DescricaoApoio = cepBase.InformacaoAdicional;
            PermiteEditarLogradouro = cepBase.PermiteAjusteLogradouro;
        }
    }
}
