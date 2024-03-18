namespace Aplicacao.Model.ContaCliente
{
    public class ContaClienteModel
    {
        public int? IdContaCliente { get; set; }
        public int IdBanco { get; set; }
        public int IdTipoConta { get; set; }
        public int? IdFormaRecebimento { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
    }
}
