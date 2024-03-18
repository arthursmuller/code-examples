using Aplicacao.Model.Autenticacao;
using B.Autenticacao;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Autenticacao;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class AutenticacaoServico : AutenticacaoServicoBase, IAutenticacaoServico
    {
        private readonly IBemMensagens _mensagens;
        private readonly PlataformaClienteContexto _contexto;
        private readonly AesCryptography _aes;

        public AutenticacaoServico(ConfiguracaoAutenticacao configuracaoAutenticacao, AesCryptography aes, IBemMensagens mensagens, PlataformaClienteContexto contexto,
            IServicoToken servicoToken) : base(servicoToken, configuracaoAutenticacao)
        {
            _mensagens = mensagens;
            _contexto = contexto;
            _aes = aes;
        }

        public async Task<AutenticacaoModel> Autenticar(LoginModel model, bool administrador = false)
        {
            var senhaCriptografada = _aes.Encrypt(model.Senha);

            var usuario = await _contexto
                                .Usuarios
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x =>
                                    x.Email.Equals(model.Email) && x.Senha.Equals(senhaCriptografada)
                                    || (!string.IsNullOrWhiteSpace(x.CPF) && x.CPF.Equals(model.Cpf) && x.Senha.Equals(senhaCriptografada)));

            if (usuario == null)
            {
                _mensagens.AdicionarErro(Mensagens.Login_Invalido, B.Mensagens.EnumMensagemTipo.negocio);
                return null;
            }

            if(!usuario.LoginPermitido())
            {
                _mensagens.AdicionarErro(Mensagens.Usario_EmailNaoConfirmado, B.Mensagens.EnumMensagemTipo.negocio);
                return null;
            }

            compararNivelAcesso(administrador, usuario);

            if (_mensagens.PossuiErros)
                return null;

            validarRegraInformacoesLogin(possuiPermissaoAdministrador: usuario.Administrador, model);

            if (_mensagens.PossuiErros)
                return null;

            return gerarToken(usuario.ID, usuario.Nome, usuario.Email, model.UsuarioTenant, usuario.Administrador);
        }

        private void compararNivelAcesso(bool administrador, UsuarioDominio usuario)
        {
            if (administrador != usuario.Administrador)
                _mensagens.AdicionarErro(Mensagens.Login_RecursoTipoCredencialDiferente, B.Mensagens.EnumMensagemTipo.negocio);
        }

        private void validarRegraInformacoesLogin(bool possuiPermissaoAdministrador, LoginModel login)
        {
            if (possuiPermissaoAdministrador && string.IsNullOrWhiteSpace(login.UsuarioTenant))
                _mensagens.AdicionarErro(Mensagens.Login_UsuarioTenantDeveSerInformado, B.Mensagens.EnumMensagemTipo.negocio);
        }
    }
}