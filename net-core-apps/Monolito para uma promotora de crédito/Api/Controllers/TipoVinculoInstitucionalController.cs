using Aplicacao.Model.TipoVinculoInstitucional;
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
    [ExcludeFromCodeCoverage]
    public class TipoVinculoInstitucionalController : BaseController
    {
        private readonly TipoVinculoInstitucionalServico _tipoVinculoInstitucionalServico;

        public TipoVinculoInstitucionalController(TipoVinculoInstitucionalServico tipoVinculoInstitucional, IBemMensagens mensagens) : base(mensagens)
            => _tipoVinculoInstitucionalServico = tipoVinculoInstitucional;

        [HttpGet("tipos-vinculo-institucional")]
        public async Task<RetornoApi<IEnumerable<TipoVinculoInstitucionalModel>>> Get()
        {
            var tiposVinculoInstitucional = await _tipoVinculoInstitucionalServico.ListarTipoVinculoInstitucional();

            return FormatarRetorno(tiposVinculoInstitucional);
        }
    }
}
