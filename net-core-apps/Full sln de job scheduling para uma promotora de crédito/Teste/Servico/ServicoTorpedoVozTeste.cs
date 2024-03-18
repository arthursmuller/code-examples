using Fila.Model.TorpedoVoz;
using Aplicacao.Servico;
using Fila.Model.TorpedoVoz;
using Infraestrutura.Providers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Aplicacao.Servico;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;
using Dominio.Entidades;
using Infraestrutura.DTO.ZenviaTorpedoVoz;

namespace Teste.Servico
{
    public class ServicoTorpedoVozTeste : ServicoBase
    {
        private Mock<IProviderZenviaTotalVoice> _provedorTorpedoVozMock = new Mock<IProviderZenviaTotalVoice>();
        private IProviderZenviaTotalVoice _provedorTorpedoVoz;
        private TorpedoVozServico _servicoTorpedoVoz;
        public ServicoTorpedoVozTeste(): base()
        {
            _provedorTorpedoVoz = _provedorTorpedoVozMock.Object;
            _servicoTorpedoVoz = new TorpedoVozServico(_mensageria, _contexto, _provedorTorpedoVoz);
        }


        [Fact]
        public async void EnviarMensagem_TelefoneInvalido_DeveAtualizarTabelaComMensagemDeErro()
        {
            configurarFornecedorTest();

            var mensagemTorpedoVoz = new TorpedoVozRequisicaoMensagem
            {
                CodigoReferenciaMensagem = "0001",
                DDD = "051",
                Telefone = "997264683",
                Mensagem = "Olá, seu token é 12345",
                IdTorpedoVozFornecedor = 1
            };

            var torpedoVozServico = obterInstanciaServico();
            await torpedoVozServico.ProcessarRequisicao(mensagemTorpedoVoz);

            var mensagemGravada = await _contexto.TorpedoVozMensagens.AsNoTracking().FirstOrDefaultAsync();

            Assert.NotNull(mensagemGravada);
        }

        
        [Fact]
        public async void EnviarMensagem_DadosValidos_GravaDadosSemMensagemDeErroNaTabela()
        {
            configurarFornecedorTest();
            var torpedoVozServico = obterInstanciaServico();
            
            var mensagemTorpedoVoz = new TorpedoVozRequisicaoMensagem
            {
                CodigoReferenciaMensagem = "0001",
                DDD = "051",
                Telefone = "997264683",
                Mensagem = "Olá, seu token é 12345",
                IdTorpedoVozFornecedor = 1
            };

            var retorno = true;
            _provedorTorpedoVozMock
                .Setup(t => t.EnviarMensagemVoz(It.IsAny<ZenviaTorpedoVozDto>(), It.IsAny<string>()))
                .ReturnsAsync(retorno);
            _provedorTorpedoVoz = _provedorTorpedoVozMock.Object;

            await torpedoVozServico.ProcessarRequisicao(mensagemTorpedoVoz);

            var mensagemGravada = await _contexto.TorpedoVozMensagens.AsNoTracking().FirstOrDefaultAsync();

            Assert.NotNull(mensagemGravada);
        }

        private void configurarFornecedorTest(){

            EmpresaDominio empresa = new EmpresaDominio("Empresa Unit Tests - ltda");
            _contexto.Empresas.Add(empresa);
            _contexto.SaveChanges();

            TorpedoVozFornecedorDominio fornecedor = 
                        new TorpedoVozFornecedorDominio( "Zenvia the Test", empresa.ID );
            
            _contexto.TorpedoVozFornecedores.Add(fornecedor);
            _contexto.SaveChanges();

        }

        private TorpedoVozServico obterInstanciaServico()
            => _servicoTorpedoVoz;
    }
}
