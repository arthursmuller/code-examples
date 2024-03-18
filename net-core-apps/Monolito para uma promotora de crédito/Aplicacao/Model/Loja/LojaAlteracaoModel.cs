using Aplicacao.Model.Loja;
using System.Collections.Generic;

namespace Aplicacao
{
    public class LojaAlteracaoModel
    {
        public string Nome { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Endereco { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Bairro { get; set; }

        public string Cep { get; set; }

        public string MensagemApresentacao { get; set; }

        public List<TelefoneLojaAlteracaoModel> Telefones { get; set; }
    }
}
