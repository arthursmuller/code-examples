using Aplicacao.Model.EnderecoCliente;
using Aplicacao.Servico;
using Dominio;
using Dominio.Enum;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class SeguroCoberturaServicoTeste : ServicoTesteBase
    {
        private readonly SeguroCoberturaServico _seguroCoberturaServico;
        private UsuarioDominio _usuarioTeste;

        public SeguroCoberturaServicoTeste() : base()
        {
            criarDadosRelacionamentos();

            _usuarioTeste = CriarUsuarioTeste();

            _seguroCoberturaServico = new SeguroCoberturaServico(_contexto, _usuarioLogin, _mensagens);
        }

        [Fact]
        public async Task GravarEndereco_SendoNovoPrincipal_DevePersistirComoUnicoPrincipal()
        {
            criarCoberturas();
            
            var resultado = await _seguroCoberturaServico.ListarSeguros();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado.Any());
        }

        private List<SeguroCoberturaDominio> criarCoberturas()
        {
            var coberturas = new List<SeguroCoberturaDominio>()
            {
                new SeguroCoberturaDominio(1, 'c', 21, 21, 'c', 1),
                new SeguroCoberturaDominio(2, 'b', 41, 41, 'b', 2),
            };

            _contexto.SeguroCobertura.AddRange(coberturas);
            SaveChanges();

            return coberturas;
        }

        private void criarDadosRelacionamentos()
        {
            _contexto.Produtos.AddRange(new[] {
                new ProdutoDominio(Produto.CartaoCreditoConsignado, "testep", "ss", false),
                new ProdutoDominio(Produto.CreditoConsignado, "testeste", "tt", false),
            });
            SaveChanges();

            _contexto.SeguroProduto.AddRange(new[] {
                new SeguroProdutoDominio("TETESTE", "TESTE", DateTime.Now, DateTime.Now, Produto.CartaoCreditoConsignado, 1),
                new SeguroProdutoDominio("TETESTE222", "TESTE222", DateTime.Now, DateTime.Now, Produto.CreditoConsignado, 1),
            });

            SaveChanges();
        }
    }
}
