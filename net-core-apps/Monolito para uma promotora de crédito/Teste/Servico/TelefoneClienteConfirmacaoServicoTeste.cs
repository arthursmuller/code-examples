using Aplicacao.Model.TelefoneClienteConfirmacao;
using Aplicacao.Servico;
using B.Configuracao;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Infraestrutura.Fila.Sms;
using Infraestrutura.Fila.Whatsapp;
using Infraestrutura.Fila.TorpedoVoz;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SharedKernel.ValueObjects.v2;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class TelefoneClienteConfirmacaoServicoTeste : ServicoTesteBase
    {
        private const int TOKEN_QUANTIDADE_DIGITOS = 4;
        private const string TOKEN_PADRAO = "1234";

        private TelefoneClienteConfirmacaoServico _telefoneClienteConfirmacaoServico;
        private ISmsServico _servicoSms;
        private IWhatsappServico _servicoWhatsapp;
        private ITorpedoVozServico _servicoTorpedoVoz;
        private IProducerSms _producerSms = It.IsAny<IProducerSms>();
        private IProducerWhatsapp _producerWhatsapp = It.IsAny<IProducerWhatsapp>();
        private IProducerTorpedoVoz _producerTorpedoVoz = It.IsAny<IProducerTorpedoVoz>();
        
        private ILogger<TelefoneClienteConfirmacaoServico> _logger = new Mock<ILogger<TelefoneClienteConfirmacaoServico>>().Object;

        [Fact]
        public async Task SolicitarConfirmacaoDePropriedade_TelefoneInexistente_DeveRetornarNullComMensagemDeErro()
        {
            instanciarServico();

            var solicitacao = await _telefoneClienteConfirmacaoServico.SolicitarConfirmacaoDePropriedade(1, new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.Sms });

            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erros => erros.Mensagem.Equals(Mensagens.Telefone_NaoLocalizado));
        }

        [Fact]
        public async Task SolicitarConfirmacaoDePropriedade_TipoSolicitacaoSMSComConvencional_DeveRetornarNullComMensagemDeErro()
        {
            instanciarServico();
            var telefone = await criarTelefoneConvencional();

            var solicitacao = await _telefoneClienteConfirmacaoServico.SolicitarConfirmacaoDePropriedade(telefone.ID, new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.Sms });

            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erros => erros.Mensagem.Equals(Mensagens.TelefoneConfirmacao_ConfirmacaoPorTelefonemaSomenteParaTelefoneCelular));
        }

        [Fact]
        public async Task SolicitarConfirmacaoDePropriedade_SolicitacaoTipoNaoImplementado_DeveRetornarFalseComMensagemDeErro()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("configuracao-tokenconfirmacaotelefone-quantidadedigitos", TOKEN_QUANTIDADE_DIGITOS.ToString())
            }, null, null);

            instanciarServico(configuracao);
            await criarTemplateSms();

            var telefone = await criarTelefone();

            var solicitacaoEnvio = await _telefoneClienteConfirmacaoServico.SolicitarConfirmacaoDePropriedade(telefone.ID, new TelefoneClienteSolicitacaoConfirmacaoEnvioModel());

            Assert.NotNull(solicitacaoEnvio);
            Assert.False(solicitacaoEnvio.SolicitacaoEnviada);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erros => erros.Mensagem.Equals(Mensagens.TipoSolicitacaoConfirmacao_NaoImplementado));
        }

        [Fact]
        public async Task SolicitarConfirmacaoDePropriedade_ExceptionNoProducerSms_DeveRetornarFalseComMensagemDeErro()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("configuracao-tokenconfirmacaotelefone-quantidadedigitos", TOKEN_QUANTIDADE_DIGITOS.ToString())
            }, null, null);

            instanciarServico(configuracao);
            await criarTemplateSms();
            var telefone = await criarTelefone();

            var solicitacaoEnvio = await _telefoneClienteConfirmacaoServico.SolicitarConfirmacaoDePropriedade(telefone.ID, new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.Sms });

            Assert.NotNull(solicitacaoEnvio);
            Assert.False(solicitacaoEnvio.SolicitacaoEnviada);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erros => erros.Mensagem.Equals(Mensagens.TelefoneConfirmacao_NaoFoiPossivelEnviarTokenPorSms));
        }

        [Fact]
        public async Task SolicitarConfirmacaoDePropriedadeSMS_DadosValidos_DeveRetornarTrueSemMensagemDeErro()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("configuracao-tokenconfirmacaotelefone-quantidadedigitos", TOKEN_QUANTIDADE_DIGITOS.ToString())
            }, null, null);

            _producerSms = new Mock<IProducerSms>().Object;
            instanciarServico(configuracao);
            await criarTemplateSms();
            
            var telefone = await criarTelefone();

            var solicitacaoEnvio = await _telefoneClienteConfirmacaoServico.SolicitarConfirmacaoDePropriedade(telefone.ID, new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.Sms });

            await assertSucesso(solicitacaoEnvio);
        }

        [Fact]
        public async Task SolicitarConfirmacaoDePropriedadeWhatsApp_DadosValidos_DeveRetornarTrueSemMensagemDeErro()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("configuracao-tokenconfirmacaotelefone-quantidadedigitos", TOKEN_QUANTIDADE_DIGITOS.ToString()),
                new KeyValuePair<string, string>("configuracao-whatsapp-quantidademaximaenviosdia", "1"),
                new KeyValuePair<string, string>("configuracao-whatsapp-intervalominimoreenviosegundos", "999999" )
            }, null, null);

            _producerWhatsapp = new Mock<IProducerWhatsapp>().Object;
            instanciarServico(configuracao);
            await criarTemplateWhatsApp();
            
            var telefone = await criarTelefone();

            var solicitacaoEnvio = await _telefoneClienteConfirmacaoServico.SolicitarConfirmacaoDePropriedade(telefone.ID, new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.WhatsApp });

            await assertSucesso(solicitacaoEnvio);
        }

        [Fact] 
        public async Task SolicitarConfirmacaoDePropriedadeWhatsApp_ExceptionNoProducerWhatsApp_DeveRetornarFalseComMensagemDeErro()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("configuracao-tokenconfirmacaotelefone-quantidadedigitos", TOKEN_QUANTIDADE_DIGITOS.ToString()),
                new KeyValuePair<string, string>("configuracao-whatsapp-quantidademaximaenviosdia", "3"),
                new KeyValuePair<string, string>("configuracao-whatsapp-intervalominimoreenviosegundos", "999999" )
            }, null, null);

            instanciarServico(configuracao);
            await criarTemplateWhatsApp();
            var telefone = await criarTelefone();

            var solicitacaoEnvio = await _telefoneClienteConfirmacaoServico.SolicitarConfirmacaoDePropriedade(telefone.ID, new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.WhatsApp });

            Assert.NotNull(solicitacaoEnvio);
            Assert.False(solicitacaoEnvio.SolicitacaoEnviada);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erros => erros.Mensagem.Equals(Mensagens.TelefoneConfirmacao_NaoFoiPossivelEnviarTokenPorWhatsapp));
        }

        [Fact]
        public async Task SolicitarConfirmacaoDePropriedadeTorpedoVoz_DadosValidos_DeveRetornarTrueSemMensagemDeErro()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("configuracao-tokenconfirmacaotelefone-quantidadedigitos", TOKEN_QUANTIDADE_DIGITOS.ToString()),
                new KeyValuePair<string, string>("configuracao-torpedovoz-quantidademaximaenviosdia", "2"),
                new KeyValuePair<string, string>("configuracao-torpedovoz-intervalominimoreenviosegundos", "999999" )
            }, null, null);

            _producerTorpedoVoz = new Mock<IProducerTorpedoVoz>().Object;
            instanciarServico(configuracao);
            await criarTemplateTorpedoVoz();
            
            var telefone = await criarTelefone();

            var solicitacaoEnvio = await _telefoneClienteConfirmacaoServico.SolicitarConfirmacaoDePropriedade(telefone.ID, new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.Telefonema });

            await assertSucesso(solicitacaoEnvio);
        }


        [Fact]
        public async Task SolicitarConfirmacaoDePropriedadeTorpedoVoz_ExceptionNoProducerTorpedoVoz_DeveRetornarFalseComMensagemDeErro()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("configuracao-tokenconfirmacaotelefone-quantidadedigitos", TOKEN_QUANTIDADE_DIGITOS.ToString()),
                new KeyValuePair<string, string>("configuracao-torpedovoz-quantidademaximaenviosdia", "2"),
                new KeyValuePair<string, string>("configuracao-torpedovoz-intervalominimoreenviosegundos", "999999" )
            }, null, null);

            instanciarServico(configuracao);
            await criarTemplateTorpedoVoz();
            var telefone = await criarTelefone();

            var solicitacaoEnvio = await _telefoneClienteConfirmacaoServico.SolicitarConfirmacaoDePropriedade(telefone.ID
                                        , new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.Telefonema });

            Assert.NotNull(solicitacaoEnvio);
            Assert.False(solicitacaoEnvio.SolicitacaoEnviada);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erros => erros.Mensagem.Equals(Mensagens.TelefoneConfirmacao_NaoFoiPossivelEnviarTokenPorTorpedoVoz));
        }

        [Fact]
        public async Task ReenviarSolicitacaoDeConfirmacaoDePropriedadeSMS_SolicitacaoInexistente_DeveRetornarTrueSemMensagemDeErro()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("configuracao-tokenconfirmacaotelefone-quantidadedigitos", TOKEN_QUANTIDADE_DIGITOS.ToString())
            }, null, null);

            _producerSms = new Mock<IProducerSms>().Object;
            instanciarServico(configuracao);
            await criarTemplateSms();
            

            var telefone = await criarTelefone();

            var solicitacaoEnvio = await _telefoneClienteConfirmacaoServico.ReenviarSolicitacaoDeConfirmacaoDePropriedade(telefone.ID, new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.Sms });

            await assertSucesso(solicitacaoEnvio);
        }

        [Fact]
        public async Task ReenviarSolicitacaoDeConfirmacaoDePropriedadeSMS_TelefoneJaConfirmado_DeveRetornarFalseComMensagemDeAlerta()
        {
            instanciarServico();
            await criarTemplateSms();
            
            var telefone = await criarTelefone();
            telefone.AlternarConfirmado(true);
            await _contexto.SaveChangesAsync();

            await criarSolicitacao(telefone.ID, TipoSolicitacaoConfirmacao.Sms);

            var solicitacao = await _telefoneClienteConfirmacaoServico.ReenviarSolicitacaoDeConfirmacaoDePropriedade(telefone.ID, new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.Sms });
            var alertas = _mensagens.BuscarAlertas();
            Assert.False(_mensagens.PossuiErros);
            Assert.NotEmpty(alertas);
            Assert.Contains(alertas, alerta => alerta.Mensagem.Equals(Mensagens.TelefoneConfirmacao_JaConfirmado));
        }

        [Fact]
        public async Task ReenviarSolicitacaoDeConfirmacaoDePropriedadeSMS_QuantidadeReenviosAtingida_DeveRetornarFalseComMensagemDeErro()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("configuracao-sms-quantidademaximaenviosdia", "1"),
                new KeyValuePair<string, string>("configuracao-sms-intervalominimoreenviosegundos", "3600")
            }, null, null);

            instanciarServico(configuracao);
            await criarTemplateSms();
            
            var telefone = await criarTelefone();
            await criarSolicitacao(telefone.ID, TipoSolicitacaoConfirmacao.Sms);

            var solicitacao = await _telefoneClienteConfirmacaoServico.ReenviarSolicitacaoDeConfirmacaoDePropriedade(telefone.ID, new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.Sms });
            Assert.NotNull(solicitacao);

            var solicitacaoGravada = await obterSolicitacao(solicitacao.Id);

            Assert.False(solicitacaoGravada.Enviada);
            Assert.Equal(Mensagens.TelefoneConfirmacao_FoiAtingidoLimiteDiarioReenviosToken, solicitacaoGravada.MensagemErro);
            Assert.False(solicitacao.SolicitacaoEnviada);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.TelefoneConfirmacao_FoiAtingidoLimiteDiarioReenviosToken));
        }

        [Fact]
        public async Task ReenviarSolicitacaoDeConfirmacaoDePropriedadeSMS_NaoPassouTempoMinimoParaReenvio_DeveRetornarFalseComMensagemDeErro()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("configuracao-sms-quantidademaximaenviosdia", "2"),
                new KeyValuePair<string, string>("configuracao-sms-intervalominimoreenviosegundos", "3600")
            }, null, null);

            instanciarServico(configuracao);
            await criarTemplateSms();
            
            var telefone = await criarTelefone();
            await criarSolicitacao(telefone.ID, TipoSolicitacaoConfirmacao.Sms);

            var solicitacao = await _telefoneClienteConfirmacaoServico.ReenviarSolicitacaoDeConfirmacaoDePropriedade(telefone.ID, new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.Sms });

            Assert.NotNull(solicitacao);

            var solicitacaoGravada = await obterSolicitacao(solicitacao.Id);

            Assert.False(solicitacaoGravada.Enviada);
            Assert.StartsWith(Mensagens.TelefoneConfirmacao_AguardeXSegundosParaSolicitarReenvio.Split()[0], solicitacaoGravada.MensagemErro);
            Assert.False(solicitacao.SolicitacaoEnviada);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.StartsWith(Mensagens.TelefoneConfirmacao_AguardeXSegundosParaSolicitarReenvio.Split()[0]));
        }

        [Fact]
        public async Task ReenviarSolicitacaoDeConfirmacaoDePropriedadeSMS_DadosValidos_DeveRetornarTrueSemMensagemDeErro()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("configuracao-sms-quantidademaximaenviosdia", "2"),
                new KeyValuePair<string, string>("configuracao-sms-intervalominimoreenviosegundos", "0")
            }, null, null);

            _producerSms = new Mock<IProducerSms>().Object;
            instanciarServico(configuracao);
            await criarTemplateSms();
            
            var telefone = await criarTelefone();
            await criarSolicitacao(telefone.ID, TipoSolicitacaoConfirmacao.Sms);

            var solicitacao = await _telefoneClienteConfirmacaoServico.ReenviarSolicitacaoDeConfirmacaoDePropriedade(telefone.ID, new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.Sms });

            Assert.NotNull(solicitacao);

            var solicitacaoGravada = await obterSolicitacao(solicitacao.Id);

            Assert.True(solicitacaoGravada.Enviada);
            Assert.Null(solicitacaoGravada.MensagemErro);
            Assert.True(solicitacao.SolicitacaoEnviada);
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task ReenviarSolicitacaoDeConfirmacaoDePropriedadeWhatsapp_DadosValidos_DeveRetornarTrueSemMensagemDeErro()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("configuracao-whatsapp-quantidademaximaenviosdia", "3"),
                new KeyValuePair<string, string>("configuracao-whatsapp-intervalominimoreenviosegundos", "0" )
            }, null, null);

            _producerWhatsapp = new Mock<IProducerWhatsapp>().Object;
            instanciarServico(configuracao);
            await criarTemplateWhatsApp();
            
            var telefone = await criarTelefone();
            await criarSolicitacao(telefone.ID, TipoSolicitacaoConfirmacao.WhatsApp);

            var solicitacao = await _telefoneClienteConfirmacaoServico.ReenviarSolicitacaoDeConfirmacaoDePropriedade(telefone.ID, 
                                            new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.WhatsApp });

            Assert.NotNull(solicitacao);

            var solicitacaoGravada = await obterSolicitacao(solicitacao.Id);

            Assert.True(solicitacaoGravada.Enviada);
            Assert.Null(solicitacaoGravada.MensagemErro);
            Assert.True(solicitacao.SolicitacaoEnviada);
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task ReenviarSolicitacaoDeConfirmacaoDePropriedadeTorpedoVoz_DadosValidos_DeveRetornarTrueSemMensagemDeErro()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("configuracao-torpedovoz-quantidademaximaenviosdia", "2"),
                new KeyValuePair<string, string>("configuracao-torpedovoz-intervalominimoreenviosegundos", "0" )
            }, null, null);

            _producerTorpedoVoz = new Mock<IProducerTorpedoVoz>().Object;
            instanciarServico(configuracao);
            await criarTemplateTorpedoVoz();
            
            var telefone = await criarTelefone();
            await criarSolicitacao(telefone.ID, TipoSolicitacaoConfirmacao.Telefonema);

            var solicitacao = await _telefoneClienteConfirmacaoServico.ReenviarSolicitacaoDeConfirmacaoDePropriedade(telefone.ID, 
                                            new TelefoneClienteSolicitacaoConfirmacaoEnvioModel { TipoSolicitacaoConfirmacao = TipoSolicitacaoConfirmacao.Telefonema });

            Assert.NotNull(solicitacao);

            var solicitacaoGravada = await obterSolicitacao(solicitacao.Id);

            Assert.True(solicitacaoGravada.Enviada);
            Assert.Null(solicitacaoGravada.MensagemErro);
            Assert.True(solicitacao.SolicitacaoEnviada);
            Assert.False(_mensagens.PossuiErros);

        }


        [Fact]
        public async Task ConfirmarPropriedadeTelefone_TelefoneInvalido_DeveRetornarFalseComMensagemDeErro()
        {
            instanciarServico();
            await criarTemplateSms();
            
            var telefoneConfirmado = await _telefoneClienteConfirmacaoServico.ConfirmarPropriedadeTelefone(1, null);

            Assert.False(telefoneConfirmado);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.Telefone_NaoLocalizado));
        }

        [Fact]
        public async Task ConfirmarPropriedadeTelefone_TokenInvalido_DeveRetornarFalseComMensagemDeErro()
        {
            instanciarServico();
            await criarTemplateSms();
            
            var telefone = await criarTelefone();
            await criarSolicitacao(telefone.ID, TipoSolicitacaoConfirmacao.Sms);

            var telefoneConfirmado = await _telefoneClienteConfirmacaoServico.ConfirmarPropriedadeTelefone(telefone.ID, "4321");

            Assert.False(telefoneConfirmado);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.TelefoneConfirmacao_TokenInvalido));
        }

        [Fact]
        public async Task ConfirmarPropriedadeTelefoneSMS_DadosValidos_DeveRetornarTrueSemMensagemDeErro()
        {
            instanciarServico();
            await criarTemplateSms();
            
            var telefone = await criarTelefone();
            await criarSolicitacao(telefone.ID, TipoSolicitacaoConfirmacao.Sms);

            var telefoneConfirmado = await _telefoneClienteConfirmacaoServico.ConfirmarPropriedadeTelefone(telefone.ID, TOKEN_PADRAO);
            var telefoneGravado = await _contexto.TelefonesCliente.AsNoTracking().FirstOrDefaultAsync(t => t.ID.Equals(telefone.ID));

            Assert.NotNull(telefoneGravado);
            Assert.True(telefoneGravado.Confirmado);
            Assert.True(telefoneConfirmado);
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task ConfirmarPropriedadeTelefoneWhatsapp_DadosValidos_DeveRetornarTrueSemMensagemDeErro()
        {
            instanciarServico();
            await criarTemplateWhatsApp();
            
            var telefone = await criarTelefone();
            await criarSolicitacao(telefone.ID, TipoSolicitacaoConfirmacao.WhatsApp);

            var telefoneConfirmado = await _telefoneClienteConfirmacaoServico.ConfirmarPropriedadeTelefone(telefone.ID, TOKEN_PADRAO);
            var telefoneGravado = await _contexto.TelefonesCliente.AsNoTracking().FirstOrDefaultAsync(t => t.ID.Equals(telefone.ID));

            Assert.NotNull(telefoneGravado);
            Assert.True(telefoneGravado.Confirmado);
            Assert.True(telefoneConfirmado);
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task ConfirmarPropriedadeTelefoneTorpedoVoz_DadosValidos_DeveRetornarTrueSemMensagemDeErro()
        {
            instanciarServico();
            await criarTemplateTorpedoVoz();
            
            var telefone = await criarTelefone();
            await criarSolicitacao(telefone.ID, TipoSolicitacaoConfirmacao.Telefonema);

            var telefoneConfirmado = await _telefoneClienteConfirmacaoServico.ConfirmarPropriedadeTelefone(telefone.ID, TOKEN_PADRAO);
            var telefoneGravado = await _contexto.TelefonesCliente.AsNoTracking().FirstOrDefaultAsync(t => t.ID.Equals(telefone.ID));

            Assert.NotNull(telefoneGravado);
            Assert.True(telefoneGravado.Confirmado);
            Assert.True(telefoneConfirmado);
            Assert.False(_mensagens.PossuiErros);
        }

        private async Task assertSucesso(TelefoneClienteSolicitacaoConfirmacaoModel solicitacaoEnvio)
        {
            Assert.NotNull(solicitacaoEnvio);

            var solicitacaoGravada = await obterSolicitacao(solicitacaoEnvio.Id);

            Assert.True(solicitacaoEnvio.SolicitacaoEnviada);
            Assert.NotNull(solicitacaoGravada.Token);
            Assert.Equal(TOKEN_QUANTIDADE_DIGITOS, solicitacaoGravada.Token.Length);
            Assert.Null(solicitacaoGravada.MensagemErro);
            Assert.True(solicitacaoGravada.Enviada);
            Assert.NotNull(solicitacaoGravada.DataEnvioSolicitacao);
            Assert.False(_mensagens.PossuiErros);
        }

        private void instanciarServico(Configuracao configuracao = null){
            
            _servicoSms = new SmsServico( _mensagens
                                        , _usuarioLogin
                                        , _contexto
                                        , _producerSms );
            
            _servicoTorpedoVoz = new TorpedoVozServico( _mensagens
                                        , _usuarioLogin
                                        , _contexto
                                        , _producerTorpedoVoz );
            
            _servicoWhatsapp = new WhatsappServico( _mensagens
                                                  , _usuarioLogin
                                                  , _contexto
                                                  , _producerWhatsapp );
            

            _telefoneClienteConfirmacaoServico = 
                new TelefoneClienteConfirmacaoServico(_mensagens
                                                    , _usuarioLogin
                                                    , _contexto
                                                    , _servicoSms
                                                    , _servicoWhatsapp
                                                    , _servicoTorpedoVoz
                                                    , configuracao
                                                    , _logger);
        }

        private async Task criarTemplateSms(){
            var templateSms = new TemplateSmsDominio( Dominio.Enum.TemplateSms.TemplateSms.TokenConfirmacaoTelefone
                                                    , "PropriedadeTelefone"
                                                    , "Seu token é #TOKEN " );
            await _contexto.AddAsync(templateSms);
            await _contexto.SaveChangesAsync();
        }

        private async Task criarTemplateWhatsApp(){
            var templateWhatsapp = new TemplateWhatsappDominio( Dominio.Enum.TemplateWhatsapp.TemplateWhatsapp.TokenConfirmacaoTelefone
                                                              , "Confirmação Telefone"
                                                              , "11111111-1111-1111-1111-111111111111");
                                                                                                                                                                                                                        
            await _contexto.AddAsync(templateWhatsapp);
            await _contexto.SaveChangesAsync();
        }   

        private async Task criarTemplateTorpedoVoz(){
            var templateTorpedoVoz = new TemplateTorpedoVozDominio( Dominio.Enum.TemplateTorpedoVoz.TemplateTorpedoVoz.TokenConfirmacaoTelefone
                                                                  , "Propriedade Telefone"
                                                                  , "O seu token é #TOKEN#");
            await _contexto.AddAsync(templateTorpedoVoz);
            await _contexto.SaveChangesAsync();

        }         

        private async Task<TelefoneClienteDominio> criarTelefone()
        {
            var usuarioTeste = await CriarUsuarioTesteAsync();
            var telefone = new TelefoneClienteDominio(usuarioTeste.Cliente.ID, new Fone("51", "99999 9999"));

            await _contexto.AddAsync(telefone);
            await _contexto.SaveChangesAsync();

            return telefone;
        }

        private async Task<TelefoneClienteDominio> criarTelefoneConvencional()
        {
            var usuarioTeste = await CriarUsuarioTesteAsync();
            var telefone = new TelefoneClienteDominio(usuarioTeste.Cliente.ID, new Fone("51", "33156327"));

            await _contexto.AddAsync(telefone);
            await _contexto.SaveChangesAsync();

            return telefone;
        }

        private async Task criarSolicitacao(int idTelefoneCliente, TipoSolicitacaoConfirmacao tipoSolicitacaoConfirmacao)
        {
            var solicitacao = new TelefoneClienteSolicitacaoConfirmacaoDominio(tipoSolicitacaoConfirmacao, idTelefoneCliente, TOKEN_PADRAO);
            solicitacao.AtualizarDadosEnvioSolicitacao(true, null);
            await _contexto.AddAsync(solicitacao);
            await _contexto.SaveChangesAsync();
        }

        private async Task<TelefoneClienteSolicitacaoConfirmacaoDominio> obterSolicitacao(int idSolicitacao)
        => await _contexto.TelefoneClienteSolicitacoesConfirmacao.LastOrDefaultAsync(s => s.ID.Equals(idSolicitacao));
    }
}
