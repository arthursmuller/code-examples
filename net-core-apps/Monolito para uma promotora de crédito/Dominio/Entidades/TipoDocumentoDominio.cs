using Dominio.Enum;

namespace Dominio
{
    public class TipoDocumentoDominio : EntidadeBase
    {
        public new TipoDocumento ID { get; private set; }

        public string Nome { get; private set; }

        public string Codigo { get; private set; }
        public bool TipoDocumentoIdentificacaoPessoal { get; private set; }
        
        public TipoDocumentoDominio () {}

        public TipoDocumentoDominio(TipoDocumento id, string nome, string codigo, bool tipoDocumentoIdentificacaoPessoal = false)
        {
            ID = id;
            Nome = nome;
            Codigo = codigo;
            TipoDocumentoIdentificacaoPessoal = tipoDocumentoIdentificacaoPessoal;
        }
    }
}
