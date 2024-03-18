using Aplicacao.Model.Autenticacao;
using AutoMapper;
using B.Configuracao;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Dominio.Enum.TemplateEmail;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IBemMensagens _mensagens;
        private readonly IUsuarioLogin _usuarioLogin;
        private readonly IAutenticacaoLoginSocialServico _autenticacaoLoginSocialServico;
        private readonly IEmailServico _emailServico;
        private readonly ITermoServico _termoServico;
        private readonly PlataformaClienteContexto _contexto;
        private readonly AesCryptography _aes;
        private readonly Configuracao _configuracao;

        public UsuarioServico(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto, AesCryptography aes,
            IAutenticacaoLoginSocialServico autenticacaoLoginSocialServico, IEmailServico emailServico, Configuracao configuracao, ITermoServico termoServico)
        {
            _mensagens = mensagens;
            _usuarioLogin = usuarioLogin;
            _contexto = contexto;
            _aes = aes;
            _autenticacaoLoginSocialServico = autenticacaoLoginSocialServico;
            _emailServico = emailServico;
            _configuracao = configuracao;
            _termoServico = termoServico;
        }

        public async Task<UsuarioModel> ObterUsuarioAutenticado()
        {
            return await obterUsuario(_usuarioLogin.IdUsuario);
        }

        public async Task<UsuarioModel> CriarUsuario(UsuarioCriacaoModel usuario)
        {
            await validarCriacaoUsuario(usuario);
            if (_mensagens.PossuiErros)
                return null;

            var senhaCriptografada = _aes.Encrypt(usuario.Senha);

            var novoUsuario = new UsuarioDominio(
                usuario.Nome,
                usuario.Email,
                false,
                new CPF(usuario.CPF),
                senhaCriptografada,
                new ClienteDominio(usuario.Nome)
            );

            if (usuario.LoginSocial?.RedeSocial != null)
            {
                var validacaoToken = await _autenticacaoLoginSocialServico.ValidarToken(Mapper.Map<LoginSocialModel>(usuario.LoginSocial));
                if (_mensagens.PossuiErros)
                    return null;

                novoUsuario.AdicionarVinculoRedeSocial(usuario.LoginSocial.RedeSocial, validacaoToken.Login);
            }

            await _contexto.AddAsync(novoUsuario);

            await _contexto.SaveChangesAsync();

            await gravarAceiteTermos(novoUsuario.ID);

            await EnviarEmailConfirmacaoEmail(novoUsuario);

            return ConverterParaModel(novoUsuario);
        }

        public async Task<UsuarioModel> AtualizarUsuario(UsuarioAtualizacaoModel usuarioAtualizado)
        {
            if (!string.IsNullOrWhiteSpace(usuarioAtualizado.CPF) && !CPF.IsValid(usuarioAtualizado.CPF, _mensagens))
                return null;

            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(x => x.ID.Equals(_usuarioLogin.IdUsuario));

            if (usuario == null)
            {
                adicionarMensagemUsuarioNaoEncontrado();

                return new UsuarioModel();
            }

            usuario.SetPropriedadesAtualizadas(usuarioAtualizado.Nome, usuarioAtualizado.Email, new CPF(usuarioAtualizado.CPF));

            await _contexto.SaveChangesAsync();

            return ConverterParaModel(usuario);
        }

        public async Task<AutenticacaoModel> AtualizarSenhaUsuarioLogado(UsuarioAtualizacaoSenhaModel requisicao)
        {
            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(x => x.ID.Equals(_usuarioLogin.IdUsuario));

            if (usuario == null)
            {
                adicionarMensagemUsuarioNaoEncontrado();
                return null;
            }

            var senhaAtualEncriptografada = _aes.Encrypt(requisicao.SenhaAtual);

            if (usuario.Senha != senhaAtualEncriptografada)
            {
                _mensagens.AdicionarErro(Mensagens.Usuario_SenhaAtualNaoConfereComUsuario, EnumMensagemTipo.formulario);
                return null;
            }

            var senhaCriptografada = _aes.Encrypt(requisicao.NovaSenha);

            usuario.SetSenha(senhaCriptografada);

            await _contexto.SaveChangesAsync();

            return _autenticacaoLoginSocialServico.GerarToken(usuario.ID, usuario.Nome, usuario.Email);
        }

        public async Task<AutenticacaoModel> AtualizarSenhaUsuarioELogar(string token, string novaSenha)
        {
            var requisicao = await consultarTokenSenha(token);

            if (requisicao == null)
            {
                return null;
            }

            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(x => x.ID.Equals(requisicao.IdUsuario));

            if (!usuario.LoginPermitido())
            {
                _mensagens.AdicionarErro(Mensagens.Usario_EmailConfirmado, B.Mensagens.EnumMensagemTipo.negocio);
                return null;
            }

            var senhaCriptografada = _aes.Encrypt(novaSenha);
            usuario.SetSenha(senhaCriptografada);

            requisicao.Invalidar();

            await _contexto.SaveChangesAsync();

            return _autenticacaoLoginSocialServico.GerarToken(usuario.ID, usuario.Nome, usuario.Email);
        }

        public async Task<bool> RequisitarTrocaDeSenha(string email)
        {
            var usuario = await _contexto.Usuarios
                .Include(u => u.Cliente)
                .FirstOrDefaultAsync(x => x.Email.Equals(email.ToLower()));

            if (usuario == null)
            {
                _mensagens.AdicionarErro(Mensagens.Usuario_EmailNaoEncontrado, EnumMensagemTipo.banco);
                return false;
            }

            if (!usuario.LoginPermitido())
            {
                _mensagens.AdicionarErro(Mensagens.Usario_EmailConfirmado, B.Mensagens.EnumMensagemTipo.negocio);
                return false;
            }

            String token;
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                Byte[] bytes = new Byte[18];
                rng.GetBytes(bytes);

                String base64 = Convert.ToBase64String(bytes);
                token = base64.Replace('+', '-').Replace('/', '_');
            }

            var resultadoEnvio = await enviarEmailRecuperacaoDeSenha(usuario, token);

            if (!resultadoEnvio)
            {
                _mensagens.AdicionarErro(Mensagens.Usuario_ProblemaEmailRecuperacaoSenha, EnumMensagemTipo.banco);
                return false;
            }

            var requisicoesAbertas = await _contexto.UsuarioRecuperacaoSenhas
                .Where(requisicao => requisicao.IdUsuario == usuario.ID)
                .ToListAsync();

            requisicoesAbertas.ForEach(requisicao => requisicao.Invalidar());

            var tokenEncriptografado = _aes.Encrypt(token);

            var novaRequisicao = new UsuarioRecuperacaoSenhaDominio(usuario.ID, tokenEncriptografado);
            await _contexto.UsuarioRecuperacaoSenhas.AddAsync(novaRequisicao);

            await _contexto.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ConsultarValidadeTokenSenha(string token)
        {
            var resultado = await consultarTokenSenha(token);
            return resultado != null;
        }

        public async Task GravarLogRequisicao(UsuarioRequisicaoLogDominio log)
        {
            await _contexto.AddAsync(log);

            await _contexto.SaveChangesAsync();
        }

        public static UsuarioModel ConverterParaModel(UsuarioDominio usuario)
        {
            return new UsuarioModel
            {
                Id = usuario.ID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                CPF = usuario.CPF,
                Administrador = usuario.Administrador
            };
        }

        public async Task<bool> EnviarEmailConfirmacaoEmail(UsuarioDominio usuario)
        {
            String token;
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                Byte[] bytes = new Byte[18];
                rng.GetBytes(bytes);

                String base64 = Convert.ToBase64String(bytes);
                token = base64.Replace('+', '-').Replace('/', '_');
            }

            var resultadoEnvio = await enviarEmailConfirmacaoEmail(usuario, token);

            if (!resultadoEnvio)
            {
                _mensagens.AdicionarErro(Mensagens.Usuario_ProblemaEmailRecuperacaoSenha, EnumMensagemTipo.banco);
                return false;
            }

            var requisicoesAbertas = await _contexto.UsuarioConfirmacaoEmailDominio
                .Where(requisicao => requisicao.IdUsuario == usuario.ID)
                .ToListAsync();

            requisicoesAbertas.ForEach(requisicao => requisicao.Invalidar());

            var tokenEncriptografado = _aes.Encrypt(token);

            var novaRequisicao = new UsuarioConfirmacaoEmailDominio(usuario.ID, tokenEncriptografado);
            await _contexto.UsuarioConfirmacaoEmailDominio.AddAsync(novaRequisicao);
            await _contexto.SaveChangesAsync();

            return true;
        }

        public async Task<AutenticacaoModel> ConfirmarEmailELogar(string token, int userId)
        {
            var usuario = await _contexto.Usuarios
                .FirstOrDefaultAsync(u => u.ID.Equals(userId));

            var emailConfirmado = await confirmarEmail(usuario, token);

            if (!emailConfirmado)
            {
                _mensagens.AdicionarAlerta(string.Format(Mensagens.Usario_EmailNaoConfirmado, usuario.ID), EnumMensagemTipo.negocio);
                return null;
            }

            await enviarEmailBoasVindas(usuario);

            await _contexto.SaveChangesAsync();

            return _autenticacaoLoginSocialServico.GerarToken(usuario.ID, usuario.Nome, usuario.Email);
        }

        private async Task<bool> confirmarEmail(UsuarioDominio usuario, string token)
        {
            if (usuario.EmailConfirmado)
            {
                _mensagens.AdicionarAlerta(string.Format(Mensagens.Usario_EmailConfirmado, usuario.ID), EnumMensagemTipo.negocio);
                return false;
            }

            var resultado = await consultarTokenEmail(token);

            if (resultado is null || !resultado.Valido)
                return false;

            usuario.ConfirmarEmail();

            return true;
        }

        private void adicionarMensagemUsuarioNaoEncontrado()
            => _mensagens.AdicionarErro(Mensagens.Usuario_NaoEncontrado, EnumMensagemTipo.banco);

        private async Task gravarAceiteTermos(int idUsuario)
        {
            var termosPendentes = await _termoServico.ObterTermosPendentesAceiteUsuario(idUsuario);

            if (termosPendentes.Any())
            {
                foreach (var termo in termosPendentes)
                    await _contexto.AddAsync(new UsuarioTermoDominio(idUsuario, termo.ID));

                await _contexto.SaveChangesAsync();
            }
        }

        private async Task validarCriacaoUsuario(UsuarioCriacaoModel usuario)
        {
            var cpf = new CPF(usuario.CPF);
            if (!string.IsNullOrWhiteSpace(cpf.ToString()) && !cpf.IsValid(_mensagens))
                return;

            if (string.IsNullOrWhiteSpace(usuario.CPF) && string.IsNullOrWhiteSpace(usuario.Email))
            {
                _mensagens.AdicionarErro(Mensagens.Usuario_DeveInformarEmailOuCpf, EnumMensagemTipo.negocio);
                return;
            }

            var usuarioCadastrado = await _contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(u =>
                                            (!string.IsNullOrWhiteSpace(u.Email) && u.Email.Equals(usuario.Email))
                                            || (!string.IsNullOrWhiteSpace(u.CPF) && u.CPF.Equals(cpf.ToString())));
            if (usuarioCadastrado != null)
                _mensagens.AdicionarErro(Mensagens.Usuario_JaCadastrado, EnumMensagemTipo.negocio);
        }

        private async Task<UsuarioModel> obterUsuario(int id)
        {
            var usuario = await _contexto.Usuarios
                .FirstOrDefaultAsync(u => u.ID.Equals(id));

            if (usuario == null)
            {
                adicionarMensagemUsuarioNaoEncontrado();

                return null;
            }

            return ConverterParaModel(usuario);
        }

        private async Task<UsuarioRecuperacaoSenhaDominio> consultarTokenSenha(string token)
        {
            var tokenEncriptografado = _aes.Encrypt(token);
            var requisicao = await _contexto.UsuarioRecuperacaoSenhas.FirstOrDefaultAsync(requisicao => requisicao.Token.Equals(tokenEncriptografado));

            if (requisicao == null)
            {
                _mensagens.AdicionarErro(Mensagens.Usuario_TokenInvalido, EnumMensagemTipo.formulario);
                return null;
            }

            if (requisicao.Expirado)
            {
                _mensagens.AdicionarErro(Mensagens.Usuario_TokenExpirado, EnumMensagemTipo.banco);
                return null;
            }

            return requisicao;
        }

        private async Task<UsuarioConfirmacaoEmailDominio> consultarTokenEmail(string token)
        {
            var requisicao = await _contexto.UsuarioConfirmacaoEmailDominio.FirstOrDefaultAsync(requisicao => requisicao.Token.Equals(token));

            if (requisicao == null)
            {
                _mensagens.AdicionarErro(Mensagens.Usuario_TokenInvalido, EnumMensagemTipo.formulario);
                return null;
            }

            if (requisicao.Expirado)
            {
                _mensagens.AdicionarErro(Mensagens.Usuario_TokenExpirado, EnumMensagemTipo.banco);
                return null;
            }

            return requisicao;
        }

        private async Task<bool> enviarEmailConfirmacaoEmail(UsuarioDominio usuario, string token)
        {
            var urlConfirmarEmail = $"{_configuracao.BuscarParametro("ambiente-frontend-url")}confirmacao-email/{token}/{usuario.ID}";

            var chavesLayout = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["url"] = urlConfirmarEmail,
                ["urlText"] = urlConfirmarEmail.Replace("https://", ""),
                ["usuario"] = usuario,
            };

            var resultado = await _emailServico.RegistrarEmail(
                TemplateEmailFinalidade.ConfirmacaoEmail,
                "Confirmar Email",
                new string[] { usuario.Email },
                chavesLayout,
                usuario.ID
            );

            return resultado;
        }

        private async Task<bool> enviarEmailRecuperacaoDeSenha(UsuarioDominio usuario, string token)
        {
            var urlRecuperacaoSenha = $"{_configuracao.BuscarParametro("ambiente-frontend-url")}recuperacao-senha/{token}";

            var chavesLayout = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["url"] = urlRecuperacaoSenha,
                ["urlText"] = urlRecuperacaoSenha.Replace("https://", ""),
                ["usuario"] = usuario,
            };

            var resultado = await _emailServico.RegistrarEmail(
                TemplateEmailFinalidade.RecuperacaoSenha,
                "Requisição de recuperação de senha",
                new string[] { usuario.Email },
                chavesLayout,
                usuario.ID
            );

            return resultado;
        }

        private async Task<bool> enviarEmailBoasVindas(UsuarioDominio usuario)
        {
            try
            {
                var urlAmbiente = _configuracao.BuscarParametro("ambiente-frontend-url");

                var chavesLayout = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
                {
                    ["url"] = urlAmbiente,
                    ["usuario"] = usuario,
                };

                var resultado = await _emailServico.RegistrarEmail(
                    TemplateEmailFinalidade.Cadastro,
                    "Boas Vindas",
                    new string[] { usuario.Email },
                    chavesLayout,
                    usuario.ID
                );
            }
            catch
            {
                _mensagens.AdicionarAlerta(string.Format(Mensagens.Usuario_erroEnvioEmail, usuario.ID), EnumMensagemTipo.comunicacaoapi);

                return false;
            }

            return true;
        }
    }
}
