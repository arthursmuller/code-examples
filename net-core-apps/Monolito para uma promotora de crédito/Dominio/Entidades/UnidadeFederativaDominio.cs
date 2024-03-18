namespace Dominio
{
    public class UnidadeFederativaDominio : EntidadeBase
    {
        public string Nome { get; private set; }

        public string Sigla { get; private set; }
        
        public UnidadeFederativaDominio(string nome, string sigla)
        {
            Nome = nome;
            Sigla = sigla;
        }
    }
}
