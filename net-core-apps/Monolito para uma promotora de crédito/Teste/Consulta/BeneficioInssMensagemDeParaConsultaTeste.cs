using B.Comunicacao.Interfaces;
using B.Comunicacao.Models;
using Dominio;
using Infraestrutura.Consulta;
using Moq;
using System.Threading.Tasks;
using Teste.Servico;
using Xunit;

namespace Teste.Consulta
{
    public class BeneficioInssMensagemDeParaConsultaTeste : ServicoTesteBase
    {
        private readonly BeneficioInssMensagemDeParaQuery _mensagemBeneficioConsulta;

        public BeneficioInssMensagemDeParaConsultaTeste()
            => _mensagemBeneficioConsulta = new BeneficioInssMensagemDeParaQuery(_contexto, _mensagens);

        [Fact]
        public async Task ObterMensagemTratada_ResponseSemErros_SemMensagemDeErro()
        {
            await _mensagemBeneficioConsulta.ObterMensagemTratada(It.IsAny<IConectaResponse>());

            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task ObterMensagemTratada_ResponseDiferenteDeRetornoApi_SemMensagemDeErro()
        {
            var response = new ConectaResponse();
            response.Content = "{\"teste\":\"teste\"}";

            await _mensagemBeneficioConsulta.ObterMensagemTratada(response);

            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task ObterMensagemTratada_PossuiUmaMensagemTratada_AdicionaUmaMensagemDeErro()
        {
            await criarMensagensBeneficioDePara();

            var response = new ConectaResponse();
            response.Content = "{\"retorno\":null,\"alertas\":[],\"erros\":[{\"codigo\":0,\"mensagem\":\"CD - Deu ruim.\",\"tipo\":5}, {\"codigo\":0,\"mensagem\":\"Então...\",\"tipo\":5}]}";

            await _mensagemBeneficioConsulta.ObterMensagemTratada(response);

            var erros = _mensagens.BuscarErros();

            Assert.True(_mensagens.PossuiErros);
            Assert.Equal(1, erros.Count);
            Assert.Contains(erros, erro => erro.Mensagem.Equals("Tente novamente."));
        }

        [Fact]
        public async Task ObterMensagemTratada_PossuiDuasMensagemTratadas_AdicionaDuasMensagemDeErro()
        {
            await criarMensagensBeneficioDePara();

            var response = new ConectaResponse();
            response.Content = "{\"retorno\":null,\"alertas\":[],\"erros\":[{\"codigo\":0,\"mensagem\":\"CD - Deu ruim.\",\"tipo\":5}, {\"codigo\":0,\"mensagem\":\"BGH01234 - Deu ruim também.\",\"tipo\":5}]}";

            await _mensagemBeneficioConsulta.ObterMensagemTratada(response);

            var erros = _mensagens.BuscarErros();

            Assert.True(_mensagens.PossuiErros);
            Assert.Equal(2, erros.Count);
            Assert.Contains(erros, erro => erro.Mensagem.Equals("Tente novamente."));
            Assert.Contains(erros, erro => erro.Mensagem.Equals("Não desista!"));
        }

        private async Task criarMensagensBeneficioDePara()
        {
            var mensagensBeneficios = new BeneficioInssMensagemDeParaDominio[]
            {
                new BeneficioInssMensagemDeParaDominio("CD", "CD - Deu ruim.", "Tente novamente."),
                new BeneficioInssMensagemDeParaDominio("BGH01234", "BGH01234 - Deu ruim também.", "Não desista!")
            };

            await _contexto.AddRangeAsync(mensagensBeneficios);
            await _contexto.SaveChangesAsync();
        }
    }
}
