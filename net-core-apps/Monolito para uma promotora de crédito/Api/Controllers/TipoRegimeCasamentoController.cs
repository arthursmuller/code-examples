using Aplicacao.Model.TipoRegimeCasamento;
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
    [Route("/cliente/regime-casamento")]
    [ExcludeFromCodeCoverage]
    public class TipoRegimeCasamentoController : BaseController
    {
        private readonly TipoRegimeCasamentoServico _tipoRegimeCasamentoServico;

        public TipoRegimeCasamentoController(TipoRegimeCasamentoServico tipoRegimeCasamentoServico, IBemMensagens mensagens) : base(mensagens)
            => _tipoRegimeCasamentoServico = tipoRegimeCasamentoServico;

        [HttpGet]
        public async Task<RetornoApi<IEnumerable<TipoRegimeCasamentoModel>>> Get()
            => FormatarRetorno(await _tipoRegimeCasamentoServico.Listar());
    }
}
