using Aplicacao.Model.Consignado;
using Aplicacao.Model.Produto;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Infraestrutura.Providers.Consignado.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("produtos/consignado")]
    [ExcludeFromCodeCoverage]
    public class ConsignadoController : BaseController
    {
        private readonly IConsignadoServico _consignadoServico;

        public ConsignadoController(IConsignadoServico consignadoServico, IBemMensagens mensagens) : base(mensagens)
            => _consignadoServico = consignadoServico;

        [Authorize]
        [HttpGet("simulacao-novo")]
        public async Task<RetornoApi<IEnumerable<RetornoSimulacaoDto>>> SimularNovo([FromQuery] SimulacaoNovoEnvioModel parametros)
        {
            var simulacao = await _consignadoServico.SimularNovo(parametros);

            return FormatarRetorno(simulacao);
        }

        [HttpGet("informacoes-produto-operacao/novo")]
        public RetornoApi<InformacaoProdutoOperacaoModel> ConsultarInformacoesProdutoOperacao()
        {
            return FormatarRetorno(new InformacaoProdutoOperacaoModel()
            {
                IdProduto = (int)Dominio.Enum.Produto.CreditoConsignado,
                IdTipoOperacao = (int)Dominio.Enum.TipoOperacao.Novo
            });
        }

        [HttpGet("informacoes-produto-operacao/refin")]
        public RetornoApi<InformacaoProdutoOperacaoModel> ConsultarInformacoesProdutoOperacaoRefin()
        {
            return FormatarRetorno(new InformacaoProdutoOperacaoModel()
            {
                IdProduto = (int)Dominio.Enum.Produto.CreditoConsignado,
                IdTipoOperacao = (int)Dominio.Enum.TipoOperacao.Refinanciamento
            });
        }

        [HttpGet("informacoes-produto-operacao/portabilidade")]
        public RetornoApi<InformacaoProdutoOperacaoModel> ConsultarInformacoesProdutoOperacaoPortabilidade()
        {
            return FormatarRetorno(new InformacaoProdutoOperacaoModel()
            {
                IdProduto = (int)Dominio.Enum.Produto.CreditoConsignado,
                IdTipoOperacao = (int)Dominio.Enum.TipoOperacao.Portabilidade
            });
        }


        [Authorize]
        [HttpPost("simulacao-refinanciamento")]
        public async Task<RetornoApi<IEnumerable<RetornoSimulacaoDto>>> SimularRefinanciamento([FromBody] SimulacaoRefinanciamentoEnvioModel parametros)
        {
            var simulacao = await _consignadoServico.SimularRefinanciamento(parametros);

            return FormatarRetorno(simulacao);
        }

        [Authorize]
        [HttpGet("simulacao-portabilidade")]
        public async Task<RetornoApi<SimulacaoPortabilidadeModel>> SimularPortabilidade([FromQuery] SimulacaoPortabilidadeEnvioModel parametros)
        {
            var simulacao = await _consignadoServico.SimularPortabilidade(parametros);

            return FormatarRetorno(simulacao);
        }

        [Authorize]
        [HttpGet("contratos")]
        public async Task<RetornoApi<IEnumerable<ContratoClienteModel>>> ListarContratosCliente([FromQuery] ContratoClienteEnvioModel parametros)
        {
            var simulacao = await _consignadoServico.ListarContratosCliente(parametros);

            return FormatarRetorno(simulacao);
        }
    }
}
