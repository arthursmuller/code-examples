namespace Dominio
{
    public class BaseCepDominio : EntidadeBase
    {
        public string CEP { get; private set; }

        public string Logradouro { get; private set; }

        public string Bairro { get; private set; }

        public string InformacaoAdicional { get; private set; }

        public bool PermiteAjusteLogradouro { get; private set; }

        public int IdTipoLogradouro { get; private set; }

        public int IdMunicipio { get; private set; }

        public int IdUnidadeFederativa { get; private set; }

        public TipoLogradouroDominio TipoLogradouro { get; private set; }

        public MunicipioDominio Municipio { get; private set; }

        public UnidadeFederativaDominio UF { get; private set; }

        public BaseCepDominio() { }

        public BaseCepDominio(
            string cep,
            string logradouro,
            string bairro,
            string informacaoAdicional,
            bool permiteAjusteLogradouro,
            int idTipoLogradouro,
            int idMunicipio,
            int idUnidadeFederativa)
        {
            CEP = cep;
            Logradouro = logradouro;
            Bairro = bairro;
            InformacaoAdicional = informacaoAdicional;
            PermiteAjusteLogradouro = permiteAjusteLogradouro;
            IdTipoLogradouro = idTipoLogradouro;
            IdMunicipio = idMunicipio;
            IdUnidadeFederativa = idUnidadeFederativa;
        }
    }
}
