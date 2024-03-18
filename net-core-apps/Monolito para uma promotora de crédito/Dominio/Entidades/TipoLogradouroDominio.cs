namespace Dominio
{
    public class TipoLogradouroDominio : EntidadeBase
    {
        public string Descricao { get; private set; }

        public string Codigo { get; private set; }

        public TipoLogradouroDominio(string codigo, string descricao)
        {
            Descricao = descricao;
            Codigo = codigo;
        }
    }
}
