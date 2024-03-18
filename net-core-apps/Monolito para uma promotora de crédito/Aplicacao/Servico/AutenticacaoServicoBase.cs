using Aplicacao.Model.Autenticacao;
using B.Autenticacao;
using Infraestrutura.Autenticacao;
using System;
using System.Security.Claims;

namespace Aplicacao.Servico
{
    public class AutenticacaoServicoBase : IAutenticacaoServicoBase
    {
        private readonly IServicoToken _servicoToken;
        private readonly ConfiguracaoAutenticacao _configuracaoAutenticacao;

        public AutenticacaoServicoBase(IServicoToken servicoToken, ConfiguracaoAutenticacao configuracaoAutenticacao)
        {
            _servicoToken = servicoToken;
            _configuracaoAutenticacao = configuracaoAutenticacao;
        }

        public AutenticacaoModel GerarToken(int idUsuario, string nomeUsuario, string email)
                => gerarToken(idUsuario, nomeUsuario, email, null, false);

        protected AutenticacaoModel gerarToken(int idUsuario, string nomeUsuario, string email, string usuarioTenant, bool possuiAtribuicaoAdministrador)
        {
            var informacaoUsuario = new Claim[]
            {
                new Claim(ClaimTypes.Name, nomeUsuario),
                new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString()),
                new Claim(ClaimTypes.Role, possuiAtribuicaoAdministrador ? "admin" : string.Empty),
                new Claim("UsuarioTenant", usuarioTenant ?? string.Empty),
            };

            var token = _servicoToken.GerarToken(informacaoUsuario, _configuracaoAutenticacao.ChaveJwt, TimeSpan.FromSeconds(_configuracaoAutenticacao.SegundosParaExpirarToken));

            return new AutenticacaoModel
            {
                Nome = nomeUsuario,
                Email = email,
                Token = token,
            };
        }
    }
}
