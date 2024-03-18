using System.Collections.Generic;

namespace Dominio.Entidades
{
    public class SituacaoEnvioDetalhesDominio: EntidadeBase
    {
        public string Descricao { get; set; }
        public IEnumerable<SmsMensagemDominio> SmsMensagens { get; set; }

        public SituacaoEnvioDetalhesDominio() {}

        public SituacaoEnvioDetalhesDominio(int id, string descricao)
        {
            ID = id;
            Descricao = descricao;
        }
    }
}