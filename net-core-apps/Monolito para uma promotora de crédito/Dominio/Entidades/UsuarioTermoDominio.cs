namespace Dominio
{
    public class UsuarioTermoDominio : EntidadeBase
    {
        public int IdUsuario { get; private set; }

        public int IdTermo { get; private set; }

        public UsuarioDominio Usuario { get; private set; }

        public TermoDominio Termo { get; private set; }

        public UsuarioTermoDominio(int idUsuario, int idTermo)
        {
            IdUsuario = idUsuario;
            IdTermo = idTermo;
        }
    }
}
