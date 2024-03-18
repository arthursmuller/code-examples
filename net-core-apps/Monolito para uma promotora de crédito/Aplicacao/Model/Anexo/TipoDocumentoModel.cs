namespace Aplicacao
{
    public class TipoDocumentoModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Codigo { get; set; }

        public TipoDocumentoModel() { }
        public TipoDocumentoModel(int id, string nome, string codigo)
        {
            Id = id;
            Nome = nome;
            Codigo = codigo;
        }
    }
}
