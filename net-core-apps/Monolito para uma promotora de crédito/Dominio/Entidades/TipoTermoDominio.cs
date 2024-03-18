using Dominio.Enum;

namespace Dominio
{
    public class TipoTermoDominio : EntidadeBase
    {
        public new TipoTermo ID { get; private set; }

        public string Nome { get; private set; }

        public TipoTermoDominio() { }

        public TipoTermoDominio(TipoTermo id, string nome)
        {
            ID = id;
            Nome = nome;
        }
    }
}
