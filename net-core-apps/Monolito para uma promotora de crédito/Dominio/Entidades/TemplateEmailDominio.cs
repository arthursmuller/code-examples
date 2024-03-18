using Dominio.Enum.TemplateEmail;

namespace Dominio
{
    public class TemplateEmailDominio : EntidadeBase
    {
        public string Conteudo { get; private set; }

        public TemplateEmailTipo IdTipo { get; private set; }

        public TemplateEmailFinalidade IdFinalidade { get; private set; }

        public TemplateEmailTipoDominio TemplateEmailTipo { get; private set; } 

        public TemplateEmailFinalidadeDominio TemplateEmailFinalidade { get; private set; }

        public TemplateEmailDominio(string conteudo, TemplateEmailTipo idTipo, TemplateEmailFinalidade idFinalidade)
        {
            Conteudo = conteudo;
            IdTipo = idTipo;
            IdFinalidade = idFinalidade;
        }
    }
}
