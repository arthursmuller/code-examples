using Aplicacao.Servico;
using Dominio;
using Dominio.Enum.TemplateEmail;
using Infraestrutura.Fila.Email;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class EmailServicoTeste : ServicoTesteBase
    {
        private readonly EmailServico _emailServico;
        private Mock<IProducerEmail> _producerEmailMock;
        private Mock<ITemplateEmailServico> _templateEmailServicoMock;

        public EmailServicoTeste() : base()
        {
            _producerEmailMock = new Mock<IProducerEmail>();
            _templateEmailServicoMock = new Mock<ITemplateEmailServico>();
            _emailServico = new EmailServico(_mensagens, _usuarioLogin, _contexto, _producerEmailMock.Object, _templateEmailServicoMock.Object);
        }

        [Fact]
        public async Task RegistrarEmail_QuandoTemplateNaoEncontrado_NaoDeveEnviarNemRegistrar()
        {
            _templateEmailServicoMock.Setup(t => t.GerarTemplate(It.IsAny<TemplateEmailFinalidade>(), It.IsAny<Dictionary<string, object>>()))
                .ReturnsAsync("");

            var retorno = await _emailServico.RegistrarEmail(TemplateEmailFinalidade.RecuperacaoSenha, "", new string[] { }, new Dictionary<string, object>());

            _producerEmailMock.Verify(p => p.Publicar(It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.False(retorno);
            Assert.Empty(await consultarRegistrosEmailBanco());
        }

        [Fact]
        public async Task RegistrarEmail_QuandoTemplateEncontrado_DeveEnviarERegistrarEnvio()
        {
            _templateEmailServicoMock.Setup(t => t.GerarTemplate(It.IsAny<TemplateEmailFinalidade>(), It.IsAny<Dictionary<string, object>>()))
                .ReturnsAsync("<div>template</div>");

            var retorno = await _emailServico.RegistrarEmail(TemplateEmailFinalidade.RecuperacaoSenha, "", new string[] { }, new Dictionary<string, object>());

            _producerEmailMock.Verify(p => p.Publicar(It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.True(retorno);
            Assert.Single(await consultarRegistrosEmailBanco());
        }

        [Fact]
        public async Task RegistrarEmail_QuandoTemplateEncontrado_UnicoDestinatario_DeveEnviarERegistrarEnvio()
        {
            _templateEmailServicoMock.Setup(t => t.GerarTemplate(It.IsAny<TemplateEmailFinalidade>(), It.IsAny<Dictionary<string, object>>()))
                .ReturnsAsync("<div>template</div>");

            var retorno = await _emailServico.RegistrarEmail(TemplateEmailFinalidade.RecuperacaoSenha, "", new string[] { }, new Dictionary<string, object>());

            _producerEmailMock.Verify(p => p.Publicar(It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            
            Assert.True(retorno);
            var email = consultarRegistrosEmailBanco().Result.FirstOrDefault();
            Assert.NotNull(email);
        }

        private async Task<List<RegistroEmailDominio>> consultarRegistrosEmailBanco() 
                        => await _contexto.RegistrosEmail.ToListAsync();
    }
}
