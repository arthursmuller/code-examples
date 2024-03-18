using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.Model.SeguroEndereco
{
    public class SeguroEnderecoModel
    {
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public string Logradouro { get; set; }
        public bool Principal { get; set; }
        public int Numero { get; set; }
        public int? IdMunicipio { get; set; }
        public int? IdTipoLogradouro { get; set; }
    }
}
