using Dominio.Enum;
using SharedKernel.ValueObjects.v2;
using System.Collections.Generic;

namespace Dominio
{
    public class UsuarioDominio : EntidadeBase
    {
        public string Nome { get; private set; }
        private string _email;
        public string Email { get => _email; private set => _email = value?.ToLower(); }
        private bool _emailConfirmado;
        public bool EmailConfirmado { 
            get => _emailConfirmado; 
            private set => _emailConfirmado = value;
        }
        private string _cpf;
        public string CPF { get => _cpf; private set => _cpf = value?.Trim(); }
        public string Senha { get; private set; }
        public bool Administrador { get; private set; } = false;

        public IEnumerable<IntencaoOperacaoDominio> IntencoesOperacao { get; private set; }
        public ClienteDominio Cliente { get; private set; }
        public List<UsuarioRedeSocialDominio> UsuariosRedesSociais { get; private set; } = new List<UsuarioRedeSocialDominio>();

        public UsuarioDominio() { }

        public UsuarioDominio(string nome, string email, bool emailConfirmado, CPF cpf, string senha, ClienteDominio cliente)
        {
            Nome = nome;
            Email = email;
            EmailConfirmado = emailConfirmado;
            CPF = cpf?.ToString();
            Senha = senha;
            Cliente = cliente;
        }
        
        public bool LoginPermitido() => EmailConfirmado;

        public void AdicionarVinculoRedeSocial(RedeSocial redeSocial, string login)
            => UsuariosRedesSociais.Add(new UsuarioRedeSocialDominio(redeSocial, login));

        public void SetPropriedadesAtualizadas(string nome, string email, CPF cpf)
        {
            Nome = nome;
            Email = email;
            CPF = cpf?.ToString();
        }

        public void SetSenha(string senha)
        {
            Senha = senha;
            setDataAtualizacao();
        }

        public void SetPermissaoAdministrador(bool concederPermissao)
        {
            Administrador = concederPermissao;
            setDataAtualizacao();
        }

        public void ConfirmarEmail()
        {
            EmailConfirmado = true;
        }
    }
}
