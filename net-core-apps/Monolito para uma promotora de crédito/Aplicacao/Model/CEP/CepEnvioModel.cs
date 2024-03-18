namespace Aplicacao.CEP
{
    public class CepEnvioModel
    {
        public int IdUnidadeFederativa { get; set; }

        public int IdMunicipio { get; set; }

        public string Bairro { get; set; }

        public int? IdTipoLogradouro { get; set; }

        public string Logradouro { get; set; }
    }
}
