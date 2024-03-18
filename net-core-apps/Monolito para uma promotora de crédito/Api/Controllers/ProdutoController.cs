using Aplicacao.Model.Produto;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ExcludeFromCodeCoverage]
    public class ProdutoController : BaseController
    {
        private readonly ProdutoServico _produtoServico;

        public ProdutoController(ProdutoServico produtoServico, IBemMensagens mensagens) : base(mensagens)
            => _produtoServico = produtoServico;

        [HttpGet("produtos")]
        public async Task<RetornoApi<IEnumerable<ProdutoModel>>> Get() =>
            FormatarRetorno(await _produtoServico.ListarProdutos());

        [HttpPut("produtos/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<RetornoApi<ProdutoModel>> Put([FromBody] ProdutoAtualizacaoModel produto, int id) => 
            FormatarRetorno(await _produtoServico.AtualizarProduto(produto, id));
    }
}
