namespace Dominio
{
    public class ContaBancariaDominio : EntidadeBase
    {
        public string Agencia { get; set; }
        public string NumeroConta { get; set; }
        public int DigitoVerificadorAgencia { get; set; }
        public BancoDominio Banco { get; set; }
        public int? IdBanco { get; set; }

        public ContaBancariaDominio() { }
        public ContaBancariaDominio(string agencia, string numeroConta, int digitoVerificadorAgencia, int? idBanco)
        {
            Agencia = agencia;
            NumeroConta = numeroConta;
            DigitoVerificadorAgencia = digitoVerificadorAgencia;
            IdBanco = idBanco;
        }


        public void SetPropriedadesAtualizadas(string agencia, string numeroConta, int digitoVerificadorAgencia, int? idBanco)
        {
            Agencia = agencia;
            NumeroConta = numeroConta;
            DigitoVerificadorAgencia = digitoVerificadorAgencia;
            IdBanco = idBanco;
        }
    }
}
