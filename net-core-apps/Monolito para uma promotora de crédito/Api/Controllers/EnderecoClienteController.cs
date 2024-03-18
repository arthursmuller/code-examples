using Aplicacao.Model.EnderecoCliente;
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
    [Route("clientes/autenticado/enderecos")]
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class EnderecoClienteController : BaseController
    {
        private readonly EnderecoClienteServico _enderecoClienteServico;

        public EnderecoClienteController(IBemMensagens mensagens, EnderecoClienteServico enderecoClienteServico) : base(mensagens)
            => _enderecoClienteServico = enderecoClienteServico;

        [HttpGet]
        public async Task<RetornoApi<IEnumerable<EnderecoClienteExibicaoModel>>> Get()
        {
            var enderecos = await _enderecoClienteServico.BuscarEnderecosPorCliente();

            return FormatarRetorno(enderecos);
        }

        [HttpPost]
        public async Task<RetornoApi<EnderecoClienteExibicaoModel>> Post([FromBody] EnderecoClienteModel enderecoGravacao)
        {
            var novoEndereco = await _enderecoClienteServico.GravarEndereco(enderecoGravacao);

            return FormatarRetorno(novoEndereco);
        }

        [HttpPut("{idEndereco}")]
        public async Task<RetornoApi<EnderecoClienteExibicaoModel>> Put(int idEndereco, [FromBody] EnderecoClienteModel enderecoAtualizacao)
        {
            var endereco = await _enderecoClienteServico.AtualizarEndereco(idEndereco, enderecoAtualizacao);

            return FormatarRetorno(endereco);
        }

        [HttpDelete("{idEndereco}")]
        public async Task<RetornoApi<bool>> Delete(int idEndereco)
        {
            var enderecoRemovido = await _enderecoClienteServico.DeletarEndereco(idEndereco);

            return FormatarRetorno(enderecoRemovido);
        }
    }
}
