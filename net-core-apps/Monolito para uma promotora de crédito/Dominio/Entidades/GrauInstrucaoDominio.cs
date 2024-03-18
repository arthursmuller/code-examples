namespace Dominio
{
    public class GrauInstrucaoDominio : EntidadeBase
    {
        public string Descricao { get; private set; }

        public GrauInstrucaoDominio(string descricao)
        {
            Descricao = descricao;
        }
    }
}
