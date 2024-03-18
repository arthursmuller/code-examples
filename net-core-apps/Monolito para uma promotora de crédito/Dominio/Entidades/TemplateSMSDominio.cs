using Dominio.Enum.TemplateSms;

namespace Dominio
{
    public class TemplateSmsDominio : EntidadeBase
    {
        public new TemplateSms ID { get; private set; }
        public string Descricao { get; private set; }
        public string Conteudo { get; set; }

        public TemplateSmsDominio() {}

        public TemplateSmsDominio(TemplateSms id, string descricao)
        {
            this.ID = id;
            this.Descricao = descricao;
        }

        public TemplateSmsDominio(TemplateSms id, string descricao, string conteudo)
        {
            this.ID = id;
            this.Descricao = descricao;
            this.Conteudo = conteudo;
        }

    }
}
