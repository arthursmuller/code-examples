namespace Aplicacao.Model.Banco
{
    public class ContaBancariaModel
    {
        public string Agencia { get; set; }
        public int IdBanco { get; set; }
        public string NumeroConta { get; set; }
        public int DigitoVerificadorAgencia { get; set; }
    }
}
