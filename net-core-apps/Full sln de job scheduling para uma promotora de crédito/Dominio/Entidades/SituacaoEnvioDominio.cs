using System.Collections.Generic;

namespace Dominio.Entidades
{
    public class SituacaoEnvioDominio: EntidadeBase
    {
        public string Descricao { get; private set; }
        public IEnumerable<SmsMensagemDominio> SmsMensagens { get; private  set; }

        public SituacaoEnvioDominio() {}

        public SituacaoEnvioDominio(int id, string descricao)
        {
            ID = id;
            Descricao = descricao;
        }
    }
}