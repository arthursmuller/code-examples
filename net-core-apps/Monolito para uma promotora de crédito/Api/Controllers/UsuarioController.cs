using Aplicacao;
using Aplicacao.Model.Autenticacao;
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
    [Route("usuarios")]
    [ExcludeFromCodeCoverage]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioServico _usuarioServico;

        public UsuarioController(IUsuarioServico usuarioServico, IBemMensagens mensagens) : base(mensagens)
        {
            _usuarioServico = usuarioServico;
        }

        [HttpGet("autenticado")]
        [Authorize]
        public async Task<RetornoApi<UsuarioModel>> Get()
        {
            var usuario = await _usuarioServico.ObterUsuarioAutenticado();

            return FormatarRetorno(usuario);
        }

        [HttpPost]
        public async Task<RetornoApi<UsuarioModel>> Post([FromBody] UsuarioCriacaoModel usuario)
        {
            var usuarioNovo = await _usuarioServico.CriarUsuario(usuario);

            return FormatarRetorno(usuarioNovo);
        }

        [HttpPut("autenticado")]
        [Authorize(Roles = "admin")]
        public async Task<RetornoApi<UsuarioModel>> Put([FromBody] UsuarioAtualizacaoModel usuario)
        {
            var usuarioAtualizado = await _usuarioServico.AtualizarUsuario(usuario);

            return FormatarRetorno(usuarioAtualizado);
        }

        [HttpPut("autenticado/senha")]
        [Authorize]
        public async Task<RetornoApi<AutenticacaoModel>> AtualizarSenha([FromBody] UsuarioAtualizacaoSenhaModel requisicao)
        {
            var usuarioComSenhaAtualizada = await _usuarioServico.AtualizarSenhaUsuarioLogado(requisicao);

            return FormatarRetorno(usuarioComSenhaAtualizada);
        }

        [HttpPost("recuperacao-senha")]
        public async Task<RetornoApi<bool>> RecuperacaoSenha([FromBody] UsuarioRecuperacaoSenhaRequisicao requisicao)
        {
            var retorno = await _usuarioServico.RequisitarTrocaDeSenha(requisicao.Email);

            return FormatarRetorno(retorno);
        }

        [HttpGet("recuperacao-senha/{token}")]
        public async Task<RetornoApi<bool>> ValidarTokenSenha(string token)
        {
            var retorno = await _usuarioServico.ConsultarValidadeTokenSenha(token);

            return FormatarRetorno(retorno);
        }

        [HttpPost("recuperacao-senha/{token}")]
        public async Task<RetornoApi<AutenticacaoModel>> AtualizarSenhaComToken(string token, [FromBody] UsuarioRecuperacaoSenha requisicao)
        {
            var usuarioComSenhaAtualizada = await _usuarioServico.AtualizarSenhaUsuarioELogar(token, requisicao.NovaSenha);

            return FormatarRetorno(usuarioComSenhaAtualizada);
        }

        [HttpPost("confirmacao-email/{token}/{userId}")]
        public async Task<RetornoApi<AutenticacaoModel>> AtualizarEmailComToken(string token, int userId)
        {
            var usuarioComEmailConfirmado = await _usuarioServico.ConfirmarEmailELogar(token, userId);

            return FormatarRetorno(usuarioComEmailConfirmado);
        }
    }
}
