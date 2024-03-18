using Aplicacao.Model.AcessoDadosPessoais;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class AcessoDadosPessoaisController : BaseController
    {
        private readonly AcessoDadosPessoaisServico _acessoDadosPessoaisServico;

        public AcessoDadosPessoaisController(IBemMensagens mensagens, AcessoDadosPessoaisServico acessoDadosPessoaisServico) : base(mensagens)
            => _acessoDadosPessoaisServico = acessoDadosPessoaisServico;

        [HttpPost("dados-pessoais/solicitacao-acesso")]
        [AllowAnonymous]
        public async Task<RetornoApi<SolicitacaoAcessoDadosPessoaisModel>> CriarSolicitacaoDeAcesso([FromBody] SolicitacaoAcessoDadosPessoaisEnvioModel solicitacao)
        {
            var novaSolicitacao = await _acessoDadosPessoaisServico.CriarSolicitacaoDeAcesso(solicitacao);

            return FormatarRetorno(novaSolicitacao);
        }
    }
}
