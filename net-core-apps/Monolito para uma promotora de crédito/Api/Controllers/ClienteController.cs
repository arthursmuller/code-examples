using Aplicacao.Filters;
using Aplicacao.Model.Banco;
using Aplicacao.Model.Cliente;
using Aplicacao.Model.ContaCliente;
using Aplicacao.Model.EstadoCivil;
using Aplicacao.Model.Genero;
using Aplicacao.Model.GrauInstrucao;
using Aplicacao.Model.OrgaoEmissorIdentificacao;
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
    [Route("clientes")]
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class ClienteController : BaseController
    {
        private readonly IClienteServico _clienteServico;
        private readonly ClienteImportacaoServico _clienteImportacaoServico;
        private readonly IContaClienteServico _contaClienteServico;

        public ClienteController(IClienteServico clienteServico, ClienteImportacaoServico clienteImportacaoServico, IBemMensagens mensagens, IContaClienteServico contaClienteServico) : base(mensagens)
        {
            _clienteServico = clienteServico;
            _clienteImportacaoServico = clienteImportacaoServico;
            _contaClienteServico = contaClienteServico;
        }

        [HttpGet("{cpf}")]
        [Authorize(Roles = "admin")]
        [ServiceFilter(typeof(FiltroLogRequisicao))]
        public async Task<RetornoApi<ClienteExibicaoCompletaModel>> Get([FromRoute] string cpf)
        {
            var cliente = await _clienteServico.ObterClientePorCpf(cpf);

            return FormatarRetorno(cliente);
        }

        [HttpGet("autenticado")]
        public async Task<RetornoApi<ClienteExibicaoModel>> Get()
        {
            var cliente = await _clienteServico.ObterClienteAutenticado();

            return FormatarRetorno(cliente);
        }

        [HttpPut("autenticado")]
        public async Task<RetornoApi<ClienteExibicaoModel>> Put([FromBody] ClienteModel cliente)
        {
            var clienteAtualizado = await _clienteServico.AtualizarCliente(cliente);

            return FormatarRetorno(clienteAtualizado);
        }

        [HttpPost("autenticado/solicitacao-importacao-dados")]
        public async Task<RetornoApi<bool>> RequisitarImportacaoDadosCliente()
        {
            var cliente = await _clienteImportacaoServico.RequisitarImportacaoDadosCliente();

            return FormatarRetorno(cliente);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{cpf}/autorizacao-importacao-dados")]
        public async Task<RetornoApi<bool>> AutorizarImportacaoDadosCliente([FromRoute] string cpf, [FromBody] ClienteAutorizacaoImportacaoDadosModel autorizacao)
        {
            var cliente = await _clienteImportacaoServico.AutorizarImportacaoDadosCliente(cpf, autorizacao.Autorizado);

            return FormatarRetorno(cliente);
        }

        [HttpGet("informacoes/graus-instrucao")]
        public async Task<RetornoApi<IEnumerable<GrauInstrucaoModel>>> ListarGrausInstrucao()
        {
            var unidadesFederativas = await _clienteServico.ListarGrausEscolaridade();

            return FormatarRetorno(unidadesFederativas);
        }

        [HttpGet("informacoes/estados-civil")]
        public async Task<RetornoApi<IEnumerable<EstadoCivilModel>>> ListarEstadosCivil()
        {
            var unidadesFederativas = await _clienteServico.ListarEstadosCivil();

            return FormatarRetorno(unidadesFederativas);
        }

        [HttpGet("informacoes/orgaos-emissores-identificacao")]
        public async Task<RetornoApi<IEnumerable<OrgaoEmissorIdentificacaoModel>>> ListarOrgaosEmissoresIdentificacao([FromQuery] string termo)
        {
            var orgaosEmissores = await _clienteServico.ListarOrgaosEmissoresIdentificacao(termo);

            return FormatarRetorno(orgaosEmissores);
        }

        [HttpGet("informacoes/generos")]
        public async Task<RetornoApi<IEnumerable<GeneroModel>>> ListarGeneros()
        {
            var generos = await _clienteServico.ListarGeneros();

            return FormatarRetorno(generos);
        }

        [HttpGet("autenticado/contas")]
        public async Task<RetornoApi<IEnumerable<ContaClienteExibicaoModel>>> ListarContas()
        {
            var contas = await _contaClienteServico.ListarContasAutenticado();

            return FormatarRetorno(contas);
        }

        [HttpDelete("autenticado/contas/{id}")]
        public async Task<RetornoApi<bool>> ExcluirConta([FromRoute] int id)
        {
            var resultado = await _contaClienteServico.ExcluirContaAutenticado(id);

            return FormatarRetorno(resultado);
        }

        [HttpPost("autenticado/adicionar-conta-bancaria")]
        public async Task<RetornoApi<bool>> AdicionarContaBancaria([FromBody] ContaBancariaModel model)
            => FormatarRetorno(await _clienteServico.AdicionarContaBancaria(model));

        [HttpPost("autenticado/atualizar-conta-bancaria")]
        public async Task<RetornoApi<bool>> AtualizarContaBancaria([FromBody] ContaBancariaModel model)
            => FormatarRetorno(await _clienteServico.AtualizarContaBancaria(model));
    }
}
