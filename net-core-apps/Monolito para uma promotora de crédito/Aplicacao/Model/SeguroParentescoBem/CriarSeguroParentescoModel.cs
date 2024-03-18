namespace Aplicacao.Model.SeguroParentescoBem
{
    public class CriarSeguroParentescoModel
    {
        public int CodigoParentesco { get; set; }
        public string NomeParentesco { get; set; }
        public CriarSeguroParentescoModel()  { }
        public CriarSeguroParentescoModel(int codigoParentesco, string nomeParentesco)
        {
            CodigoParentesco = codigoParentesco;
            NomeParentesco = nomeParentesco;
        }

    }
}
