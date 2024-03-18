using Aplicacao.Model.Documento;
using Aplicacao.Model.SeguroProposta;
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
    [Route("/clientes/autenticado/seguros/propostas")]
    [ExcludeFromCodeCoverage]
    [Authorize]

    public class SeguroPropostaController : BaseController
    {
        private readonly ISeguroPropostaServico _servico;

        public SeguroPropostaController(ISeguroPropostaServico servico, IBemMensagens mensagens) : base(mensagens)
            => _servico = servico;

        [HttpGet]
        [AllowAnonymous]
        public async Task<RetornoApi<SeguroPropostaExibicaoModel>> Get()
            => FormatarRetorno(await _servico.Listar());

        [HttpGet("link-pagamento")]
        public async Task<RetornoApi<string>> Consultarlink()
            => FormatarRetorno(await _servico.ConsultarLinkPagamento());

        [HttpGet("meio-pagamentos")]
        public async Task<RetornoApi<IEnumerable<MeioPagamentoExibicaoModel>>> ConsultarMeioPagamentos()
            => FormatarRetorno(await _servico.ListarMeioPagamentos());

        [HttpPost]
        public async Task<RetornoApi<string>> Criar([FromBody] CriarSeguroPropostaModel model)
            => FormatarRetorno(await _servico.Criar(model));

        [HttpGet("enviar-proposta")]
        public async Task<RetornoApi<bool>> EnviarPropostaIcatu()
            => FormatarRetorno(await _servico.EnviarPropostaIcatu());

        [HttpGet("baixar-termo")]
        public async Task<RetornoApi<DocumentoModel>> BaixarTermo()
            => FormatarRetorno(await _servico.BaixarTermo());
    }
}
