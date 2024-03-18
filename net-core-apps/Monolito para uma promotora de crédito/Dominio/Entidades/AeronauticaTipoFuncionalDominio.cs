namespace Dominio
{
    public class AeronauticaTipoFuncionalDominio : EntidadeBase
    {
        public string Descricao { get; private set; }
        public string Sigla { get; private set; }

        public AeronauticaTipoFuncionalDominio(string sigla, string descricao)
        {
            Descricao = descricao;
            Sigla = sigla;
        }
    }
}
