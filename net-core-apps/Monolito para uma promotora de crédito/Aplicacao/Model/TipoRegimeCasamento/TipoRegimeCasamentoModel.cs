namespace Aplicacao.Model.TipoRegimeCasamento
{
    public class TipoRegimeCasamentoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public TipoRegimeCasamentoModel(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }
}
