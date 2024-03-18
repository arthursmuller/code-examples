using System;

namespace Dominio
{
    public class IntencaoOperacaoObservacaoDominio : EntidadeBase
    {
        public int IdIntencaoOperacao { get; private set; }
        public string Observacao { get; private set; }

        public DateTime DataInclusao { get; private set; } = DateTime.Now;

        public IntencaoOperacaoDominio IntencaoOperacao { get; private set; }

        protected IntencaoOperacaoObservacaoDominio() { }

        public IntencaoOperacaoObservacaoDominio(int idIntencaoOperacao, string observacao)
        {
            IdIntencaoOperacao = idIntencaoOperacao;
            Observacao = observacao;
        }
    }
}
