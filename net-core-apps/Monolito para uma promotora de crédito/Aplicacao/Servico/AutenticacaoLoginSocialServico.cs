using Aplicacao.Model.Autenticacao;
using AutoMapper;
using B.Autenticacao;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Google.Apis.Auth;
using Infraestrutura;
using Infraestrutura.Autenticacao;
using Infraestrutura.RedesSociais;
using LoginSocialApple;
using LoginSocialFacebook;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Aplicacao.Servico
{
    public class AutenticacaoLoginSocialServico : AutenticacaoServicoBase, IAutenticacaoLoginSocialServico
    {
        private readonly IBemMensagens _mensagens;
        private readonly ILogger<IAutenticacaoLoginSocialServico> _logger;
        private readonly IProvedorFacebook _provedorFacebook;
        private readonly IProvedorApple _provedorApple;
        private readonly PlataformaClienteContexto _contexto;
        private readonly RedesSociaisConfiguracao _redesSociaisconfiguracao;

        public AutenticacaoLoginSocialServico(IServicoToken servicoToken, ConfiguracaoAutenticacao configuracaoAutenticacao, IBemMensagens mensagens, ILogger<IAutenticacaoLoginSocialServico> logger,
            IProvedorFacebook provedorFacebook, PlataformaClienteContexto contexto, RedesSociaisConfiguracao redesSociaisconfiguracao, IProvedorApple provedorApple)
            : base(servicoToken, configuracaoAutenticacao)
        {
            _mensagens = mensagens;
            _logger = logger;
            _provedorFacebook = provedorFacebook;
            _contexto = contexto;
            _redesSociaisconfiguracao = redesSociaisconfiguracao;
            _provedorApple = provedorApple;
        }

        public async Task<AutenticacaoLoginSocialModel> Autenticar(LoginSocialModel loginSocial)
        {
            var validacaoToken = await ValidarToken(loginSocial);

            if (_mensagens.PossuiErros)
                return null;

            var usuario = await obterUsuarioPeloLoginSocial(loginSocial.RedeSocial, validacaoToken.Login);
            if (usuario is null)
            {

                usuario = await obterUsuarioPorEmail(validacaoToken.Email);
                if(usuario is null)
                    return new AutenticacaoLoginSocialModel(validacaoToken.Nome, validacaoToken.Email, token: string.Empty);

                usuario.AdicionarVinculoRedeSocial(loginSocial.RedeSocial, validacaoToken.Login);

                await _contexto.SaveChangesAsync();
            }

            return Mapper.Map<AutenticacaoLoginSocialModel>(GerarToken(usuario.ID, usuario.Nome, usuario.Email));
        }

        public async Task<ValidacaoTokenModel> ValidarToken(LoginSocialModel loginSocial)
        {
            ValidacaoTokenModel validacao = null;
            try
            {
                validacao = loginSocial.RedeSocial switch
                {
                    RedeSocial.Google => await validarTokenGoogle(loginSocial.Token),
                    RedeSocial.Facebook => await validarTokenFacebook(loginSocial.Token),
                    RedeSocial.AppleSignIn => await validarTokenAppleSignIn(loginSocial.Token, loginSocial.AppleCode, loginSocial.RedirectUrl),
                    _ => null,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Mensagens.LoginSocial_TokenInvalido} {ex.Message}");
                _mensagens.AdicionarErro(Mensagens.LoginSocial_TokenInvalido, B.Mensagens.EnumMensagemTipo.negocio);
            }

            if (!_mensagens.PossuiErros && validacao is null)
                _mensagens.AdicionarErro(Mensagens.LoginSocial_NaoImplementado, B.Mensagens.EnumMensagemTipo.negocio);

            return validacao;
        }

        private async Task<ValidacaoTokenModel> validarTokenGoogle(string token)
        {
            var payloadGoogle = await GoogleJsonWebSignature.ValidateAsync(token, new ValidationSettings { Audience = new string[] { _redesSociaisconfiguracao?.Google?.ClientId } });

            return new ValidacaoTokenModel(login: payloadGoogle.Email, payloadGoogle.Email, payloadGoogle.Name);
        }

        private async Task<ValidacaoTokenModel> validarTokenFacebook(string token)
        {
            var validacao = await _provedorFacebook.ValidarToken(token);
            if (validacao is null)
            {
                _mensagens.AdicionarErro(Mensagens.LoginSocial_FacebookTokenInformadoInvalido, B.Mensagens.EnumMensagemTipo.negocio);
                return null;
            }

            return new ValidacaoTokenModel(login: validacao.Email, validacao.Email, validacao.Name);
        }

        private async Task<ValidacaoTokenModel> validarTokenAppleSignIn(string token, string code, string redirectUrl)
        {

            LoginSocialApple.Dto.ValidacaoTokenRetornoDto validacao;

            /* Valida somente o token, para o momento da criação da conta! */
            if(string.IsNullOrEmpty(code))
                validacao = await _provedorApple.ValidarToken(token);
            /* Para o caso de criação, além de validarmos se o token gerado está correto, geramos uma nova chave de autenticação */
            else 
                validacao = await _provedorApple.ValidarToken(code, redirectUrl);

            if (validacao is null)
            {
                _mensagens.AdicionarErro(Mensagens.LoginSocial_AppleTokenInformadoInvalido, B.Mensagens.EnumMensagemTipo.negocio);
                return null;
            }

            return new ValidacaoTokenModel(login: validacao.Email, validacao.Email, validacao.Name);
        }

        private async Task<UsuarioDominio> obterUsuarioPeloLoginSocial(RedeSocial redeSocial, string login) =>
            await _contexto
                .UsuariosRedesSociais
                .Include(u => u.Usuario)
                .Where(u => u.IdRedeSocial.Equals(redeSocial) && u.Login.Equals(login) && u.Ativo)
                .Select(u => u.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync();

        private async Task<UsuarioDominio> obterUsuarioPorEmail(string email) =>
            await _contexto
                .Usuarios
                .FirstOrDefaultAsync(e => e.Email.Equals(email));
                            
    }
}
