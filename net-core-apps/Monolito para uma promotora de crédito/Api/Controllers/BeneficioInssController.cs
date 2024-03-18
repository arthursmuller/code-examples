using Aplicacao.Comando;
using Aplicacao.Consulta;
using Aplicacao.Model.Beneficio;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Dominio.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("cliente/autenticado/beneficios-inss")]
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class BeneficioInssController : BaseController
    {
        private readonly BeneficioInssQuery _beneficioInssQuery;
        private readonly BeneficioInssAutorizacaoVigenteQuery _autorizacaoVigenteQuery;
        private readonly BeneficioInssReenvioTokenCommand _reenvioTokenCommand;
        private readonly BeneficioInssSolicitacaoAutorizacaoCommand _solicitacaoAutorizacaoCommand;
        private readonly BeneficioInssValidacaoTokenCommand _validacaoTokenCommand;

        public BeneficioInssController(IBemMensagens mensagens, BeneficioInssQuery beneficioInssQuery, BeneficioInssAutorizacaoVigenteQuery autorizacaoVigenteQuery,
            BeneficioInssReenvioTokenCommand reenvioTokenCommand, BeneficioInssSolicitacaoAutorizacaoCommand solicitacaoAutorizacaoCommand,
            BeneficioInssValidacaoTokenCommand validacaoTokenCommand) : base(mensagens)
        {
            _beneficioInssQuery = beneficioInssQuery;
            _autorizacaoVigenteQuery = autorizacaoVigenteQuery;
            _reenvioTokenCommand = reenvioTokenCommand;
            _solicitacaoAutorizacaoCommand = solicitacaoAutorizacaoCommand;
            _validacaoTokenCommand = validacaoTokenCommand;
        }

        [HttpGet("{chaveAutorizacao}")]
        public async Task<RetornoApi<IEnumerable<ConsultaBeneficioModel>>> Get(string chaveAutorizacao)
        {
            var beneficios = await _beneficioInssQuery.ConsultarBeneficiosInss(chaveAutorizacao);
            return FormatarRetorno(beneficios);
        }

        [HttpGet("reenvio-token")]
        public async Task<RetornoApi<bool>> ReenviarTokenParaAssinatura([FromQuery] SolicitacaoReenvioTokenAssinaturaModel parametrosSolicitacao)
        {
            var solicitacaoReenvio = await _reenvioTokenCommand.ReenviarTokenParaAssinatura(parametrosSolicitacao);
            return FormatarRetorno(solicitacaoReenvio);
        }

        [HttpGet("autorizacoes")]
        public async Task<RetornoApi<ObtencaoAutorizacaoConsultaBeneficioModel>> ObterAutorizacaoVigente()
        {
            var autorizacaoVigente = await _autorizacaoVigenteQuery.ObterAutorizacaoVigente();
            return FormatarRetorno(autorizacaoVigente);
        }

        [HttpPost("autorizacoes")]
        public async Task<RetornoApi<SolicitacaoAutorizacaoConsultaBeneficioModel>> SolicitarAutorizacaoConsultaBeneficioInss([FromBody] SolicitacaoAutorizacaoConsultaBeneficioEnvioModel parametrosSolicitacao)
        {
            var solicitacaoAutorizacao = await _solicitacaoAutorizacaoCommand.SolicitarAutorizacaoConsultaBeneficioInss(parametrosSolicitacao);
            return FormatarRetorno(solicitacaoAutorizacao);
        }

        [HttpPost("validacao-token")]
        public async Task<RetornoApi<ValidacaoTokenAssinaturaModel>> SolicitarAutorizacaoConsultaBeneficioInss([FromBody] ValidacaoTokenAssinaturaEnvioModel parametrosValidacao)
        {
            var validacaoToken = await _validacaoTokenCommand.ValidarTokenAssinatura(parametrosValidacao);
            return FormatarRetorno(validacaoToken);
        }
    }
}
