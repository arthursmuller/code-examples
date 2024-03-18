using Dominio.Enum;

namespace Dominio
{
    public class RedeSocialDominio : EntidadeBase
    {
        public new RedeSocial ID { get; private set; }
        public string Nome { get; private set; }

        public RedeSocialDominio() { }

        public RedeSocialDominio(RedeSocial id, string nome)
        {
            ID = id;
            Nome = nome;
        }
    }
}
