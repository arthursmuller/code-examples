namespace Aplicacao.Model.SeguroProfissao
{
    public class SeguroProfissaoIcatuExibicaoModel
    {
        public int ID { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public SeguroProfissaoIcatuExibicaoModel(int id, int codigo, string descricao)
        {
            ID = id;
            Codigo = codigo;
            Descricao = descricao;
        }
    }
}
