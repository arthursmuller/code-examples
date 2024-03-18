using Dominio.Enum;

namespace Dominio
{
    public class TipoContaDominio : EntidadeBase
    {
        public new TipoConta ID { get; private set; }

        public string Nome { get; private set; }

        public string Sigla { get; private set; }

        public TipoContaDominio() {}

        public TipoContaDominio(TipoConta id, string nome, string sigla)
        {
            ID = id;
            Nome = nome;
            Sigla = sigla;
        }
    }
}
