namespace Aplicacao.Model.Beneficio
{
    public class ObtencaoAutorizacaoConsultaBeneficioModel
    {
        public string ChaveAutorizacao { get; set; }

        public bool PossuiAutorizacaoValida { get => !string.IsNullOrWhiteSpace(ChaveAutorizacao); }

        public ObtencaoAutorizacaoConsultaBeneficioModel(string chaveAutorizacao)
            => ChaveAutorizacao = chaveAutorizacao;
    }
}
