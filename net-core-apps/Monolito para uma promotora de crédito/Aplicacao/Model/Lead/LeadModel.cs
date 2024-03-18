using Aplicacao.Model.Convenio;
using System;
using System.ComponentModel.DataAnnotations;

namespace Aplicacao
{
    public class LeadModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public ConvenioModel Convenio { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string OrigemRequisicaoPalavraChave { get; set; }

        public string OrigemRequisicaoMidia { get; set; }

        public string OrigemRequisicaoConteudo { get; set; }

        public string OrigemRequisicaoTermo { get; set; }

        public string OrigemRequisicaoCampanha { get; set; }

        public LojaModel Loja { get; set; }

        public bool DesejaContatoWhatsApp { get; set; }

        public string LinkContatoWhatsAppLoja { get; set; }

        public DateTime DataAtualizacao { get; set; }
    }
}