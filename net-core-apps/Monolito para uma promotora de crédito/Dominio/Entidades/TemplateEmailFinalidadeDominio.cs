using Dominio.Enum.TemplateEmail;

namespace Dominio
{
    public class TemplateEmailFinalidadeDominio : EntidadeBase
    {
        public new TemplateEmailFinalidade ID { get; private set; }
        public string Descricao { get; private set; }

        public TemplateEmailFinalidadeDominio() {}

        public TemplateEmailFinalidadeDominio(TemplateEmailFinalidade id, string descricao)
        {
            ID = id;
            Descricao = descricao;
        }
    }
}
