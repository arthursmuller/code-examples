using System;
using Dominio.Enum;

namespace Dominio
{
    public class AnexoDominio : EntidadeBase
    {
        public string Link { get; private set; }

        public TipoDocumento IdTipoDocumento { get; private set; }

        public int IdCliente { get; private set; }

        public DateTime DataCadastro { get; private set; } = DateTime.Now;

        private string extensao;
        public string Extensao { get => extensao; private set => extensao = value?.ToLower().Trim(); }

        public TipoDocumentoDominio TipoDocumento { get; private set; }

        public ClienteDominio Cliente { get; private set; }

        public AnexoDominio() { }

        public AnexoDominio(TipoDocumento idTipoDocumento, int idCliente, string link, string extensao)
        {
            IdTipoDocumento = idTipoDocumento;
            IdCliente = idCliente;
            Link = link;
            Extensao = extensao;
        }
    }
}
