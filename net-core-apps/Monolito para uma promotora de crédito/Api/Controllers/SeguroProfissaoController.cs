using Aplicacao.Model.SeguroProfissao;
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
    public class SeguroProfissaoController : BaseController
    {
        private readonly SeguroProfissaoServico _servico;

        public SeguroProfissaoController(SeguroProfissaoServico seguroProdutoService, IBemMensagens mensagens) : base(mensagens)
            => _servico = seguroProdutoService;

        [HttpGet("/clientes/autenticado/seguros/profissoes")]
        public async Task<RetornoApi<IEnumerable<SeguroProfissaoExibicaoModel>>> Get() => 
            FormatarRetorno(await _servico.Listar());

        [Authorize(Roles = "admin")]
        [HttpPost("/administrador/autenticado/seguro/profissao")]

        public async Task<RetornoApi<IEnumerable<SeguroProfissaoExibicaoModel>>> Adicionar(IEnumerable<CriarSeguroProfissaoModel> seguroProfissoes) =>
            FormatarRetorno(await _servico.Adicionar(seguroProfissoes));

        [Authorize(Roles = "admin")]
        [HttpPost("/administrador/autenticado/seguro/profissao/icatu")]
        public async Task<RetornoApi<bool>> AdicionarIcatu(IEnumerable<CriarSeguroProfissaoIcatuModel> seguroProfissoes) =>
            FormatarRetorno(await _servico.Adicionar(seguroProfissoes));
    }
}
