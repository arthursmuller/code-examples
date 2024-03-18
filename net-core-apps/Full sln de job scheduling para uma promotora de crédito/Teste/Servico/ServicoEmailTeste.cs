using System.Linq;
using Dominio.Entidades;
using Fila.Model.Email;
using Infraestrutura.DTO.Email;
using Infraestrutura.Enum;
using Infraestrutura.Providers;
using Aplicacao.Servico;
using Xunit;
using Moq;

namespace Teste.Servico
{
    public class ServicoEmailTeste: ServicoBase
    {
        private readonly string testeEmail = "clientehml@bempromotora.com.br";
        private readonly string testeEmailNomeExibicao = "Plataforma cliente final - robo | (unit tests)";
        private readonly string testeEmailPassword = "matone@123";
        private readonly string office365SmtpHost = "smtp.office365.com";
        private readonly int office365SmtpPort = 587;

        private readonly Mock<IProviderEmail> _providerEmailMock = new Mock<IProviderEmail>();
        private IProviderEmail _providerEmail;
        private readonly EmailServico _emailServico;

        public ServicoEmailTeste(): base()
        {
            _providerEmail = _providerEmailMock.Object;
            _emailServico = new EmailServico(_mensageria, _contexto, _providerEmail);
        }

        [Fact]
        public async void MandarMensagem_QuandoRequisitado_DeveRetornarStatusEnviado()
        {

            _providerEmailMock
                .Setup(x => x.EnviarEmail(It.IsAny<EmailMensagemDto>()))
                .ReturnsAsync(StatusEnvio.Sucesso);
            _providerEmail = _providerEmailMock.Object;

            EmailMensagemDto mensagem = new EmailMensagemDto()
            {
                Destinatario = testeEmail,
                Copia = testeEmail,

                Assunto = "my test message",
                Prioritario = true,
                Corpo = @"
                    <html>
                        <body>
                            <p>Dear reader,</p>
                            <p>This is a test email</p>
                            <p>Sincerely,<br>-Unit Test</br></p>
                        </body>
                    </html>
                    ",

                Porta = office365SmtpPort,
                Host = office365SmtpHost,
                Ssl = true,
                Senha = testeEmailPassword,
                Usuario = testeEmail,
                NomeExibicao = testeEmailNomeExibicao,
            };

            StatusEnvio result = await _providerEmail.EnviarEmail(mensagem);

            Assert.True(result == StatusEnvio.Sucesso);
        }

        [Fact]
        public async void MandarMensagem_QuandoInvalidoConfiguracao_DeveRetornarStatusErro()
        {
            _providerEmailMock
                .Setup(x => x.EnviarEmail(It.IsAny<EmailMensagemDto>()))
                .ReturnsAsync(StatusEnvio.Erro);
            _providerEmail = _providerEmailMock.Object;

            EmailMensagemDto mensagem = new EmailMensagemDto()
            {
                Destinatario = testeEmail,
                Prioritario = true,
                Senha = testeEmailPassword,
                Usuario = testeEmail,
            };

            StatusEnvio result = await _providerEmail.EnviarEmail(mensagem);

            Assert.True(result == StatusEnvio.Erro);
        }

        [Fact]
        public async void MandarMensagem_QuandoInvalidoRequisicao_DeveRetornarStatusErro()
        {
            _providerEmailMock
                .Setup(x => x.EnviarEmail(It.IsAny<EmailMensagemDto>()))
                .ReturnsAsync(StatusEnvio.Erro);
            _providerEmail = _providerEmailMock.Object;

            EmailMensagemDto mensagem = new EmailMensagemDto()
            {
                Porta = office365SmtpPort,
                Host = office365SmtpHost,
                Ssl = true,
                Senha = testeEmailPassword,
                Usuario = testeEmail,
                NomeExibicao = testeEmailNomeExibicao,
            };

            StatusEnvio result = await _providerEmail.EnviarEmail(mensagem);

            Assert.True(result == StatusEnvio.Erro);
        }

        [Fact]
        public async void ReceberRequisicaoDeMensagem_AposEnviar_DevePersistirRequisicao()
        {
            _providerEmailMock
                .Setup(x => x.EnviarEmail(It.IsAny<EmailMensagemDto>()))
                .ReturnsAsync(StatusEnvio.Sucesso);
            _providerEmail = _providerEmailMock.Object;

            int idEmailFornecedor = configurarFornecedorTest();

            EmailRequisicaoMensagem mensagem = new EmailRequisicaoMensagem()
            {
                CodigoReferenciaMensagem = "email test",
                Destinatarios = new string[] {testeEmail},
                Mensagem = "test",
                Assunto = "Service test",
                Prioritario = false,
                IdEmailFornecedor = idEmailFornecedor,
            };

            await _emailServico.ProcessarRequisicao(mensagem);

            EmailMensagemDominio mensagemLogada = _contexto.EmailMensagens.FirstOrDefault(m => m.IdEmailFornecedor == idEmailFornecedor);

            Assert.NotNull(mensagemLogada.DataEnvio);
            Assert.True(mensagemLogada.DataEnvio != null);
        }

        private int configurarFornecedorTest() 
        {
            EmpresaDominio empresa = new EmpresaDominio("Empresa Unit Tests - ltda");
            _contexto.Empresas.Add(empresa);
            _contexto.SaveChanges();

            EmailFornecedorDominio email = new EmailFornecedorDominio(
                testeEmailNomeExibicao,
                testeEmail,
                testeEmailPassword,
                office365SmtpHost,
                office365SmtpPort,
                true,
                empresa.ID
            );
            _contexto.EmailFornecedores.Add(email);
            _contexto.SaveChanges();

            return email.ID;
        }
    }
}
