using Aplicacao.Model.Anexo;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class AnexoServico : ServicoBase, IAnexoServico
    {
        private readonly IProviderAzure _providerAzure;
        private readonly INotificacaoServico _notificacaoServico;

        public AnexoServico(
            IBemMensagens mensagens,
            IUsuarioLogin usuarioLogin,
            PlataformaClienteContexto contexto,
            IProviderAzure providerAzure,
            INotificacaoServico notificacaoServico
        ) : base(mensagens, usuarioLogin, contexto)
        {
            _providerAzure = providerAzure;
            _notificacaoServico = notificacaoServico;
        }

        public async Task<AnexoModel> GravarArquivo(AnexoCriacaoModel anexo)
        {
            var usuarioAutenticado = await ObterDadosUsuarioAutenticado();

            if (_mensagens.PossuiErros)
                return null;

            if (!await validarSeTipoDocumentoValido(anexo.IdTipoDocumento))
                return null;

            var linkAnexo = await anexarArquivo(anexo.AnexoBase64, anexo.Extensao);

            var novoAnexo = await _contexto.AddAsync(
                new AnexoDominio(
                    (TipoDocumento)anexo.IdTipoDocumento,
                    usuarioAutenticado.Cliente.ID,
                    linkAnexo,
                    anexo.Extensao
                ));

            await _contexto.SaveChangesAsync();

            return converterParaModel(await obterConsultaBase().FirstOrDefaultAsync(a => a.ID.Equals(novoAnexo.Entity.ID)));
        }

        public async Task<IEnumerable<AnexoModel>> BuscarAnexosPorUsuarioAutenticado()
        {
            var anexos = await obterConsultaBase()
                                .Where(x => x.Cliente.IdUsuario.Equals(_usuarioLogin.IdUsuario))
                                .ToListAsync();

            if (anexos == null || !anexos.Any())
            {
                _mensagens.AdicionarAlerta(string.Format(Mensagens.Anexo_NaoForamEncontradosAnexosParaUsuario, _usuarioLogin.IdUsuario), EnumMensagemTipo.banco);

                return null;
            }

            return converterAnexosParaModel(anexos);
        }

        public async Task<IEnumerable<AnexoModel>> BuscarAnexosPorCpfUsuario(string cpf)
        {
            List<AnexoDominio> anexos = await obterConsultaBase()
                                                .Where(x => x.Cliente.Usuario.CPF.Equals(cpf))
                                                .ToListAsync();

            if (anexos == null || !anexos.Any())
            {
                _mensagens.AdicionarAlerta(string.Format(Mensagens.Anexo_NaoForamEncontradosAnexosParaUsuario, cpf), EnumMensagemTipo.banco);

                return null;
            }

            return converterAnexosParaModel(anexos);
        }

        public async Task<AnexoModel> BuscarAnexo(int idAnexo)
        {
            var anexo = await obterConsultaBase()
                .FirstOrDefaultAsync(x => x.ID.Equals(idAnexo));

            if (anexo == null)
            {
                _mensagens.AdicionarErro(string.Format(Mensagens.Anexo_IdNaoEncontrado, idAnexo), EnumMensagemTipo.banco);

                return null;
            }

            if (!verificarPermissao(anexo))
                return null;

            var anexoModel = converterParaModel(anexo);

            anexoModel.AnexoBase64 = await _providerAzure.ObterBase64Azure(anexo.Link);

            return anexoModel;
        }

        public async Task<bool> DeletarAnexo(int id)
        {
            var anexo = await _contexto.Anexos
                .Include(c => c.Cliente)
                .Where(a => a.ID.Equals(id))
                .FirstOrDefaultAsync();

            if (anexo == null)
            {
                _mensagens.AdicionarErro(string.Format(Mensagens.Anexo_IdNaoEncontrado, id), EnumMensagemTipo.banco);
                return false;
            }

            if (!verificarPermissao(anexo))
                return false;

            _contexto.Remove(anexo);

            _providerAzure.ExcluirArquivo(anexo.Link);

            await _contexto.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<TipoDocumentoModel>> ListarTiposDocumentos()
        {
            var opcoes = await _contexto.TiposDocumento.AsNoTracking().ToListAsync();

            return opcoes.Select(op =>
                new TipoDocumentoModel()
                {
                    Id = (int)op.ID,
                    Nome = op.Nome,
                    Codigo = op.Codigo,
                });
        }

        public async Task<TipoDocumentoModel> ObterTipoDocumentoPorDescricao(string descricao)
        {
            var tipoDocumento = await _contexto.TiposDocumento.AsNoTracking().FirstOrDefaultAsync(t => t.Nome.Equals(descricao));

            if (tipoDocumento == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Anexo_TipoDocumentoNaoLocalizado, EnumMensagemTipo.banco);

                return new TipoDocumentoModel();
            }

            return new TipoDocumentoModel()
            {
                Id = (int)tipoDocumento.ID,
                Nome = tipoDocumento.Nome,
                Codigo = tipoDocumento.Codigo,
            };
        }

        public async Task<bool> SolicitarAnexoParaCliente(AnexoSolicitacaoModel solicitacao)
        {
            var tipoDocumento = await _contexto.TiposDocumento.FirstOrDefaultAsync(d => d.ID == solicitacao.IdTipoDocumento);
            if (tipoDocumento is null)
            {
                _mensagens.AdicionarErro(Mensagens.Anexo_TipoDocumentoNaoLocalizado, EnumMensagemTipo.banco);
                return false;
            }

            var cliente = await _contexto.Clientes.FirstOrDefaultAsync(c => c.ID == solicitacao.IdCliente);

            if (cliente is null)
            {
                _mensagens.AdicionarErro(Mensagens.Cliente_NaoEncontrado, EnumMensagemTipo.negocio);
                return false;
            }

            if (await _contexto.SolicitacoesDocumento.AnyAsync(s => s.IdCliente == solicitacao.IdCliente 
                && s.IdTipoDocumento == solicitacao.IdTipoDocumento && !s.Concluido))
            {
                _mensagens.AdicionarErro(String.Format(Mensagens.Solicitacao_EmAndamento, tipoDocumento.Nome), EnumMensagemTipo.negocio);
                return false;
            }

            await _contexto.AddAsync(new SolicitacaoDocumentoDominio(solicitacao.IdTipoDocumento, solicitacao.IdCliente, solicitacao.Solicitante, solicitacao.Motivo));

            await _contexto.SaveChangesAsync();

            var chaves = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["documento"] = tipoDocumento,
            };
            await _notificacaoServico.GerarNotificacao(cliente.IdUsuario, Dominio.Enum.Notificacoes.NotificacaoFinalidade.SolicitacaoDocumentos, chaves);

            return true;
        }

        private IQueryable<AnexoDominio> obterConsultaBase()
        {
            return _contexto.Anexos
                .Include(anexo => anexo.Cliente.Usuario)
                .Include(anexos => anexos.TipoDocumento)
                .AsNoTracking();
        }

        private async Task<bool> validarSeTipoDocumentoValido(int idTipoDocumento)
        {
            var tipoDocumento = await _contexto.TiposDocumento
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(t => (int)t.ID == idTipoDocumento);

            if (tipoDocumento is null || (int)tipoDocumento.ID != idTipoDocumento)
            {
                _mensagens.AdicionarErro(Mensagens.Anexo_TipoDocumentoNaoLocalizado, EnumMensagemTipo.formulario);
                return false;
            }

            return true;
        }

        private async Task<string> anexarArquivo(string base64, string extensao)
        {
            var anexoByte = Convert.FromBase64String(base64);
            return await _providerAzure.SalvarArquivo(anexoByte, extensao);
        }

        private IEnumerable<AnexoModel> converterAnexosParaModel(IEnumerable<AnexoDominio> anexosDominio)
        {
            return anexosDominio.Select(x => converterParaModel(x));
        }

        private AnexoModel converterParaModel(AnexoDominio anexoDominio)
        {
            return new AnexoModel()
            {
                Id = anexoDominio.ID,
                LinkAnexo = anexoDominio.Link,
                TipoDocumento = new TipoDocumentoModel() { Id = (int)anexoDominio.IdTipoDocumento, Nome = anexoDominio.TipoDocumento.Nome, Codigo = anexoDominio.TipoDocumento.Codigo },
                IdUsuario = anexoDominio.IdCliente,
                Usuario = anexoDominio.Cliente == null ? null : UsuarioServico.ConverterParaModel(anexoDominio.Cliente.Usuario),
                Extensao = anexoDominio.Extensao,
                DataCadastro = anexoDominio.DataCadastro
            };
        }

        private bool verificarPermissao(AnexoDominio anexo)
        {
            if (_usuarioLogin.IdUsuario != anexo.Cliente.IdUsuario)
            {
                _mensagens.AdicionarErro(Mensagens.Usuario_UsuarioLogadoDiferenteDoUsuarioOrigem, EnumMensagemTipo.negocio);

                return false;
            }

            return true;
        }
    }
}
