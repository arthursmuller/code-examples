using Aplicacao.Model.IntencaoOperacao;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.ValueObjects.v2;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class IntencaoOperacaoController : BaseController
    {
        private readonly IntencaoOperacaoServico _intencaoOperacaoServico;

        public IntencaoOperacaoController(IntencaoOperacaoServico intencaoOperacaoServico, IBemMensagens mensagens) : base(mensagens)
            => _intencaoOperacaoServico = intencaoOperacaoServico;

        [HttpGet("clientes/autenticado/intencoes-operacao/{id}")]
        public async Task<RetornoApi<IntencaoOperacaoModel>> Get(int id)
        {
            var intencaoOperacao = await _intencaoOperacaoServico.BuscarIntencaoOperacaoAutenticado(id);

            return FormatarRetorno(intencaoOperacao);
        }

        [HttpGet("clientes/autenticado/intencoes-operacao")]
        public async Task<RetornoApi<IEnumerable<IntencaoOperacaoModel>>> Get()
        {
            var intencoesOperacao = await _intencaoOperacaoServico.ListarIntencoesOperacaoAutenticado();

            return FormatarRetorno(intencoesOperacao);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("intencoes-operacao")]
        public async Task<RetornoApi<IEnumerable<IntencaoOperacaoModel>>> ListarTodasIntencoesOperacao([FromQuery] IntencaoOperacaoConsultaModel consulta)
        {
            var intencoesOperacao = await _intencaoOperacaoServico.ListarIntencoesOperacao(consulta);

            return FormatarRetorno(intencoesOperacao);
        }

        [HttpGet("clientes/{cpf}/intencoes-operacao")]
        [Authorize(Roles = "admin")]
        public async Task<RetornoApi<IEnumerable<IntencaoOperacaoModel>>> Get([FromRoute] string cpf)
        {
            var intencoesOperacao = await _intencaoOperacaoServico.ListarIntencoesOperacao(new CPF(cpf));

            return FormatarRetorno(intencoesOperacao);
        }

        [HttpPost("intencoes-operacao")]
        public async Task<RetornoApi<IntencaoOperacaoNovoModel>> Post([FromBody] IntencaoOperacaoCriacaoModel intencaoOperacao)
        {
            var intencaoOperacaoNova = await _intencaoOperacaoServico.GravarIntencaoOperacao(intencaoOperacao);

            return FormatarRetorno(intencaoOperacaoNova);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("intencoes-operacao/{id}")]
        public async Task<RetornoApi<bool>> Put([FromBody] IntencaoOperacaoAtualizacaoModel intencaoOperacao, int id)
        {
            var intencaoOperacaoAtualizada = await _intencaoOperacaoServico.AtualizarIntencaoOperacao(intencaoOperacao, id);

            return FormatarRetorno(intencaoOperacaoAtualizada);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("intencoes-operacao/{id}/atender")]
        public async Task<RetornoApi<bool>> Atender([FromBody] IntencaoOperacaoAtendimentoModel intencaoOperacao, int id)
        {
            var intencaoOperacaoAtualizada = await _intencaoOperacaoServico.AtenderIntencaoOperacao(intencaoOperacao, id);

            return FormatarRetorno(intencaoOperacaoAtualizada);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("intencoes-operacao/{id}/prosseguir")]
        public async Task<RetornoApi<bool>> Prosseguir(int id, [FromBody] IntencaoOperacaoProsseguirSituacaoModel requisicao)
        {
            var intencaoOperacaoAtualizada = await _intencaoOperacaoServico.ProsseguirIntencaoOperacao(id, requisicao);

            return FormatarRetorno(intencaoOperacaoAtualizada);
        }
    }
}
