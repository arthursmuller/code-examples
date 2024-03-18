namespace Dominio
{
    public class InssEspecieBeneficioDominio: EntidadeBase
    {
        public string Descricao { get; set; }

        public string Codigo { get; set; }

        public InssEspecieBeneficioDominio() { }

        public InssEspecieBeneficioDominio(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

    }
}
