namespace Aplicacao.Model.SeguroParentescoBem
{
    public class CriarSeguroParentescoIcatuModel
    {
        public int IdSeguroParentesco { get; set; }
        public string Descricao { get; set; }
        public int Codigo { get; set; }

        public CriarSeguroParentescoIcatuModel(int idSeguroParentesco, string descricao, int codigo)
        {
            IdSeguroParentesco = idSeguroParentesco;
            Descricao = descricao;
            Codigo = codigo;
        }
    }
}
