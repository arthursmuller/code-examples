using Dominio.Enum.TemplateTorpedoVoz;

namespace Dominio
{
    public class TemplateTorpedoVozDominio : EntidadeBase
    {
        public new TemplateTorpedoVoz ID { get; private set; }
        public string Descricao { get; private set; }
        public string Conteudo { get; set; }

        public TemplateTorpedoVozDominio() {}
        
        public TemplateTorpedoVozDominio(TemplateTorpedoVoz id, string descricao)
        {
            ID = id;
            Descricao = descricao;
        }
        public TemplateTorpedoVozDominio(TemplateTorpedoVoz id, string descricao, string conteudo)
        {
            ID = id;
            Descricao = descricao;
            Conteudo = conteudo;
        }
            
    }
}
