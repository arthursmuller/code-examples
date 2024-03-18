namespace Dominio
{
    public class IntencaoOperacaoSituacaoDominio : EntidadeBase
    {
        public string Nome { get; private set; }
        public string DescricaoPadrao { get; private set; }
        public bool PermiteAtualizacoes { get; private set; }
        public bool PermiteSituacaoExtraordinaria { get; private set; }
        public bool SituacaoExtraordinaria { get; private set; }

        public IntencaoOperacaoSituacaoDominio(string nome, string descricaoPadrao, bool permiteAtualizacoes, bool permiteSituacaoExtraordinaria, bool situacaoExtraordinaria = false)
        {
            Nome = nome;
            DescricaoPadrao = descricaoPadrao;
            PermiteAtualizacoes = permiteAtualizacoes;
            PermiteSituacaoExtraordinaria = permiteSituacaoExtraordinaria;
            SituacaoExtraordinaria = situacaoExtraordinaria;
        }
    }
}
