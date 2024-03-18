using Dominio.Enum;

namespace Dominio
{
    public class ConvenioOrgaoDominio : EntidadeBase
    {
        public string Codigo { get; set; }

        public string Nome { get; set; }

        public string CNPJ { get; set; }

        public int? IdUnidadeFederativa { get; set; }

        public UnidadeFederativaDominio UF { get; set; }

        public Convenio IdConvenio { get; private set; }

        public ConvenioDominio Convenio { get; set; }

        public ConvenioOrgaoDominio(string codigo, string cNPJ, string nome, Convenio idConvenio, int? idUnidadeFederativa = null)
        {
            Codigo = codigo;
            Nome = nome;
            CNPJ = cNPJ;
            IdUnidadeFederativa = idUnidadeFederativa;
            IdConvenio = idConvenio;
        }
    }
}
