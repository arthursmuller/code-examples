using Aplicacao;
using Aplicacao.Servico;
using B.Configuracao;
using Dominio;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class LeadCorrespondenteServicoTeste : ServicoTesteBase
    {
        private readonly LeadCorrespondenteServico _leadCorrespondenteServico;

        public LeadCorrespondenteServicoTeste()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("email-suporte-comercial", "teste@teste.com"),
            }, null, null);

            _leadCorrespondenteServico = new LeadCorrespondenteServico(_contexto, new Mock<IEmailServico>().Object, configuracao);
        }

        [Fact]
        public async Task GravarLead_QuandoDadosValidos_DeveRetornarId()
        {
            await criarDadosBase();

            var leadCorrespondente = new LeadCorrespondenteCriacaoModel
            {
                Atividades = "Vendas",
                CNPJ = "07022524000122",
                Email = "email@email.com",
                IdMunicipio = 1,
                Nome = "Empresa Teste",
                Telefone = "515645745"
            };

            var idLead = await _leadCorrespondenteServico.GravarLead(leadCorrespondente);

            Assert.NotNull(idLead);
            Assert.True(idLead > 0);
            Assert.False(_mensagens.PossuiErros);
        }

        private async Task criarDadosBase()
        {
            var uf = new UnidadeFederativaDominio("Rio Grande do Sul", "RS");
            await _contexto.AddAsync(uf);
            await _contexto.SaveChangesAsync();

            var municipio = new MunicipioDominio("Porto Alegre", 1);
            var tipoLogradouro = new TipoLogradouroDominio("R", "Rua");

            await _contexto.AddAsync(municipio);
            await _contexto.AddAsync(tipoLogradouro);
            await _contexto.SaveChangesAsync();
        }
    }
}
