namespace Dominio
{
    public class UsuarioRequisicaoLogDominio : EntidadeBase
    {
        public int IdUsuario { get; private set; }

        public string UsuarioTenant { get; set; }

        public string UrlRequisicao { get; private set; }

        public string CorpoRequisicao { get; private set; }

        public UsuarioDominio Usuario { get; private set; }

        public UsuarioRequisicaoLogDominio(int idUsuario, string usuarioTenant, string urlRequisicao, string corpoRequisicao)
        {
            IdUsuario = idUsuario;
            UsuarioTenant = usuarioTenant;
            UrlRequisicao = urlRequisicao;
            CorpoRequisicao = corpoRequisicao;
        }
    }
}