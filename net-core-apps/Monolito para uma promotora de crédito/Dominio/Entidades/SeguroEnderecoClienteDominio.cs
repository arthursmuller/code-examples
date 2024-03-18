namespace Dominio
{
    public class SeguroEnderecoClienteDominio : EntidadeBase
    {
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public string Logradouro { get; set; }
        public int? Numero { get; private set; }
        public int? IdMunicipio { get; private set; }
        public MunicipioDominio Municipio { get; private set; }
        
        public SeguroEnderecoClienteDominio() { }

        public SeguroEnderecoClienteDominio(string bairro, string cep, string complemento, string logradouro, int? numero, int? idMunicipio)
        {
            Bairro = bairro;
            Cep = cep;
            Complemento = complemento;
            Logradouro = logradouro;
            Numero = numero;
            IdMunicipio = idMunicipio;
        }
    }
}
