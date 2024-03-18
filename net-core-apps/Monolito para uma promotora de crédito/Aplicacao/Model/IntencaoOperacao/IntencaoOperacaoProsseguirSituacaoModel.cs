namespace Aplicacao.Model.IntencaoOperacao
{
    public class IntencaoOperacaoProsseguirSituacaoModel
    {
        public bool ProsseguirParaPassoExcecao { get; set; } = false;

        public int? IdSituacaoExcepcional { get; set; }

        public bool PendenciaUsuario { get; set; } = false;
        
        public string DescricaoEspecifica { get; set; }
    }
}
