using System;
using Dominio.Enum;

namespace Dominio
{
    public class DocumentoIdentificacaoClienteDominio : EntidadeBase
    {
        public int IdCliente { get; private set; }

        public TipoDocumento IdTipoDocumento { get; private set; }

        public int IdOrgaoEmissor { get; private set; }

        public int IdUnidadeFederativa { get; private set; }

        public string Numero { get; private set; }

        public DateTime DataEmissao { get; private set; }

        public bool Deletado { get; private set; }

        public TipoDocumentoDominio TipoDocumento { get; private set; }

        public OrgaoEmissorIdentificacaoDominio OrgaoEmissor { get; private set; }

        public UnidadeFederativaDominio Uf { get; private set; }

        public ClienteDominio Cliente { get; private set; }

        public DocumentoIdentificacaoClienteDominio(int idCliente, TipoDocumento idTipoDocumento, int idOrgaoEmissor, int idUnidadeFederativa, string numero, DateTime dataEmissao)
        {
            IdCliente = idCliente;
            IdTipoDocumento = idTipoDocumento;
            IdOrgaoEmissor = idOrgaoEmissor;
            IdUnidadeFederativa = idUnidadeFederativa;
            Numero = numero;
            DataEmissao = dataEmissao;
            Deletado = false;
        }

        public void SetAtualizarDocumento(TipoDocumento idTipoDocumento, int idOrgaoEmissor, int idUnidadeFederativa, string numero, DateTime dataEmissao)
        {
            IdTipoDocumento = idTipoDocumento;
            IdOrgaoEmissor = idOrgaoEmissor;
            IdUnidadeFederativa = idUnidadeFederativa;
            Numero = numero;
            DataEmissao = dataEmissao;
            Deletado = false;

            atualizarData();
        }

        public void AlternarAtivo(bool ativo)
        {
            Deletado = !ativo;

            atualizarData();
        }

        private void atualizarData()
        {
            DataAtualizacao = DateTime.Now;
        }
    }
}
