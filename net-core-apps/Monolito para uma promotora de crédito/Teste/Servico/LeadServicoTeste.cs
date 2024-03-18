using Aplicacao;
using Aplicacao.Servico;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Geolocalizacao;
using Infraestrutura.RedesSociais;
using Moq;
using SharedKernel.ValueObjects.v2;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class LeadServicoTeste : ServicoTesteBase
    {
        private readonly Mock<IProviderMaxMind> _providerMaxMindMock;
        private readonly LojaServico _lojaServico;
        private readonly LeadServico _leadServico;

        public LeadServicoTeste() : base()
        {
            var redesSociaisConfiguracao = new RedesSociaisConfiguracao()
            {
                WhatsApp = new WhatsAppConfiguracao()
                {
                    LinkGeradorFormatado = "test:{0}{1}",
                }
            };

            var geolocalizacaoConfiguracao = new GeolocalizacaoConfiguracao();
            _providerMaxMindMock = new Mock<IProviderMaxMind>();
            _lojaServico = new LojaServico(_contexto, _mensagens, _usuarioLogin, redesSociaisConfiguracao, geolocalizacaoConfiguracao);
            _leadServico = new LeadServico(_mensagens, _contexto, _providerMaxMindMock.Object, _lojaServico);
        }

        [Fact]
        public async Task BuscarLead_QuandoEnviadoUmIdDeLeadExistente_DeveRetornarLead()
        {
            await criarLeads();

            const int id = 1;
            var lead = await _leadServico.BuscarLead(id);

            Assert.NotNull(lead);
            Assert.Equal(lead.Id, id);
            Assert.Equal(Fone.DesmascararTelefone(lead.Telefone), lead.Telefone);
            Assert.Equal(Fone.DesmascararTelefone(lead.Celular), lead.Celular);
            Assert.False(_mensagens.PossuiErros);
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        [Fact]
        public async Task BuscarLead_QuandoEnviadoUmIdDeLeadNaoExistente_DeveRetornarErro()
        {
            await criarLeads();

            var lead = await _leadServico.BuscarLead(121212);

            Assert.Null(lead);
            Assert.NotEmpty(_mensagens.BuscarErros().Where(mensagem => mensagem.Mensagem.Contains(string.Format(Mensagens.Lead_IdNaoEncontrado, 121212))));
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        [Fact]
        public async Task AtualizarLead_QuandoEnviadaInformacoesNovasDeLeadExistente_DeveAtualizarALead()
        {
            await criarLeads();

            var leadParaAtualizar = new LeadAtualizacaoModel()
            {
                CPF = "45396865008",
                IdConvenio = 1,
                Email = "newmail@hotmail.com",
                Telefone = "4141414141",
                IdProduto = 1,
            };

            var leadAtualizada = await _leadServico.AtualizarLead(leadParaAtualizar, 1);

            Assert.NotNull(leadAtualizada);
            Assert.False(_mensagens.PossuiErros);
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        [Fact]
        public async Task AtualizarLead_QuandoEnviadaInformacoesNovasDeLeadNaoExistente_DeveRetornarErro()
        {
            await criarLeads();

            var leadParaAtualizar = new LeadAtualizacaoModel() { };

            var leadAtualizada = await _leadServico.AtualizarLead(leadParaAtualizar, 121212);

            Assert.Null(leadAtualizada);
            Assert.NotEmpty(_mensagens.BuscarErros().Where(mensagem => mensagem.Mensagem.Contains(string.Format(Mensagens.Lead_IdNaoEncontrado, 121212))));
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        [Fact]
        public async Task GravarLead_QuandoEnviadoLeadComLatitudeLongitude_DeveGravarLeadSemChamarApiGeolocalizacao()
        {
            await criarLeads();

            (double? latitude, double? longitude) retornoMaxMind = (30, 20);

            _providerMaxMindMock
                .Setup(maxMind => maxMind.ObterLatitudeLongitude())
                .Returns(retornoMaxMind);
            var providerMaxMind = _providerMaxMindMock.Object;

            var leadModel = new LeadCriacaoModel()
            {
                CPF = "09810103930",
                Telefone = "51999239813",
                Email = "novoemail@bempromotora.com.br",
                IdConvenio = 1,
                IdProduto = 1,
                Latitude = 10.0,
                Longitude = 10.0,
                OrigemRequisicaoPalavraChave = "palavraChave",
                OrigemRequisicaoMidia = "midia",
                OrigemRequisicaoConteudo = "conteudo",
                OrigemRequisicaoTermo = "termo",
                OrigemRequisicaoCampanha = "campanha"
            };

            var novoLead = await _leadServico.GravarLead(leadModel);

            Assert.NotNull(novoLead);
            Assert.True(novoLead.Id > 0);
            _providerMaxMindMock.Verify(providerMock => providerMock.ObterLatitudeLongitude(), Times.Never());
            Assert.NotEqual(novoLead.Latitude, retornoMaxMind.latitude);
            Assert.NotEqual(novoLead.Longitude, retornoMaxMind.longitude);
            Assert.False(_mensagens.PossuiErros);
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        [Fact]
        public async Task GravarLead_QuandoEnviadoLeadSemLatitudeLongitude_DeveGravarLeadChamandoApiGeolocalizacao()
        {
            await criarLeads();

            (double? latitude, double? longitude) retornoMaxMind = (30, 20);

            _providerMaxMindMock
                .Setup(maxMind => maxMind.ObterLatitudeLongitude())
                .Returns(retornoMaxMind);

            var leadModel = new LeadCriacaoModel()
            {
                CPF = "09810103930",
                Telefone = "51999239813",
                Email = "novoemail@bempromotora.com.br",
                IdConvenio = 1,
                IdProduto = 1,
                OrigemRequisicaoPalavraChave = "palavraChave",
                OrigemRequisicaoMidia = "midia",
                OrigemRequisicaoConteudo = "conteudo",
                OrigemRequisicaoTermo = "termo",
                OrigemRequisicaoCampanha = "campanha"
            };

            var novoLead = await _leadServico.GravarLead(leadModel);

            Assert.NotNull(novoLead);
            Assert.True(novoLead.Id > 0);
            _providerMaxMindMock.Verify(providerMock => providerMock.ObterLatitudeLongitude(), Times.Once());
            Assert.Equal(novoLead.Latitude, retornoMaxMind.latitude);
            Assert.Equal(novoLead.Longitude, retornoMaxMind.longitude);
            Assert.False(_mensagens.PossuiErros);
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        [Fact]
        public async Task GravarLead_QuandoSemLojaDesejaContatoWhatsUpp_DeveGravarLeadAdicionandoLojaComLink()
        {
            await criarLeads();

            (double? latitude, double? longitude) retornoMaxMind = (30, 20);

            _providerMaxMindMock
                .Setup(maxMind => maxMind.ObterLatitudeLongitude())
                .Returns(retornoMaxMind);

            await criarDadosBase(retornoMaxMind.latitude.Value, retornoMaxMind.longitude.Value);

            var leadModel = new LeadCriacaoModel()
            {
                CPF = "09810103930",
                Telefone = "51999239813",
                Email = "novoemail@bempromotora.com.br",
                IdConvenio = 1,
                IdProduto = 1,
                OrigemRequisicaoPalavraChave = "palavraChave",
                OrigemRequisicaoMidia = "midia",
                OrigemRequisicaoConteudo = "conteudo",
                OrigemRequisicaoTermo = "termo",
                OrigemRequisicaoCampanha = "campanha",
                DesejaContatoWhatsApp = true,
            };

            var novoLead = await _leadServico.GravarLead(leadModel);

            Assert.NotNull(novoLead.LinkContatoWhatsAppLoja);
            Assert.False(_mensagens.PossuiErros);
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        [Theory]
        [InlineData("55607251020", null, null)]
        [InlineData("821.673.430-19", null, null)]
        [InlineData(null, "51956452588", null)]
        [InlineData(null, null, "bem@bempromotora.com.br")]
        public async Task GravarLead_HavendoSomenteUmaInformacaoValida_DeveGravarLeadSemErros(string cpf, string telefone, string email)
        {
            (double? latitude, double? longitude) retornoMaxMind = (30, 20);

            _providerMaxMindMock
                .Setup(maxMind => maxMind.ObterLatitudeLongitude())
                .Returns(retornoMaxMind);
            var providerMaxMind = _providerMaxMindMock.Object;

            var leadModel = new LeadCriacaoModel()
            {
                CPF = cpf,
                Telefone = telefone,
                Email = email
            };

            var novoLead = await _leadServico.GravarLead(leadModel);

            Assert.NotNull(novoLead);
            Assert.True(novoLead.Id > 0);
            _providerMaxMindMock.Verify(providerMock => providerMock.ObterLatitudeLongitude(), Times.Once());
            Assert.Equal(novoLead.Latitude, retornoMaxMind.latitude);
            Assert.Equal(novoLead.Longitude, retornoMaxMind.longitude);
            Assert.False(_mensagens.PossuiErros);
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        private async Task criarLeads()
        {
            var leads = new List<LeadDominio>();

            leads.Add(new LeadDominio(
                new CPF("09809809830"),
                "Amilton",
                "(51) 3443-6661",
                "(51) 99697-6661",
                "amilton@bempromotora.com.br",
                Dominio.Enum.Produto.CreditoConsignado,
                Dominio.Enum.Convenio.INSS,
                -90.0,
                -20.0,
                "palavraChave",
                "midia",
                "conteudo",
                "termo",
                "campanha",
                null,
                false,
                ""
            ));

            leads.Add(new LeadDominio(
                new CPF("10110110122"),
                null,
                "(47) 99914-2887",
                "(47) 99992-5377",
                "alaska@bempromotora.com.br",
                Dominio.Enum.Produto.CreditoConsignado,
                Dominio.Enum.Convenio.INSS,
                -12.5,
                1,
                "palavraChave",
                "midia",
                "conteudo",
                "termo",
                "campanha",
                null,
                false,
                ""
            ));

            leads.Add(new LeadDominio(
                new CPF("01901901912"),
                null,
                "(47) 99992-5377",
                "(47) 99992-5377",
                "outroamilton@bempromotora.com.br",
                Dominio.Enum.Produto.CreditoConsignado,
                Dominio.Enum.Convenio.INSS,
                -1,
                0.5,
                "palavraChave",
                "midia",
                "conteudo",
                "termo",
                "campanha",
                null,
                false,
                ""
            ));

            await _contexto.Leads.AddRangeAsync(leads);

            await _contexto.SaveChangesAsync();
        }

        private async Task criarDadosBase(double latitude, double longitude)
        {
            var uf = new UnidadeFederativaDominio("Rio Grande do Sul", "RS");
            await _contexto.AddAsync(uf);
            await _contexto.SaveChangesAsync();

            var municipio = new MunicipioDominio("Porto Alegre", 1);
            var tipoLogradouro = new TipoLogradouroDominio("R", "Rua");

            await _contexto.AddAsync(municipio);
            await _contexto.AddAsync(tipoLogradouro);
            await _contexto.SaveChangesAsync();

            await _contexto.Lojas.AddAsync(new LojaDominio(
                "loja do centro",
                1,
                "Centro Historico",
                1,
                "rua dos andradas",
                null,
                null,
                "90020006",
                "",
                new List<TelefoneLojaDominio>
                {
                    new TelefoneLojaDominio("47999142887", true, "Mensagem whatsapp")
                },
                new NetTopologySuite.Geometries.Point(latitude, longitude) { SRID = 4326 }
            ));
            await _contexto.SaveChangesAsync();
        }
    }
}
