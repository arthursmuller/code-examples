using System;
using System.Linq;
using System.Text;
using Dominio.Entidades;
using Fila.Model.Sms;
using Infraestrutura.DTO.Zenvia;
using Infraestrutura.Enum;
using Infraestrutura.Providers;
using Aplicacao.Servico;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace Teste.Servico
{
    public class ServicoSmsTeste: ServicoBase
    {
        private readonly string DDD = "51";
        private readonly string testeTelefone = "997264683";
        private readonly string testeEmailNomeExibicao = "Plataforma cliente final - robo | (unit tests)";
        private readonly string testeZenviaUsuario = "bem.sms.cliente";
        private readonly string testeZenviaSenha = "xxxxxx";

        private Mock<IProviderZenvia> _providerZenviaMock = new Mock<IProviderZenvia>();
        private IProviderZenvia _providerZenvia;
        private readonly SmsServico _smsServico;

        public ServicoSmsTeste(): base()
        {
            _providerZenvia = _providerZenviaMock.Object;
            _smsServico = new SmsServico(_mensageria, _contexto, _providerZenvia);
        }

        [Fact]
        public async void MandarMensagem_QuandoRequisitado_DeveRetornarStatusEnviado()
        {

            (StatusEnvio i1, ZenviaStatus? i2, ZenviaStatusDetalhes? i3) retEnviarMensagem 
                    = (StatusEnvio.Sucesso, ZenviaStatus.Ok, null);

            ZenviaSmsMensagemDto mensagem = new ZenviaSmsMensagemDto()
            {
                To = testeTelefone,
                Msg = "Hello, I'm a unit test!",
                From = testeEmailNomeExibicao,
                AggregateId = 9,
                Id = gerarIdRdeferencia(),
            };
            string credenciais = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{testeZenviaUsuario}:{testeZenviaSenha}"));

            _providerZenviaMock
                .Setup(zenvia => zenvia.EnviarMensagem(mensagem, credenciais))
                .ReturnsAsync(retEnviarMensagem);
            _providerZenvia = _providerZenviaMock.Object;


            (StatusEnvio, ZenviaStatus?, ZenviaStatusDetalhes?) result = await _providerZenvia.EnviarMensagem(mensagem, credenciais);

            Assert.True(result.Item1 == StatusEnvio.Sucesso);
            Assert.True(result.Item2 == ZenviaStatus.Ok);
        }

        [Fact]
        public async void MandarMensagem_QuandoInvalidoRequisicao_DeveRetornarStatusErro()
        {
            (StatusEnvio i1, ZenviaStatus? i2, ZenviaStatusDetalhes? i3) retEnviarMensagem 
                    = (StatusEnvio.Sucesso, ZenviaStatus.Error, null);


            ZenviaSmsMensagemDto mensagem = new ZenviaSmsMensagemDto()
            {
                To = "5551000001122",
                Msg = "Hello, I'm a unit test!",
                From = testeEmailNomeExibicao,
                AggregateId = 9,
                Id = gerarIdRdeferencia(),
            };

            string credenciais = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"{testeZenviaUsuario}:{testeZenviaSenha}"));
            _providerZenviaMock
                .Setup(zenvia => zenvia.EnviarMensagem(mensagem, credenciais))
                .ReturnsAsync(retEnviarMensagem);
            _providerZenvia = _providerZenviaMock.Object;


            (StatusEnvio, ZenviaStatus?, ZenviaStatusDetalhes?) result = await _providerZenvia.EnviarMensagem(mensagem, credenciais);

            Assert.True(result.Item1 == StatusEnvio.Sucesso);
            Assert.True(result.Item2 == ZenviaStatus.Error);
        }

        [Fact]
        public async void ReceberRequisicaoDeMensagem_AposEnviar_DevePersistirRequisicao()
        {
            int idSmsFornecedor = configurarFornecedorTest();
            
            (StatusEnvio i1, ZenviaStatus? i2, ZenviaStatusDetalhes? i3) retEnviarMensagem 
                    = (StatusEnvio.Sucesso, ZenviaStatus.Ok, ZenviaStatusDetalhes.MessageSent);

            SmsRequisicaoMensagem mensagem = new SmsRequisicaoMensagem()
            {
                DDD = "051",
                Telefone = "997264683",
                Mensagem = "Hello, I'm a unit test!",
                IdSmsFornecedor = idSmsFornecedor,
                CodigoReferenciaMensagem = gerarIdRdeferencia(),
            };

            _providerZenviaMock
                .Setup(zenvia => zenvia.EnviarMensagem(It.IsAny<ZenviaSmsMensagemDto>(), It.IsAny<string>()))
                .ReturnsAsync(retEnviarMensagem);
            _providerZenvia = _providerZenviaMock.Object;

            await _smsServico.ProcessarRequisicao(mensagem);

            SmsMensagemDominio mensagemLogada = _contexto.SmsMensagens.FirstOrDefault(m => m.IdSmsFornecedor == idSmsFornecedor);

            Assert.NotNull(mensagemLogada.DataEnvio);
            Assert.True((ZenviaStatus)mensagemLogada.IdSituacaoEnvio == ZenviaStatus.Ok);
            Assert.True(mensagemLogada.DataEnvio != null);
        }

        private int configurarFornecedorTest() 
        {
            EmpresaDominio empresa = new EmpresaDominio("Empresa Unit Tests - ltda");
            _contexto.Empresas.Add(empresa);
            _contexto.SaveChanges();

            SmsFornecedorDominio smsFornecedor = new SmsFornecedorDominio(
                testeEmailNomeExibicao,
                testeZenviaUsuario,
                testeZenviaSenha,
                20,
                empresa.ID
            );
            _contexto.SmsFornecedores.Add(smsFornecedor);
            _contexto.SaveChanges();

            return smsFornecedor.ID;
        }

        private string gerarIdRdeferencia()
        {
            Random random = new Random();
            return random.Next(9000000).ToString();
        }
    }
}
