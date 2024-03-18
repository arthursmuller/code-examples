using Dominio.Enum;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;

namespace Dominio
{
    public class LeadDominio : EntidadeBase
    {
        public string CPF { get; private set; }
        public string Nome { get; private set; }

        private string telefone;
        public string Telefone { get => telefone; private set => telefone = Fone.DesmascararTelefone(value); }

        private string celular;
        public string Celular { get => celular; private set => celular = Fone.DesmascararTelefone(value); }

        public string Email { get; private set; }
        public Produto? IdProduto { get; private set; }
        public Convenio? IdConvenio { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string OrigemRequisicaoPalavraChave { get; private set; }
        public string OrigemRequisicaoMidia { get; private set; }
        public string OrigemRequisicaoConteudo { get; private set; }
        public string OrigemRequisicaoTermo { get; private set; }
        public string OrigemRequisicaoCampanha { get; private set; }
        public int? IdLoja { get; private set; }
        public bool DesejaContatoWhatsApp { get; private set; }
        public string LinkContatoWhatsAppLoja { get; private set; }
        public bool Quitacao { get; private set; }

        public IEnumerable<IntencaoOperacaoDominio> IntencoesOperacao { get; private set; }
        public ConvenioDominio Convenio { get; private set; }
        public LojaDominio Loja { get; private set; }
        public ProdutoDominio Produto { get; private set; }

        public LeadDominio() { }

        public LeadDominio(
            CPF cpf,
            string nome,
            string telefone,
            string celular,
            string email,
            Produto? idProduto,
            Convenio? idConvenio,
            double latitude,
            double longitude,
            string origemRequisicaoPalavraChave,
            string origemRequisicaoMidia,
            string origemRequisicaoConteudo,
            string origemRequisicaoTermo,
            string origemRequisicaoCampanha,
            int? idLoja,
            bool desejaContatoWhatsApp,
            string linkContatoWhatsAppLoja,
            bool quitacao = false
        )
        {
            CPF = cpf.ToString();
            Nome = nome;
            Telefone = telefone;
            Celular = celular;
            Email = email?.ToLower();
            IdProduto = idProduto;
            IdConvenio = idConvenio;
            Latitude = latitude;
            Longitude = longitude;
            OrigemRequisicaoPalavraChave = origemRequisicaoPalavraChave;
            OrigemRequisicaoMidia = origemRequisicaoMidia;
            OrigemRequisicaoConteudo = origemRequisicaoConteudo;
            OrigemRequisicaoTermo = origemRequisicaoTermo;
            OrigemRequisicaoCampanha = origemRequisicaoCampanha;
            IdLoja = idLoja;
            DesejaContatoWhatsApp = desejaContatoWhatsApp;
            LinkContatoWhatsAppLoja = linkContatoWhatsAppLoja;
            DataAtualizacao = DateTime.Now;
            Quitacao = quitacao;
        }

        public void SetPropriedadesAtualizadas(CPF cpf, string nome, string telefone, string celular, string email, Produto? idProduto, Convenio? idConvenio, int? idLoja, bool desejaContatoWhatsApp, string linkContatoWhatsAppLoja)
        {
            CPF = cpf.ToString();
            Nome = nome;
            Telefone = telefone;
            Celular = celular;
            Email = email?.ToLower();
            IdProduto = idProduto;
            IdConvenio = idConvenio;
            IdLoja = idLoja;
            DesejaContatoWhatsApp = desejaContatoWhatsApp;
            LinkContatoWhatsAppLoja = linkContatoWhatsAppLoja;
            DataAtualizacao = DateTime.Now;
        }

        public void SetInformacoesLoja(int? idLoja, string linkContatoWhatsAppLoja)
        {
            IdLoja = idLoja;
            LinkContatoWhatsAppLoja = linkContatoWhatsAppLoja;
            DataAtualizacao = DateTime.Now;
        }

        public void SetConvenio(Convenio convenio)
        {
            IdConvenio = convenio;
        }
    }
}