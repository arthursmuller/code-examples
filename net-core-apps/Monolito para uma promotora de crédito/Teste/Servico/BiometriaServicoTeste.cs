using Dominio;
using Dominio.Enum;
using Aplicacao.Servico;
using Xunit;
using System.Threading.Tasks;
using Infraestrutura.Providers.Unico;
using Moq;
using Microsoft.Extensions.Logging;
using Infraestrutura.Providers;
using System.Linq;
using Aplicacao.Model.Biometria;
using System;
using Infraestrutura.Providers.Unico.DTO;
using Dominio.Resource;
using Infraestrutura.Utils;

namespace Teste.Servico
{
    public class BiometriaServicoTeste : ServicoTesteBase
    {

        private readonly UsuarioDominio _usuarioTeste;
        private readonly Mock<IUnicoProvider> _unicoProvider = new Mock<IUnicoProvider>();
        private readonly Mock<IProviderAzure> _azureProvider = new Mock<IProviderAzure>();
        private BiometriaServico _biometriaServico;

        private string _codigoUnico = "ab1b299b-9781-48d9-bedc-f238f6f5cc57";
        private string _imagemAzure = "xxxxxxxxxxxxxx";

        public BiometriaServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();
        }

        [Fact]
        public async Task ExecutarBiometria_DadosCorretos_RetornaSucesso()
        {
            instanciarServico();
            defineRetornoSucessoCriarProcessoUnico();

            await criaRegistroBiometria();
            var final = _contexto
                            .BiometriaClientes
                            .FirstOrDefault();

            Assert.NotNull(final);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(BiometriaSituacao.Pendente, final.IdBiometriaSituacao);
            Assert.Equal(0, final.Score);
        }

        [Fact]
        public async Task ExecutarBiometria_BiometriaExistente_RetornaFalha()
        {
            instanciarServico();
            defineRetornoSucessoCriarProcessoUnico();
            defineRetornoSucessoBuscarProcessoUnico();

            var registro = await criaRegistroBiometria();
            await ExecutaWebhookSucesso(registro);
            var novaSolicitacao = await _biometriaServico.ExecutarBiometria();

            Assert.False(novaSolicitacao);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.Biometria_JaCadastrada));
        }

        [Fact]
        public async Task ExecutarBiometria_SemAnexo_RetornaFalha()
        {
            instanciarServico();

            var novaSolicitacao = await _biometriaServico.ExecutarBiometria();

            Assert.False(novaSolicitacao);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.Anexo_TipoDocumentoSefileBiometriaNaoLocalizado));
        }

        [Fact]
        public async Task ExecutarBiometria_SemImagemAzure_RetornaFalha()
        {
            instanciarServico();
            defineRetornoSucessoCriarProcessoUnico();
            setAzureProvide("");
            await criaAnexoSelfie();

            var novaSolicitacao = await _biometriaServico.ExecutarBiometria();

            Assert.False(novaSolicitacao);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.Selfie_NaoEncontrada));
        }

        [Fact]
        public async Task ExecutarBiometria_ProviderRetornaErroCentralizacaoRosto_RetornaFalha()
        {
            instanciarServico();
            defineRetornoErroCentralizacaoRostoCriarProcessoUnico();
            setAzureProvide(_imagemAzure);
            await criaAnexoSelfie();

            var novaSolicitacao = await _biometriaServico.ExecutarBiometria();

            Assert.False(novaSolicitacao);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.ProvedorUnico_SelfieNaoIdentificado));
        }

        [Fact]
        public async Task ExecutarBiometria_ProviderRetornaErroOutros_RetornaFalha()
        {
            instanciarServico();
            setAzureProvide(_imagemAzure);
            await criaAnexoSelfie();
            defineRetornoErroOutrosCriarProcessoUnico();

            var novaSolicitacao = await _biometriaServico.ExecutarBiometria();

            Assert.False(novaSolicitacao);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.ProvedorUnico_ErroAutenticarSefie));
        }

        [Fact]
        public async Task ObterSituacaoBiometria_BiometriaNaoExiste_RetornaFalha()
        {
            instanciarServico();

            var biometria = await _biometriaServico.ObterSituacaoBiometria();

            Assert.Null(biometria);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.Biometria_NaoEncontrada));
        }

        [Fact]
        public async Task ObterSituacaoBiometria_DadosCorretos_RetornaSucesso()
        {
            instanciarServico();
            defineRetornoSucessoCriarProcessoUnico();
            defineRetornoSucessoBuscarProcessoUnico();
            var registro = await criaRegistroBiometria();
            await ExecutaWebhookSucesso(registro);

            var biometria = await _biometriaServico.ObterSituacaoBiometria();

            Assert.NotNull(biometria);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(biometria.IdBiometriaSituacao, (int)BiometriaSituacao.Concluido);
        }

        [Fact]
        public async Task ObterSituacaoBiometria_ConsultaSituacaoUnicoFalha_RetornaFalha()
        {
            instanciarServico();
            defineRetornoSucessoCriarProcessoUnico();
            defineRetornoPendenteBuscarProcessoUnico();
            var registro = await criaRegistroBiometria();
            await ExecutaWebhookPendente(registro);
            defineRetornoNullSituacaoProcessoUnico();
            
            var biometria = await _biometriaServico.ObterSituacaoBiometria();

            Assert.NotNull(biometria);
            Assert.True(_mensagens.PossuiErros);
            Assert.Equal(biometria.IdBiometriaSituacao, (int)BiometriaSituacao.Pendente);
        }

        [Fact]
        public async Task ObterSituacaoBiometria_ConsultaSituacaoUnicoNaoConcluida_RetornaPendente()
        {
            instanciarServico();
            defineRetornoSucessoCriarProcessoUnico();
            defineRetornoPendenteBuscarProcessoUnico();
            var registro = await criaRegistroBiometria();
            await ExecutaWebhookPendente(registro);

            var biometria = await _biometriaServico.ObterSituacaoBiometria();

            Assert.NotNull(biometria);
            Assert.True(_mensagens.PossuiErros);
            Assert.Equal(biometria.IdBiometriaSituacao, (int)BiometriaSituacao.Pendente);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(string.Format(Mensagens.Biometria_SelfieAuntenticadaSituacao, ((BiometriaSituacao)BiometriaSituacao.Pendente).GetDescription())));
        }

        [Fact]
        public async Task ObterSituacaoBiometria_ConsultaSituacaoUnicoNaoConcluida_RetornaFalha()
        {
            instanciarServico();
            defineRetornoSucessoCriarProcessoUnico();
            defineRetornoFalhaBuscarProcessoUnico();
            var registro = await criaRegistroBiometria();
            await ExecutaWebhookPendente(registro);

            var biometria = await _biometriaServico.ObterSituacaoBiometria();

            Assert.NotNull(biometria);
            Assert.True(_mensagens.PossuiErros);
            Assert.Equal(biometria.IdBiometriaSituacao, (int)BiometriaSituacao.Pendente);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(string.Format(Mensagens.Biometria_SelfieAuntenticadaSituacao, ((BiometriaSituacao)BiometriaSituacao.Falha).GetDescription())));
        }

        [Fact]
        public async Task ProcessarWebhook_StatusConcluido_RetornaSucesso()
        {
            instanciarServico();
            defineRetornoSucessoCriarProcessoUnico();
            defineRetornoSucessoBuscarProcessoUnico();
            var registro = await criaRegistroBiometria();
            
            await ExecutaWebhookSucesso(registro);
            var final = _contexto.BiometriaClientes.FirstOrDefault();

            Assert.NotNull(final);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(100, final.Score);
        }

        private void instanciarServico()
            =>  _biometriaServico = new BiometriaServico(_mensagens
                                                    , _usuarioLogin
                                                    , _contexto
                                                    , _unicoProvider.Object
                                                    , _azureProvider.Object);

        private async Task<AnexoDominio> criaAnexoSelfie()
        {
            var tipoDocumento = new TipoDocumentoDominio(TipoDocumento.SelfieBiometria, "teste documento", "teste documento codigo");
            _contexto.TiposDocumento.Add(tipoDocumento);

            var anexo = new AnexoDominio(TipoDocumento.SelfieBiometria
                                        , _usuarioTeste.Cliente.ID
                                        , "http://teste"
                                        , ".jpeg");
            _contexto.Anexos.Add(anexo);
            await _contexto.SaveChangesAsync();
            return anexo;
        }

        private async Task<RegistroBiometriaUnicoDominio> criaRegistroBiometria()
        {
            setAzureProvide();
            await criaAnexoSelfie();
            await _biometriaServico.ExecutarBiometria();
            return _contexto.RegistrosBiometriaCliente.FirstOrDefault();
        }

        private void setAzureProvide(string retorno = null)
            => _azureProvider
                .Setup(s => s.ObterBase64Azure(It.IsAny<string>()))
                .ReturnsAsync(retorno ?? _imagemAzure);

        private async Task ExecutaWebhookSucesso(RegistroBiometriaUnicoDominio registro)
        {
            var dadosUnico = new DadosRetornoUnico()
            {
                id = registro.Codigo,
                status = (int)SituacaoRetornoUnico.Concluido,
                score = 100
            };
            var webhook = new BiometriaWebhookRetornoUnicoModel()
            {
                eventDate = DateTime.Now,
                @event = new Guid().ToString(),
                data = dadosUnico
            };
            await _biometriaServico.ProcessarRetornoWebhookUnico(webhook);
        }

        private async Task ExecutaWebhookPendente(RegistroBiometriaUnicoDominio registro)
        {
            var dadosUnico = new DadosRetornoUnico()
            {
                id = registro.Codigo,
                status = (int)SituacaoRetornoUnico.AguardandoExecucao,
                score = 0
            };

            var webhook = new BiometriaWebhookRetornoUnicoModel()
            {
                eventDate = DateTime.Now,
                @event = new Guid().ToString(),
                data = dadosUnico
            };

            await _biometriaServico.ProcessarRetornoWebhookUnico(webhook);
        }

        private void defineRetornoSucessoCriarProcessoUnico() 
            =>  _unicoProvider
                    .Setup(x => x.CriarProcesso(It.IsAny<UnicoRequisicaoCriarProcessoDto>()))
                    .ReturnsAsync(new UnicoResultadoCriarProcessoDto()
                    {
                        Id = _codigoUnico,
                        Error = null
                    });

        private void defineRetornoErroCentralizacaoRostoCriarProcessoUnico()
            =>  _unicoProvider
                      .Setup(x => x.CriarProcesso(It.IsAny<UnicoRequisicaoCriarProcessoDto>()))
                      .ReturnsAsync(new UnicoResultadoCriarProcessoDto()
                      {
                          Id = _codigoUnico,
                          Error = new UnicoResultadoErroDto()
                          {
                              Code = 500,
                              Description = "Erro centralizacao rosto"
                          }
                      });

        private void defineRetornoErroOutrosCriarProcessoUnico()
            =>  _unicoProvider
                    .Setup(x => x.CriarProcesso(It.IsAny<UnicoRequisicaoCriarProcessoDto>()))
                    .ReturnsAsync(new UnicoResultadoCriarProcessoDto()
                    {
                        Id = _codigoUnico,
                        Error = new UnicoResultadoErroDto()
                        {
                            Code = 1000,
                            Description = "Erro centralizacao rosto"
                        }
                    });

        private void defineRetornoSucessoBuscarProcessoUnico()
            =>   _unicoProvider
                    .Setup(x => x.BuscarProcesso(It.IsAny<string>()))
                    .ReturnsAsync(new UnicoResultadoBuscaProcessoDto()
                    {
                        FaceMatch = true,
                        HasBiometry = true,
                        Id = _codigoUnico,
                        Liveness = true,
                        OCRCode = 0,
                        Score = 100,
                        Status = (int)SituacaoRetornoUnico.Concluido
                    });

        private void defineRetornoPendenteBuscarProcessoUnico()
            =>  _unicoProvider
                    .Setup(x => x.BuscarProcesso(It.IsAny<string>()))
                    .ReturnsAsync(new UnicoResultadoBuscaProcessoDto()
                    {
                        FaceMatch = true,
                        HasBiometry = true,
                        Id = _codigoUnico,
                        Liveness = true,
                        OCRCode = 0,
                        Score = 3,
                        Status = (int)SituacaoRetornoUnico.AguardandoExecucao
                    });

        private void defineRetornoFalhaBuscarProcessoUnico()
            => _unicoProvider
                    .Setup(x => x.BuscarProcesso(It.IsAny<string>()))
                    .ReturnsAsync(new UnicoResultadoBuscaProcessoDto()
                    {
                        FaceMatch = true,
                        HasBiometry = true,
                        Id = _codigoUnico,
                        Liveness = true,
                        OCRCode = 0,
                        Score = 3,
                        Status = (int)SituacaoRetornoUnico.Erro
                    });

        private void defineRetornoNullSituacaoProcessoUnico()
            =>  _unicoProvider
                    .Setup(x => x.BuscarProcesso(It.IsAny<string>()))
                    .ReturnsAsync((UnicoResultadoBuscaProcessoDto)null);
    }
}