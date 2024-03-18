using Dominio.Enum.TemplateEmail;

namespace Dominio
{
    public class TemplateEmailTipoDominio: EntidadeBase
    {
        public new TemplateEmailTipo ID { get; private set; }
        public string Descricao { get; private set; }

        public TemplateEmailTipoDominio() {}

        public TemplateEmailTipoDominio(TemplateEmailTipo id, string descricao)
        {
            ID = id;
            Descricao = descricao;
        }
    }
}
