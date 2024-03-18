using System;

namespace Dominio
{
    public class IntencaoOperacaoHistoricoDominio : EntidadeBase
    {
        public int IdIntencaoOperacao { get; set; }
        public int IdIntencaoOperacaoSituacao { get; set; }
        public string DescricaoEspecifica { get; private set; }
        public bool PendenciaUsuario { get; private set; }

        public IntencaoOperacaoDominio IntencaoOperacao { get; private set; }
        public IntencaoOperacaoSituacaoDominio SituacaoIntencaoOperacao { get; private set; }

        public string Descricao { get => !String.IsNullOrWhiteSpace(DescricaoEspecifica) ? DescricaoEspecifica : SituacaoIntencaoOperacao?.DescricaoPadrao ?? null; }

        public IntencaoOperacaoHistoricoDominio(int idIntencaoOperacao, int idIntencaoOperacaoSituacao, string descricaoEspecifica = null, bool pendenciaUsuario = false)
        {
            IdIntencaoOperacao = idIntencaoOperacao;
            IdIntencaoOperacaoSituacao = idIntencaoOperacaoSituacao;
            DescricaoEspecifica = descricaoEspecifica;
            PendenciaUsuario = pendenciaUsuario;
        }
    }
}
