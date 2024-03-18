using Aplicacao.Model.UnidadeFederativa;
using Dominio;

namespace Aplicacao.Model.Municipio
{
    public class MunicipioModel
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public UnidadeFederativaModel UF { get; set; }

        public MunicipioModel() { }

        public MunicipioModel(MunicipioDominio municipio)
        {
            Id = municipio.ID;
            Descricao = municipio.Descricao;
            UF = municipio.UF == null ? null :
                new UnidadeFederativaModel
                {
                    Id = municipio.UF.ID,
                    Nome = municipio.UF.Nome,
                    Sigla = municipio.UF.Sigla
                };
        }
    }
}
