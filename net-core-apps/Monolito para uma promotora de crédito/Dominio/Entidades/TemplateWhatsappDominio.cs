using Dominio.Enum.TemplateWhatsapp;

namespace Dominio
{
    public class TemplateWhatsappDominio : EntidadeBase
    {
        public new TemplateWhatsapp ID { get; private set; }
        
        public string Descricao { get; private set; }
        
        public string GUID { get; private set; }

        public TemplateWhatsappDominio() {}

        public TemplateWhatsappDominio(TemplateWhatsapp id, string descricao)
        {
            this.ID = id;
            this.Descricao = descricao;
        }

        public TemplateWhatsappDominio(TemplateWhatsapp id, string descricao, string GUID)
        {
            this.ID = id;
            this.Descricao = descricao;
            this.GUID = GUID;
        }


    }
}
