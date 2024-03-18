using Aplicacao.Model.TelefoneClienteConfirmacao;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("telefones")]
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class TelefoneClienteConfirmacaoController : BaseController
    {
        private readonly TelefoneClienteConfirmacaoServico _telefoneClienteConfirmacaoServico;

        public TelefoneClienteConfirmacaoController(TelefoneClienteConfirmacaoServico telefoneClienteConfirmacaoServico, IBemMensagens mensagens) : base(mensagens)
            => _telefoneClienteConfirmacaoServico = telefoneClienteConfirmacaoServico;

        [HttpPost("{idTelefoneCliente}/solicitacoes-confirmacao")]
        public async Task<RetornoApi<TelefoneClienteSolicitacaoConfirmacaoModel>> SolicitarConfirmacaoDePropriedade([FromRoute] int idTelefoneCliente, [FromBody] TelefoneClienteSolicitacaoConfirmacaoEnvioModel solicitacao)
        {
            var novaSolicitacao = await _telefoneClienteConfirmacaoServico.SolicitarConfirmacaoDePropriedade(idTelefoneCliente, solicitacao);

            return FormatarRetorno(novaSolicitacao);
        }

        [HttpPatch("{idTelefoneCliente}/solicitacoes-confirmacao/reenvio")]
        public async Task<RetornoApi<TelefoneClienteSolicitacaoConfirmacaoModel>> ReenviarSolicitacaoConfirmacaoDePropriedade([FromRoute] int idTelefoneCliente, [FromBody] TelefoneClienteSolicitacaoConfirmacaoEnvioModel solicitacaoReenvio)
        {
            var solicitacao = await _telefoneClienteConfirmacaoServico.ReenviarSolicitacaoDeConfirmacaoDePropriedade(idTelefoneCliente, solicitacaoReenvio);

            return FormatarRetorno(solicitacao);
        }

        [HttpPatch("{idTelefoneCliente}/confirmacoes")]
        public async Task<RetornoApi<bool>> ConfirmarPropriedade([FromRoute] int idTelefoneCliente, [FromBody] TelefoneClienteConfirmacaoTokenModel telefoneClienteConfirmacaoTokenModel)
        {
            var telefoneConfirmado = await _telefoneClienteConfirmacaoServico.ConfirmarPropriedadeTelefone(idTelefoneCliente, telefoneClienteConfirmacaoTokenModel.Token);

            return FormatarRetorno(telefoneConfirmado);
        }
    }
}
