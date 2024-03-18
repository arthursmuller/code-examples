using Dominio.Enum;

namespace Dominio
{

    public class BiometriaSituacaoDominio : EntidadeBase
    {
        public new BiometriaSituacao ID { get; private set; }

        public string Descricao { get; private set; }

        public BiometriaSituacaoDominio() { }

        public BiometriaSituacaoDominio(BiometriaSituacao id, string descricao)
        {
            ID = id;
            Descricao = descricao;
        }
    }

}
