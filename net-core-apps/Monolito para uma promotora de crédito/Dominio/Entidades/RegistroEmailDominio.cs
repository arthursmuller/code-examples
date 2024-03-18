using System;
using Dominio.Enum.TemplateEmail;

namespace Dominio
{
    public class RegistroEmailDominio : EntidadeBase
    {
        public string CodigoReferenciaEmail { get => String.Format("API{0:0000000000}", ID); }
        public TemplateEmailFinalidade IdFinalidade { get; private set; }
        public string Destinatarios { get; private set; }
        public int? IdUsuario { get; private set; }
        public int? CodigoOrigem { get; private set; }
        public UsuarioDominio Usuario { get; private set; }
        public TemplateEmailFinalidadeDominio TemplateEmailFinalidade { get; private set; }


        public RegistroEmailDominio(TemplateEmailFinalidade idFinalidade, string destinatarios, int? idUsuario)
        {
            IdFinalidade = idFinalidade;
            Destinatarios = destinatarios;
            IdUsuario = idUsuario;
        }

        public RegistroEmailDominio(TemplateEmailFinalidade idFinalidade, string[] destinatarios, int? idUsuario)
        {
            IdFinalidade = idFinalidade;
            Destinatarios = String.Join(", ", destinatarios);
            IdUsuario = idUsuario;
        }
    }
}
