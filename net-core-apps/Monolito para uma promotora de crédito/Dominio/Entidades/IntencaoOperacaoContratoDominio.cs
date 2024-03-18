namespace Dominio
{
    public class IntencaoOperacaoContratoDominio : EntidadeBase
    {
        public string Contrato { get; private set; }

        public int IdIntencaoOperacao { get; private set; }

        public IntencaoOperacaoDominio IntencaoOperacao { get; private set; }

        public IntencaoOperacaoContratoDominio(string contrato)
            => Contrato = contrato;

        public IntencaoOperacaoContratoDominio(int idIntencaoOperacao, string contrato)
        {
            IdIntencaoOperacao = idIntencaoOperacao;
            Contrato = contrato;
        }
    }
}
