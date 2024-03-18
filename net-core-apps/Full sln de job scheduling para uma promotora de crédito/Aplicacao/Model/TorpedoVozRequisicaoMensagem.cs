namespace Fila.Model.TorpedoVoz
{
    public class TorpedoVozRequisicaoMensagem
    {
        public string CodigoReferenciaMensagem { get; set; }
        public string DDD { get; set; }
        public string Telefone { get; set; }
        public string Mensagem { get; set; }
        public int IdTorpedoVozFornecedor { get; set; }
    }
}