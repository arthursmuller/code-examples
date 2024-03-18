using Aplicacao;
using Aplicacao.Model.Anexo;
using Aplicacao.Servico;
using Dominio;
using Dominio.Resource;
using Infraestrutura.Providers;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class AnexoServicoTeste : ServicoTesteBase
    {
        private const string EXTENSAO = "png";

        private readonly UsuarioDominio _usuarioTeste;
        private readonly AnexoServico _anexoServico;
        private readonly string _linkArquivo = "http://linkdomeuarquivo";
        private readonly string _anexoBase64 = "R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";

        public AnexoServicoTeste() : base()
        {
            var providerAzureMock = new Mock<IProviderAzure>();
            var notificacaoServicoMock = new Mock<INotificacaoServico>();

            _usuarioTeste = CriarUsuarioTeste();

            UsuarioLoginDominio usuarioLogin = new UsuarioLoginDominio { IdUsuario = _usuarioTeste.ID, Nome = _usuarioTeste.Nome };

            _anexoServico = new AnexoServico(_mensagens, usuarioLogin, _contexto, providerAzureMock.Object, notificacaoServicoMock.Object);
        }

        [Fact]
        public async Task GravarArquivo_QuandoCodigoDocumentoInvalido_DeveRetornarErro()
        {
            await criarTiposDocumentos();
            AnexoCriacaoModel requisicao = new AnexoCriacaoModel()
            {
                IdTipoDocumento = 0,
                AnexoBase64 = "423",
                Extensao = "png",
            };

            var resultado = await _anexoServico.GravarArquivo(requisicao);

            Assert.True(_mensagens.PossuiErros);
            Assert.True(_mensagens.TemErroFormulario);
            Assert.Null(resultado);
        }

        [Fact]
        public async Task DeletarArquivo_QuandoDoUsuario_DeveExcluir()
        {
            var tipoDocumento = await criarTiposDocumentos();
            AnexoCriacaoModel requisicao = new AnexoCriacaoModel()
            {
                IdTipoDocumento = (int)tipoDocumento.ID,
                AnexoBase64 = _anexoBase64,
                Extensao = "png",
            };

            var anexo = await _anexoServico.GravarArquivo(requisicao);
            var resultado = await _anexoServico.DeletarAnexo(anexo.Id);

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
        }

        [Fact]
        public async Task DeletarArquivo_QuandoDeOutroUsuario_DeveRetornarErro()
        {
            var tipoDocumento = await criarTiposDocumentos();

            var outroUsuario = await CriarUsuarioTesteAsync();
            var novoAnexo = new AnexoDominio(tipoDocumento.ID, outroUsuario.ID, "qqr", ".ext");
            await _contexto.AddAsync(novoAnexo);
            await _contexto.SaveChangesAsync();

            var resultado = await _anexoServico.DeletarAnexo(novoAnexo.ID);

            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), e => e.Mensagem.Equals(Mensagens.Usuario_UsuarioLogadoDiferenteDoUsuarioOrigem));
            Assert.False(resultado);
        }

        [Fact]
        public async Task GravarArquivo_QuandoCorretoCodigoDocumento_DevePersistir()
        {
            var tipoDocumento = await criarTiposDocumentos();
            AnexoCriacaoModel requisicao = new AnexoCriacaoModel()
            {
                IdTipoDocumento = (int)tipoDocumento.ID,
                AnexoBase64 = _anexoBase64,
                Extensao = "png",
            };

            var resultado = await _anexoServico.GravarArquivo(requisicao);

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
        }

        [Fact]
        public async Task GravarArquivo_QuandoInformadoIdTipoDocument_DevePersistir()
        {
            var tipoDocumento = await criarTiposDocumentos();
            AnexoCriacaoModel requisicao = new AnexoCriacaoModel()
            {
                IdTipoDocumento = (int)tipoDocumento.ID,
                AnexoBase64 = _anexoBase64,
                Extensao = "png",
            };

            var resultado = await _anexoServico.GravarArquivo(requisicao);

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
        }

        [Fact]
        public async Task BuscarAnexo_QuandoAnexoDoUsuarioLogado_DeveRetornar()
        {
            var tipoDocumento = await criarTiposDocumentos();
            var anexoTeste = new AnexoDominio(tipoDocumento.ID, 1, _linkArquivo, EXTENSAO);
            await _contexto.Anexos.AddAsync(anexoTeste);
            await _contexto.SaveChangesAsync();

            var resultado = await _anexoServico.BuscarAnexo(anexoTeste.ID);

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
        }

        [Fact]
        public async Task SolicitarAnexo_QuandoNovaSolicitacao_DevePesistir()
        {
            var tipoDocumento = await criarTiposDocumentos();

            var solicitacao = new AnexoSolicitacaoModel()
            {
                IdCliente = _usuarioTeste.Cliente.ID,
                IdTipoDocumento = tipoDocumento.ID,
                Solicitante = "Bob"
            };

            var resultado = await _anexoServico.SolicitarAnexoParaCliente(solicitacao);

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
            Assert.Single(_contexto.SolicitacoesDocumento.ToList());
        }

        [Fact]
        public async Task SolicitarAnexo_QuandoSolicitacaojaExistente_DeveRetornarErro()
        {
            var tipoDocumento = await criarTiposDocumentos();

            var solicitacao = new AnexoSolicitacaoModel()
            {
                IdCliente = _usuarioTeste.Cliente.ID,
                IdTipoDocumento = tipoDocumento.ID,
                Solicitante = "Bob"
            };

            await _anexoServico.SolicitarAnexoParaCliente(solicitacao);
            var resultado = await _anexoServico.SolicitarAnexoParaCliente(solicitacao);

            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(String.Format(Mensagens.Solicitacao_EmAndamento, tipoDocumento.Nome)));
            Assert.False(resultado);
            Assert.Single(_contexto.SolicitacoesDocumento.ToList());
        }

        private async Task<TipoDocumentoDominio> criarTiposDocumentos()
        {
            var documentoTeste = new TipoDocumentoDominio(Dominio.Enum.TipoDocumento.CarteiraDeTrabalho, "teste documento", "teste documento codigo");
            await _contexto.TiposDocumento.AddAsync(documentoTeste);

            await _contexto.SaveChangesAsync();

            return documentoTeste;
        }
    }
}
