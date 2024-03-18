namespace Dominio
{
    public class MunicipioDominio : EntidadeBase
    {
        public string Descricao { get; private set; }

        public int IdUnidadeFederativa { get; private set; }

        public UnidadeFederativaDominio UF { get; private set; }

        public MunicipioDominio(string descricao, int idUnidadeFederativa)
        {
            Descricao = descricao;
            IdUnidadeFederativa = idUnidadeFederativa;
        }
    }
}
