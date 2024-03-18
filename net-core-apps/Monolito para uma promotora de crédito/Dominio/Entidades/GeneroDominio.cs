namespace Dominio
{
    public class GeneroDominio : EntidadeBase
    {
        public string Descricao { get; private set; }

        public string Sigla { get; private set; }

        public GeneroDominio(string descricao, string sigla)
        {
            Descricao = descricao;
            Sigla = sigla;
        }
    }
}
