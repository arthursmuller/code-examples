namespace Dominio
{
    public class SiapeTipoFuncionalDominio: EntidadeBase
    {
        public string Descricao { get; set; }

        public string Codigo { get; set; }

        public SiapeTipoFuncionalDominio() { }

        public SiapeTipoFuncionalDominio(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

    }
}
