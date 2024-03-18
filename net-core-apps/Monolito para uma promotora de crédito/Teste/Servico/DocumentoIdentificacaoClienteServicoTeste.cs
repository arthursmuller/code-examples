using Aplicacao.Model.DocumentoIdentificacaoCliente;
using Aplicacao.Servico;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class DocumentoIdentificacaoClienteServicoTeste : ServicoTesteBase
    {
        private readonly DocumentoIdentificacaoClienteServico _documentoIdentificacaoClienteServico;
        private UsuarioDominio _usuarioTeste;

        public DocumentoIdentificacaoClienteServicoTeste() : base()
        {
            criarDadosRelacionamentos();

            _usuarioTeste = CriarUsuarioTeste();
            var anexoServicoMock = new Mock<IAnexoServico>();

            _documentoIdentificacaoClienteServico = new DocumentoIdentificacaoClienteServico(_mensagens, _usuarioLogin, _contexto, It.IsAny<IClienteServico>(), anexoServicoMock.Object);
        }

        [Fact]
        public async Task BuscarDocumentoPorCliente_QuandoAchado_DeveRetornar()
        {
            await criarDocumentoTeste();
            var resultado = await _documentoIdentificacaoClienteServico.BuscarDocumentoPorCliente();

            Assert.Equal(1, resultado.Count());
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task GravarDocumento_QuandoNaoExiste_DeveGravar()
        {
            var requisicao = new DocumentoIdentificacaoClienteModel()
            {
                IdTipoDocumento = 1,
                IdOrgaoEmissor = 1,
                IdUnidadeFederativa = 1,
                Numero = "11122233344",
                DataEmissao = DateTime.Now,
            };

            var resultado = await _documentoIdentificacaoClienteServico.GravarDocumento(requisicao);

            Assert.Equal(1, (await consultarDocumentosBanco()).Count());
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task GravarDocumento_QuandoJaExisteInativo_DeveAtivar()
        {
            var documento = await criarDocumentoTeste();
            documento.AlternarAtivo(false);
            await _contexto.SaveChangesAsync();

            var requisicao = new DocumentoIdentificacaoClienteModel()
            {
                IdTipoDocumento = (int)documento.IdTipoDocumento,
                IdOrgaoEmissor = 1,
                IdUnidadeFederativa = 1,
                Numero = documento.Numero,
                DataEmissao = documento.DataEmissao,
            };

            var resultado = await _documentoIdentificacaoClienteServico.GravarDocumento(requisicao);

            Assert.Equal(1, (await consultarDocumentosBanco()).Count());
            Assert.Equal((await consultarDocumentosBanco()).Count(d => d.Deletado), 0);
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task GravarDocumento_QuandoJaExisteDoMesmoTipo_DeveRetornarErro()
        {
            var documento = await criarDocumentoTeste();

            var requisicao = new DocumentoIdentificacaoClienteModel()
            {
                IdTipoDocumento = (int)Dominio.Enum.TipoDocumento.RegistroIdentidadeCivil,
                IdOrgaoEmissor = 1,
                IdUnidadeFederativa = 1,
                Numero = "11122233344",
                DataEmissao = documento.DataEmissao,
            };

            var resultado = await _documentoIdentificacaoClienteServico.GravarDocumento(requisicao);

            Assert.Equal(1, (await consultarDocumentosBanco()).Count());
            Assert.True(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task AtualizarDocumento_SeFoiExcluido_DeveRetornarErro()
        {
            var documento = await criarDocumentoTeste();
            documento.AlternarAtivo(false);
            await _contexto.SaveChangesAsync();

            var requisicao = new DocumentoIdentificacaoClienteModel()
            {
                IdTipoDocumento = 1,
                IdOrgaoEmissor = 1,
                IdUnidadeFederativa = 1,
                Numero = "11122233344",
                DataEmissao = documento.DataEmissao,
            };

            var resultado = await _documentoIdentificacaoClienteServico.AtualizarDocumento(documento.ID, requisicao);

            Assert.True(_mensagens.PossuiErros);
            Assert.Equal(1, (await consultarDocumentosBanco()).Count());
        }

        [Fact]
        public async Task AtualizarDocumento_QuandoJaExisteOutroDocumentoComMesmoTipo_DeveRetornarErro()
        {
            var documentoExistente = new DocumentoIdentificacaoClienteDominio(1, Dominio.Enum.TipoDocumento.IdentidadeProfissional, 1, 1, "99988877766", DateTime.Now);
            await _contexto.DocumentosCliente.AddAsync(documentoExistente);
            await _contexto.SaveChangesAsync();

            var documentoParaAtualizar = await criarDocumentoTeste();

            var requisicao = new DocumentoIdentificacaoClienteModel()
            {
                IdTipoDocumento = (int)Dominio.Enum.TipoDocumento.IdentidadeProfissional,
                IdOrgaoEmissor = documentoParaAtualizar.IdOrgaoEmissor,
                IdUnidadeFederativa = documentoParaAtualizar.IdUnidadeFederativa,
                Numero = "11122233344",
                DataEmissao = documentoParaAtualizar.DataEmissao,
            };

            var resultado = await _documentoIdentificacaoClienteServico.AtualizarDocumento(documentoParaAtualizar.ID, requisicao);

            Assert.True(_mensagens.PossuiErros);
            Assert.Equal(2, (await consultarDocumentosBanco()).Count());
        }

        [Fact]
        public async Task AtualizarDocumento_QuandoExistente_DevePersistir()
        {
            var documentoParaAtualizar = await criarDocumentoTeste();

            var requisicao = new DocumentoIdentificacaoClienteModel()
            {
                IdTipoDocumento = (int)documentoParaAtualizar.IdTipoDocumento,
                IdOrgaoEmissor = documentoParaAtualizar.IdOrgaoEmissor,
                IdUnidadeFederativa = documentoParaAtualizar.IdUnidadeFederativa,
                Numero = "11122233344",
                DataEmissao = documentoParaAtualizar.DataEmissao,
            };

            var resultado = await _documentoIdentificacaoClienteServico.AtualizarDocumento(documentoParaAtualizar.ID, requisicao);

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(1, (await consultarDocumentosBanco()).Count());
        }

        [Fact]
        public async Task RemoverDocumento_QuandoExistente_DeveApenasMarcarComoExcludo()
        {
            var documentoParaRemover = await criarDocumentoTeste();

            var resultado = await _documentoIdentificacaoClienteServico.RemoverDocumento(documentoParaRemover.ID);

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(1, (await consultarDocumentosBanco()).Count());
            Assert.Equal(0, (await consultarDocumentosBanco()).Count(d => !d.Deletado));
        }

        private async Task<List<DocumentoIdentificacaoClienteDominio>> consultarDocumentosBanco() => await _contexto.DocumentosCliente.ToListAsync();

        private async Task<DocumentoIdentificacaoClienteDominio> criarDocumentoTeste()
        {
            var documento = new DocumentoIdentificacaoClienteDominio(1, Dominio.Enum.TipoDocumento.RegistroIdentidadeCivil, 1, 1, "99988877766", DateTime.Now);
            await _contexto.DocumentosCliente.AddAsync(documento);
            await _contexto.SaveChangesAsync();
            return documento;
        }

        private void criarDadosRelacionamentos()
        {
            _contexto.AddAsync(new OrgaoEmissorIdentificacaoDominio("SJS", "SJS"));
            _contexto.UnidadesFederativas.Add(new UnidadeFederativaDominio("Rio Grande do Sul", "RS"));
            _contexto.Convenios.Add(new ConvenioDominio(Dominio.Enum.Convenio.INSS, "INSS", "000020", ""));
            _contexto.SaveChanges();

            _contexto.ConvenioOrgaos.Add(new ConvenioOrgaoDominio("20101", "00394411000109", "PRESIDENCIA DA REPUBLICA", Dominio.Enum.Convenio.INSS, 1));
            _contexto.TiposDocumento.Add(new TipoDocumentoDominio(Dominio.Enum.TipoDocumento.IdentidadeProfissional, "CPF", "cpf"));
            _contexto.SaveChanges();

            _contexto.TiposDocumento.Add(new TipoDocumentoDominio(Dominio.Enum.TipoDocumento.RegistroIdentidadeCivil, "RG", "rg"));
            _contexto.SaveChanges();
        }
    }
}
