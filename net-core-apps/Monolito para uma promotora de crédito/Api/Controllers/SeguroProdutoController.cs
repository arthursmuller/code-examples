using Aplicacao.Model.SeguroProduto;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("/clientes/autenticado/seguros/produtos")]
    [ExcludeFromCodeCoverage]
    public class SeguroProdutoController : BaseController
    {
        private readonly ISeguroProdutoServico _seguroProdutoService;

        public SeguroProdutoController(ISeguroProdutoServico seguroProdutoService, IBemMensagens mensagens) : base(mensagens)
            => _seguroProdutoService = seguroProdutoService;

        [HttpGet]
        public async Task<RetornoApi<IEnumerable<SeguroProdutoModel>>> Get()
            => FormatarRetorno(await _seguroProdutoService.Listar());
    }
}
