namespace Dominio
{
    public class OrgaoEmissorIdentificacaoDominio : EntidadeBase
    {
        public string Codigo { get; private set; }

        public string Descricao { get; private set; }

        public OrgaoEmissorIdentificacaoDominio(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }
    }
}
