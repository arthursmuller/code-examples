using Fila.Model.WhatsApp;
using Aplicacao.Servico;
using B.WhatsApp;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;
using Dominio.Entidades;

namespace Teste.Servico
{
    public class ServicoWhatsAppTeste : ServicoBase
    {
        private Mock<IProvedorWhatsApp> _provedorWhatsAppMock = new Mock<IProvedorWhatsApp>();
        private IProvedorWhatsApp _provedorWhatsApp;

        public ServicoWhatsAppTeste(){
            _provedorWhatsApp = _provedorWhatsAppMock.Object;
            configurarFornecedorTest();
        }

        [Fact]
        public async void EnviarMensagem_TelefoneInvalido_DeveAtualizarTabelaComMensagemDeErro()
        {
            var mensagem = new Dictionary<string, string>();
            mensagem.Add("Key", "Value");

            var mensagemWhatsApp = new WhatsAppRequisicaoMensagem
            {
                CodigoReferenciaMensagem = "APIxxxxx1",
                IdTemplate = new Guid(),
                DDD = "xxx",
                Telefone = "997264683",
                Mensagem = mensagem,
                IdWhatsAppFornecedor = 1
            };

            var whatsAppServico = obterInstanciaServico();
            await whatsAppServico.EnviarMensagem(mensagemWhatsApp);
            var mensagemGravada = await _contexto.WhatsappMensagens.AsNoTracking().FirstOrDefaultAsync();

            Assert.NotNull(mensagemGravada);
            Assert.NotNull(mensagemGravada.MensagemRetornoErro);
            Assert.Equal(string.Join(";", _mensageria.BuscarErros()?.Select(s => s.Mensagem)), mensagemGravada.MensagemRetornoErro);
        }

        [Fact]
        public async void EnviarMensagem_ProvocandoException_DeveAtualizarTabelaComMensagemDeErro()
        {
            var mensagem = new Dictionary<string, string>();
            mensagem.Add("Key", "Value");

            var mensagemWhatsApp = new WhatsAppRequisicaoMensagem
            {
                IdTemplate = new Guid(),
                DDD = "051",
                Telefone = "997264683",
                Mensagem = mensagem,
                IdWhatsAppFornecedor = 1
            };

            _provedorWhatsApp = null;
            var whatsAppServico = obterInstanciaServico();
            await whatsAppServico.EnviarMensagem(mensagemWhatsApp);

            var mensagemGravada = await _contexto.WhatsappMensagens.AsNoTracking().FirstOrDefaultAsync();

            Assert.NotNull(mensagemGravada);
            Assert.NotNull(mensagemGravada.MensagemRetornoErro);
            Assert.NotEqual(string.Join(";", _mensageria.BuscarErros()?.Select(s => s.Mensagem)), mensagemGravada.MensagemRetornoErro);
        }

        [Fact]
        public async void EnviarMensagem_DadosValidos_GravaDadosSemMensagemDeErroNaTabela()
        {
            configurarFornecedorTest();
            var whatsAppServico = obterInstanciaServico();

            var mensagem = new Dictionary<string, string>();
            mensagem.Add("Key1", "Value1");
            mensagem.Add("Key2", "Value2");

            var mensagemEnvio = new WhatsAppRequisicaoMensagem
            {
                IdTemplate = new Guid(),
                DDD = "051",
                Telefone = "997264683",
                Mensagem = mensagem,
                IdWhatsAppFornecedor = 1
            };


            await whatsAppServico.EnviarMensagem(mensagemEnvio);

            var mensagemGravada = await _contexto.WhatsappMensagens.AsNoTracking().FirstOrDefaultAsync();

            Assert.NotNull(mensagemGravada);
            Assert.Null(mensagemGravada.MensagemRetornoErro);
            Assert.Equal(mensagemEnvio.IdTemplate, mensagemGravada.IdTemplate);
            Assert.Equal(JsonSerializer.Serialize(mensagem), mensagemGravada.MensagemEnvio);
        }

        private WhatsAppServico obterInstanciaServico()
            => new WhatsAppServico(_mensageria, _contexto, _provedorWhatsApp);
        private void configurarFornecedorTest(){

            EmpresaDominio empresa = new EmpresaDominio("Empresa Unit Tests - ltda");
            _contexto.Empresas.Add(empresa);
            _contexto.SaveChanges();

            WhatsappFornecedorDominio fornecedor = 
                        new WhatsappFornecedorDominio( "Zenvia the Test", empresa.ID );
            fornecedor.defineChave("XXXXXXXXXXXXXXXX");
            _contexto.WhatsappFornecedores.Add(fornecedor);
            _contexto.SaveChanges();

        }
    }
}
