using Aplicacao.Model.TelefoneCliente;
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
    [Route("clientes/autenticado/telefones")]
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class TelefoneClienteController : BaseController
    {
        private readonly TelefoneClienteServico _telefoneClienteServico;

        public TelefoneClienteController(TelefoneClienteServico telefoneClienteServico, IBemMensagens mensagens) : base(mensagens)
           => _telefoneClienteServico = telefoneClienteServico;

        [HttpGet]
        public async Task<RetornoApi<IEnumerable<TelefoneClienteExibicaoModel>>> Get()
        {
            var telefonesGravados = await _telefoneClienteServico.BuscarTelefonesPorCliente();

            return FormatarRetorno(telefonesGravados);
        }

        [HttpPost]
        public async Task<RetornoApi<bool>> Post([FromBody] TelefoneClienteModel telefone)
        {
            var telefonesGravados = await _telefoneClienteServico.GravarTelefone(telefone);

            return FormatarRetorno(telefonesGravados);
        }

        [HttpDelete("{idTelefone}")]
        public async Task<RetornoApi<bool>> Delete([FromRoute] int idTelefone)
        {
            var telefonesGravados = await _telefoneClienteServico.DeletarTelefone(idTelefone);

            return FormatarRetorno(telefonesGravados);
        }
    }
}
