using Aplicacao.Model.Produto;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class ProdutoServico
    {
        private readonly IBemMensagens _mensagens;
        private readonly PlataformaClienteContexto _contexto;

        public ProdutoServico(IBemMensagens mensagens, PlataformaClienteContexto contexto)
        {
            _mensagens = mensagens;
            _contexto = contexto;
        }

        public async Task<IEnumerable<ProdutoModel>> ListarProdutos()
        {
            var produtos = await _contexto.Produtos
                .AsNoTracking()
                .OrderBy(p => p.Nome)
                .ToListAsync();

            if (produtos == null)
            {
                _mensagens.AdicionarErro(Mensagens.Produto_NenhumEncontrado, EnumMensagemTipo.banco);

                return null;
            }

            return converterProdutosParaModel(produtos);
        }

        public async Task<ProdutoModel> AtualizarProduto(ProdutoAtualizacaoModel produtoAtualizacao, int id)
        {
            var produto = await _contexto.Produtos.FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (produto == null)
            {
                _mensagens.AdicionarErro(string.Format(Mensagens.Produto_IdNaoEncontrado, id), EnumMensagemTipo.banco);

                return new ProdutoModel();
            }

            produto.SetPropriedadesAtualizadas(produtoAtualizacao.Nome, produtoAtualizacao.Sigla, produtoAtualizacao.RequerConvenio);

            await _contexto.SaveChangesAsync();

            return converterParaModel(produto);
        }

        private static IEnumerable<ProdutoModel> converterProdutosParaModel(List<ProdutoDominio> produtos)
        {
            return produtos.Select(x => converterParaModel(x));
        }

        public static ProdutoModel converterParaModel(ProdutoDominio produto)
        {
            return new ProdutoModel
            {
                ID = (int)produto.ID,
                Nome = produto.Nome,
                Sigla = produto.Sigla,
                RequerConvenio = produto.RequerConvenio,
            };
        }
    }
}
