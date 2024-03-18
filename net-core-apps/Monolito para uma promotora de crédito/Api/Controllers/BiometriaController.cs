using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using B.Web.Controllers;
using B.Mensagens.Interfaces;
using B.Models;
using System.Threading.Tasks;
using Aplicacao.Model.Biometria;
using Aplicacao.Servico;
using System.Diagnostics.CodeAnalysis;

namespace Api.Controllers
{
    [Route("/clientes/autenticado/biometria")]
    [ExcludeFromCodeCoverage]
    [Authorize]
    public class BiometriaController : BaseController
    {
        private readonly IBiometriaServico _biometriaServico;
        public BiometriaController(IBemMensagens mensageria, IBiometriaServico biometria) : base(mensageria)
        {
            _biometriaServico = biometria;
        }

        [HttpGet]
        [Authorize]
        public async Task<RetornoApi<BiometriaConsultaModel>> ObterSituacaoBiometria()
        {
            var biometria = await _biometriaServico.ObterSituacaoBiometria();
            return FormatarRetorno(biometria);
        }

        [HttpPost]
        [Authorize]
        public async Task<RetornoApi<bool>> Post()
        {
            var biometria = await _biometriaServico.ExecutarBiometria();
            return FormatarRetorno(biometria);
        }

        [HttpPost("webhook")]
        public async Task<RetornoApi<bool>> ProcessarRetornoBiometriaUnico([FromBody] BiometriaWebhookRetornoUnicoModel retorno)
        {
            var webhook = await _biometriaServico.ProcessarRetornoWebhookUnico(retorno);
            return FormatarRetorno(webhook);
        }


    }
}
