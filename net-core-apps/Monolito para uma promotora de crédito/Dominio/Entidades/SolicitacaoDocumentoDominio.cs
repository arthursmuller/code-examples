using System;
using Dominio.Enum;

namespace Dominio
{
    public class SolicitacaoDocumentoDominio : EntidadeBase
    {
        public TipoDocumento IdTipoDocumento { get; private set; }
        public int IdCliente { get; private set; }
        public DateTime DataSolicitacao { get; private set; }
        public bool Concluido { get; private set; } = false;
        public string Motivo { get; private set; }

        public TipoDocumentoDominio TipoDocumento { get; private set; }
        public ClienteDominio Cliente { get; private set; }
        public SolicitacaoDocumentoDominio () {}
        public SolicitacaoDocumentoDominio(TipoDocumento idTipoDocumento, int idCliente, string solicitante, string motivo)
        {
            IdTipoDocumento = idTipoDocumento;
            IdCliente = idCliente;
            UsuarioAtualizacao = solicitante;
            Motivo = motivo;

            DataSolicitacao = DateTime.Now;
        }

        public void Concluir()
        {
            Concluido = true;
            setDataAtualizacao();
        }
    }
}
