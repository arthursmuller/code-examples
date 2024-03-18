namespace Dominio
{
    public class BancoDominio : EntidadeBase
    {
        public string Nome { get; private set; }

        public string CNPJ { get; private set; }

        public string Codigo { get; private set; }

        public bool PermitePortabilidade { get; private set; }

        public BancoDominio(string codigo, string cNPJ, string nome, bool permitePortabilidade)
        {
            Codigo = codigo;
            CNPJ = cNPJ;
            Nome = nome;
            PermitePortabilidade = permitePortabilidade;
        }
    }
}
