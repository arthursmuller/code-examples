using Aplicacao;
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
    [Route("/cliente/documentos")]
    [ExcludeFromCodeCoverage]
    public class TipoDocumentoController : BaseController
    {
        private readonly TipoDocumentoServico _tipoDocumentoServico;

        public TipoDocumentoController(TipoDocumentoServico tipoDocumentoServico, IBemMensagens mensagens) : base(mensagens)
            => _tipoDocumentoServico = tipoDocumentoServico;

        [HttpGet("identificacao-pessoal")]
        public async Task<RetornoApi<IEnumerable<TipoDocumentoModel>>> Get()
            => FormatarRetorno(await _tipoDocumentoServico.ListarIdentificaoPessoal());
    }
}
