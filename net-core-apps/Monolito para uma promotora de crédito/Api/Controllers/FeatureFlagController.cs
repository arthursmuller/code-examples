using Aplicacao.Model.FeatureFlags;
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
    [Route("feature-flags")]
    [ExcludeFromCodeCoverage]
    public class FeatureFlagController : BaseController
    {
        private readonly FeatureFlagServico _featureFlagServico;

        public FeatureFlagController(FeatureFlagServico featureFlagServico, IBemMensagens mensagens) : base(mensagens)
        {
            _featureFlagServico = featureFlagServico;
        }

        [HttpGet()]
        public async Task<RetornoApi<IEnumerable<FeatureFlagModel>>> Get()
        {
            var flags = await _featureFlagServico.ConsultarChaves();

            return FormatarRetorno(flags);
        }

        [HttpPost()]
        [Authorize(Roles = "admin")]
        public async Task<RetornoApi<FeatureFlagModel>> Post([FromBody] FeatureFlagModel requisicao)
        {
            var flag = await _featureFlagServico.AdicionarOuAtualizar(requisicao);

            return FormatarRetorno(flag);
        }
    }
}
