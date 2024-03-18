namespace Aplicacao.Model.Genero
{
    public class GeneroModel
    {
        public int Id { get; set; }

        public string Sigla { get; set; }

        public string Descricao { get; set; }

        public GeneroModel() { }
        public GeneroModel(int id, string sigla, string descricao)
        {
            Id = id;
            Sigla = sigla;
            Descricao = descricao;
        }

    }
}
