using Aplicacao.Model.DocumentoIdentificacaoCliente;
using Aplicacao.Model.UnidadeFederativa;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers.Cliente.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class DocumentoIdentificacaoClienteServico : ServicoBase
    {
        private readonly IClienteServico _clienteServico;
        private readonly IAnexoServico _anexoServico;

        public DocumentoIdentificacaoClienteServico(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto,
            IClienteServico clienteServico, IAnexoServico anexoServico) : base(mensagens, usuarioLogin, contexto)
        {
            _clienteServico = clienteServico;
            _anexoServico = anexoServico;
        }

        public async Task<IEnumerable<DocumentoIdentificacaoClienteExibicaoModel>> BuscarDocumentoPorCliente()
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return null;

            var documentos = await obterConsultaBase()
                                .Where(d => d.IdCliente == usuario.Cliente.ID && !d.Deletado)
                                .ToListAsync();

            return documentos.Select(d => new DocumentoIdentificacaoClienteExibicaoModel(d));
        }

        public async Task<DocumentoIdentificacaoClienteExibicaoModel> GravarDocumento(DocumentoIdentificacaoClienteModel documentoGravacao, bool substituir = false)
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (usuario == null)
                return null;

            var documento = await _contexto.DocumentosCliente
                .FirstOrDefaultAsync(d =>
                    d.IdCliente.Equals(usuario.Cliente.ID)
                    && (int)d.IdTipoDocumento == documentoGravacao.IdTipoDocumento
                );

            if (documento != null)
            {
                if (documento.Numero == documentoGravacao.Numero && documento.IdOrgaoEmissor == documentoGravacao.IdOrgaoEmissor)
                    documento.AlternarAtivo(true);
                else if (documento.Deletado || substituir)
                {
                    if (substituir)
                        documento.AlternarAtivo(false);

                    documento = new DocumentoIdentificacaoClienteDominio(
                        usuario.Cliente.ID,
                        (Dominio.Enum.TipoDocumento)documentoGravacao.IdTipoDocumento,
                        documentoGravacao.IdOrgaoEmissor,
                        documentoGravacao.IdUnidadeFederativa,
                        documentoGravacao.Numero,
                        documentoGravacao.DataEmissao
                    );

                    await _contexto.AddAsync(documento);
                }
                else 
                {
                    _mensagens.AdicionarErro(Mensagens.Documento_JaExisteDeMesmoTipoVinculadoAoCliente, EnumMensagemTipo.negocio);
                    return null;
                }
            }
            else
            {
                documento = new DocumentoIdentificacaoClienteDominio(
                    usuario.Cliente.ID,
                    (Dominio.Enum.TipoDocumento)documentoGravacao.IdTipoDocumento,
                    documentoGravacao.IdOrgaoEmissor,
                    documentoGravacao.IdUnidadeFederativa,
                    documentoGravacao.Numero,
                    documentoGravacao.DataEmissao
                );

                await _contexto.AddAsync(documento);
            }

            await _contexto.SaveChangesAsync();

            return await obterDocumento(documento.ID, usuario.Cliente.ID);
        }

        public async Task<DocumentoIdentificacaoClienteExibicaoModel> AtualizarDocumento(int idDocumentoAtualizacao, DocumentoIdentificacaoClienteModel documentoAtualizacao)
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return null;

            var documentosCliente = await _contexto.DocumentosCliente.Where(d => d.IdCliente.Equals(usuario.Cliente.ID)).ToListAsync();

            var documento = documentosCliente.FirstOrDefault(d => d.ID.Equals(idDocumentoAtualizacao) && !d.Deletado);
            if (documento == null)
            {
                _mensagens.AdicionarErro(Mensagens.Documento_NaoLocalizado, EnumMensagemTipo.banco);

                return null;
            }

            if (documentosCliente.Any(d => (int)d.IdTipoDocumento == documentoAtualizacao.IdTipoDocumento && !documento.Deletado && d.ID != idDocumentoAtualizacao))
            {
                _mensagens.AdicionarErro(Mensagens.Documento_JaExisteDeMesmoTipoVinculadoAoCliente, EnumMensagemTipo.negocio);

                return null;
            }

            documento.SetAtualizarDocumento(
                (Dominio.Enum.TipoDocumento)documentoAtualizacao.IdTipoDocumento,
                documentoAtualizacao.IdOrgaoEmissor,
                documentoAtualizacao.IdUnidadeFederativa,
                documentoAtualizacao.Numero,
                documentoAtualizacao.DataEmissao
            );

            await _contexto.SaveChangesAsync();

            return await obterDocumento(documento.ID, usuario.Cliente.ID);
        }

        public async Task<bool> RemoverDocumento(int idDocumento)
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return false;

            var documento = await _contexto.DocumentosCliente.FirstOrDefaultAsync(d => d.ID.Equals(idDocumento) && d.IdCliente.Equals(usuario.Cliente.ID));
            if (documento == null)
            {
                _mensagens.AdicionarErro(Mensagens.Documento_NaoLocalizado, EnumMensagemTipo.banco);

                return true;
            }

            documento.AlternarAtivo(false);

            await _contexto.SaveChangesAsync();

            return true;
        }

        #region Importação

        public async Task ImportarDocumentos(DocumentoIdentificacaoDto documentoIdentificacaoCliente, IEnumerable<UnidadeFederativaModel> unidadesFederativas)
        {
            var documento = await converterDocumentoIdentificacao(documentoIdentificacaoCliente, unidadesFederativas);
            if (new DocumentoIdentificacaoClienteModelValidacao().Validate(documento).IsValid)
                await GravarDocumento(documento, true);
        }

        private async Task<DocumentoIdentificacaoClienteModel> converterDocumentoIdentificacao(DocumentoIdentificacaoDto documentoIdentificacaoCliente, IEnumerable<UnidadeFederativaModel> unidadesFederativas)
        {
            if (documentoIdentificacaoCliente == null)
            {
                return null;
            }

            var orgaoEmissor = await _clienteServico.ObterOrgaoEmissor(documentoIdentificacaoCliente.DescricaoOrgaoEmissor);
            var tipoDocumento = await _anexoServico.ObterTipoDocumentoPorDescricao(documentoIdentificacaoCliente.DescricaoTipoDocumento);

            var documentoIdentificacao = new DocumentoIdentificacaoClienteModel()
            {
                Numero = documentoIdentificacaoCliente.NroDocumentoIdentificacao,
                IdUnidadeFederativa = unidadesFederativas.FirstOrDefault(uf => uf.Sigla.Equals(documentoIdentificacaoCliente.UfDocumentoIdentificacao))?.Id ?? 0,
                IdOrgaoEmissor = orgaoEmissor.Id,
                IdTipoDocumento = tipoDocumento.Id,
            };

            if (documentoIdentificacaoCliente.DataEmissaoDocumentoIdentidade.HasValue)
                documentoIdentificacao.DataEmissao = documentoIdentificacaoCliente.DataEmissaoDocumentoIdentidade.Value;

            return documentoIdentificacao;
        }

        #endregion

        private IQueryable<DocumentoIdentificacaoClienteDominio> obterConsultaBase() => _contexto.DocumentosCliente
            .Include(d => d.TipoDocumento)
            .Include(d => d.OrgaoEmissor)
            .Include(d => d.Uf)
            .AsNoTracking();

        private async Task<DocumentoIdentificacaoClienteExibicaoModel> obterDocumento(int idDocumento, int idCliente)
        {
            var documento = await obterConsultaBase()
                                .FirstOrDefaultAsync(d => d.ID.Equals(idDocumento) && !d.Deletado && d.IdCliente.Equals(idCliente));

            if (documento == null)
                return null;

            return new DocumentoIdentificacaoClienteExibicaoModel(documento);
        }
    }
}
