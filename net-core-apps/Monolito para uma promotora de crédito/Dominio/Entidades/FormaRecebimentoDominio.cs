using Dominio.Enum;

namespace Dominio
{
    public class FormaRecebimentoDominio : EntidadeBase
    {
        public new FormaRecebimento ID { get; private set; }

        public string Nome { get; private set; }
        
        public FormaRecebimentoDominio () {}

        public FormaRecebimentoDominio(FormaRecebimento id, string nome)
        {
            ID = id;
            Nome = nome;
        }
    }
}
