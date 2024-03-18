namespace Dominio
{
    public class MarinhaTipoFuncionalDominio : EntidadeBase
    {
        public string Descricao { get; private set; }
        public string Sigla { get; private set; }

        public MarinhaTipoFuncionalDominio(string sigla, string descricao)
        {
            Descricao = descricao;
            Sigla = sigla;
        }
    }
}
