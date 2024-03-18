using Aplicacao.Model.OrgaoEmissorIdentificacao;
using Aplicacao.Model.UnidadeFederativa;
using Dominio;
using System;

namespace Aplicacao.Model.DocumentoIdentificacaoCliente
{
    public class DocumentoIdentificacaoClienteExibicaoModel
    {
        public int Id { get; set; }

        public TipoDocumentoModel TipoDocumento { get; set; }

        public OrgaoEmissorIdentificacaoModel OrgaoEmissor { get; set; }

        public UnidadeFederativaModel Uf { get; set; }

        public string Numero { get; set; }

        public DateTime DataEmissao { get; set; }

        public DocumentoIdentificacaoClienteExibicaoModel() { }

        public DocumentoIdentificacaoClienteExibicaoModel(DocumentoIdentificacaoClienteDominio documentoIdentificacao)
        {
            Id = documentoIdentificacao.ID;
            TipoDocumento = documentoIdentificacao.TipoDocumento == null ? null :
                new TipoDocumentoModel
                {
                    Id = (int)documentoIdentificacao.TipoDocumento.ID,
                    Codigo = documentoIdentificacao.TipoDocumento.Codigo,
                    Nome = documentoIdentificacao.TipoDocumento.Nome
                };
            OrgaoEmissor = documentoIdentificacao.OrgaoEmissor == null ? null :
                new OrgaoEmissorIdentificacaoModel
                {
                    Id = documentoIdentificacao.OrgaoEmissor.ID,
                    Codigo = documentoIdentificacao.OrgaoEmissor.Codigo,
                    Descricao = documentoIdentificacao.OrgaoEmissor.Descricao
                };
            Uf = documentoIdentificacao.Uf == null ? null :
                new UnidadeFederativaModel
                {
                    Id = documentoIdentificacao.Uf.ID,
                    Nome = documentoIdentificacao.Uf.Nome,
                    Sigla = documentoIdentificacao.Uf.Sigla
                };
            Numero = documentoIdentificacao.Numero;
            DataEmissao = documentoIdentificacao.DataEmissao;
        }
    }
}
