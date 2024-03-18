using Dominio.Enum;

namespace Dominio
{
    public class UsuarioRedeSocialDominio : EntidadeBase
    {
        public int IdUsuario { get; private set; }
        public RedeSocial IdRedeSocial { get; private set; }
        public string Login { get; private set; }
        public bool Ativo { get; private set; }

        public UsuarioDominio Usuario { get; private set; }

        public UsuarioRedeSocialDominio(int idUsuario, RedeSocial idRedeSocial, string login)
        {
            IdUsuario = idUsuario;
            IdRedeSocial = idRedeSocial;
            Login = login;
            Ativo = true;
        }

        public UsuarioRedeSocialDominio(RedeSocial idRedeSocial, string login)
        {
            IdRedeSocial = idRedeSocial;
            Login = login;
            Ativo = true;
        }

        public void AlternarAtivo(bool ativo)
        {
            Ativo = ativo;
            setDataAtualizacao();
        }
    }
}
