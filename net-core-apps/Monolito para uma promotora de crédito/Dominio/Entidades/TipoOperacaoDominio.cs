using System.Collections.Generic;
using Dominio.Enum;

namespace Dominio
{
    public class TipoOperacaoDominio : EntidadeBase
    {
        public new TipoOperacao ID { get; private set; }

        public string Nome { get; private set; }

        public string Sigla { get; private set; }

        public IEnumerable<IntencaoOperacaoSituacaoPassoDominio> PassosIntencaoOperacao { get; private set; } 

        public TipoOperacaoDominio() { }

        public TipoOperacaoDominio(TipoOperacao id, string nome, string sigla)
        {
            ID = id;
            Nome = nome;
            Sigla = sigla;
        }
    }
}
