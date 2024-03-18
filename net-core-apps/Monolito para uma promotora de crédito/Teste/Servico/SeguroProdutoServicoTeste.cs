using Aplicacao.Interfaces;
using Aplicacao.Model.EnderecoCliente;
using Aplicacao.Servico;
using B.Configuracao;
using Dominio;
using Dominio.Enum;
using Infraestrutura.Providers.IcatuApi;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class SeguroProdutoServicoTeste : ServicoTesteBase
    {
        private readonly SeguroProdutoServico _seguroProdutoServico;
        private UsuarioDominio _usuarioTeste;
        private ILogger<ISeguroPropostaServico> _loggerMock = new Mock<ILogger<ISeguroPropostaServico>>().Object;
        
        public SeguroProdutoServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();
            
            criarDadosRelacionamentos();

            var icatuApiServicoMock = new Mock<IProviderIcatu>();
            var documentoServico = new Mock<IDocumentoServico>();
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("ambiente-frontend-url", "teste")
            }, null, null);

            _seguroProdutoServico = new SeguroProdutoServico(
                _contexto, 
                _usuarioLogin, 
                _mensagens, 
                new SeguroPropostaServico(_contexto, _usuarioLogin, _mensagens, configuracao, _loggerMock, icatuApiServicoMock.Object, documentoServico.Object)
            );
        }

        [Fact]
        public async Task Listar_Produto_Deve_Retornar_Produto_Da_Cobertura_Do_UsuarioLogado()
        {
            var resultado = await _seguroProdutoServico.Listar();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado.Count() == 1);
        }

        [Fact]
        public async Task Listar_Produto_Deve_Retornar_Todos_Produtos()
        {
            var proposta = _contexto.SeguroProposta.FirstOrDefault(s => s.Cliente.ID == _usuarioTeste.Cliente.ID);
            _contexto.SeguroProposta.Remove(proposta);
            await SaveChangesAsync();

            var resultado = await _seguroProdutoServico.Listar();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado.Count() > 1);
        }

        private void criarDadosRelacionamentos()
        {
            _contexto.Clientes.AddRange(new[] {
                new ClienteDominio("test"),
                new ClienteDominio("test2")
            });
            SaveChanges();

            _contexto.Produtos.AddRange(new[] {
                new ProdutoDominio(Produto.CartaoCreditoConsignado, "testep", "ss", false),
                new ProdutoDominio(Produto.CreditoConsignado, "testeste", "tt", false),
            });
            SaveChanges();

            _contexto.SeguroProduto.AddRange(new[] {
                new SeguroProdutoDominio("TETESTE", "TESTE", DateTime.Now, DateTime.Now, Produto.CartaoCreditoConsignado, 1),
                new SeguroProdutoDominio("TETESTE222", "TESTE222", DateTime.Now, DateTime.Now, Produto.CreditoConsignado, 2),
            });
            SaveChanges();

            var propostas = new List<SeguroPropostaDominio>()
            {
                new SeguroPropostaDominio(12, false, 1, _usuarioTeste.Cliente.ID, MeioPagamentoSeguro.CartaoCredito),
                new SeguroPropostaDominio(22, false, 2, 2, MeioPagamentoSeguro.CartaoCredito)
            };

            _contexto.SeguroProposta.AddRange(propostas);
            SaveChanges();

            var coberturas = new List<SeguroCoberturaDominio>()
            {
                new SeguroCoberturaDominio(1, 'c', 21, 21, 'c', 1),
                new SeguroCoberturaDominio(2, 'b', 41, 41, 'b', 2),
            };

            _contexto.SeguroCobertura.AddRange(coberturas);
            SaveChanges();
        }
    }
}
