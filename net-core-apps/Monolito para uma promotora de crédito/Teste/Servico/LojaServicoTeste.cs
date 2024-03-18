using Aplicacao;
using Aplicacao.Model.Loja;
using Aplicacao.Servico;
using Dominio;
using Dominio.Resource;
using Infraestrutura.Geolocalizacao;
using Infraestrutura.RedesSociais;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class LojaServicoTeste : ServicoTesteBase
    {
        private readonly LojaServico _lojaServico;
        private readonly string _linkWhatsApp = "test:{0}{1}";

        public LojaServicoTeste() : base()
        {
            var redesSociaisConfiguracao = new RedesSociaisConfiguracao()
            {
                WhatsApp = new WhatsAppConfiguracao()
                {
                    LinkGeradorFormatado = _linkWhatsApp,
                }
            };
            var geolocalizacaoConfiguracao = new GeolocalizacaoConfiguracao();
            _lojaServico = new LojaServico(_contexto, _mensagens, _usuarioLogin, redesSociaisConfiguracao, geolocalizacaoConfiguracao);
        }

        [Fact]
        public async Task BuscarLoja_QuandoEnviadoUmIdDeLojaExistente_DeveRetornarLoja()
        {
            await criarLojas();

            const int id = 1;
            var loja = await _lojaServico.BuscarLoja(id);

            Assert.NotNull(loja);
            Assert.Equal(loja.Id, id);
            Assert.False(_mensagens.PossuiErros);
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        [Fact]
        public async Task BuscarLoja_QuandoEnviadoUmIdDeLojaNaoExistente_DeveRetornarErro()
        {
            await criarLojas();

            var loja = await _lojaServico.BuscarLoja(121212);

            Assert.Null(loja);
            Assert.NotEmpty(_mensagens.BuscarErros().Where(mensagem => mensagem.Mensagem.Contains(string.Format(Mensagens.Loja_IdNaoEncontrada, 121212))));
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        [Fact]
        public async Task BuscarLojas_QuandoExistiremLojasCadastradas_DeveRetornarAListaTotalDeLojas()
        {
            await criarLojas();

            var lojas = await _lojaServico.BuscarLojas();

            Assert.NotNull(lojas);
            Assert.Equal(lojas.Count(), _contexto.Lojas.Count());
            Assert.False(_mensagens.PossuiErros);
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        [Fact]
        public async Task BuscarLojas_QuandoNaoExistiremLojasCadastradas_DeveRetornarAlerta()
        {
            var lojas = await _lojaServico.BuscarLojas();

            Assert.Empty(lojas);
            Assert.NotEmpty(_mensagens.BuscarAlertas().Where(mensagem => mensagem.Mensagem.Contains(Mensagens.Loja_NenhumaEncontrada)));
            Assert.Empty(_mensagens.BuscarErros());
        }

        [Fact]
        public async Task CriarLoja_QuandoEnviadoTodosParametrosNecessarios_DeveAdicionarALoja()
        {
            await criarLojas();

            var lojaParaAdicionar = new LojaCriacaoModel()
            {
                Nome = "loja nova",
                Latitude = 10.00,
                Longitude = 15.00,
                Logradouro = "Rua nova",
                IdMunicipio = 1,
                IdTipoLogradouro = 1,
                Bairro = "Centro",
                Cep = "99888333",
                Telefones = new List<TelefoneLojaCriacaoModel>
                {
                    new TelefoneLojaCriacaoModel { Telefone = "01010101" },
                    new TelefoneLojaCriacaoModel { Telefone = "02020202" }
                }
            };

            var lojaAdicionada = await _lojaServico.CriarLoja(lojaParaAdicionar);

            Assert.NotNull(lojaAdicionada);
            Assert.True(lojaAdicionada.Id > 0);
            Assert.NotNull(_contexto.Lojas.Find(lojaAdicionada.Id));
            Assert.False(_mensagens.PossuiErros);
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        [Fact]
        public async Task DeletarLoja_QuandoEnviadoUmIdDeLojaExistente_DeveDeletarALoja()
        {
            await criarLojas();
            var resultado = await _lojaServico.DeletarLoja(1);

            Assert.True(resultado);
            Assert.False(_mensagens.PossuiErros);
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        [Fact]
        public async Task DeletarLoja_QuandoEnviadoUmIdDeLojaNaoExistente_DeveRetornarErro()
        {
            await criarLojas();
            var resultado = await _lojaServico.DeletarLoja(121212);

            Assert.False(resultado);
            Assert.NotEmpty(_mensagens.BuscarErros().Where(mensagem => mensagem.Mensagem.Contains(string.Format(Mensagens.Loja_IdNaoEncontrada, 121212))));
            Assert.Empty(_mensagens.BuscarAlertas());
        }

        [Fact]
        public async Task ObterLinkContatoWhatsApp_PassandoIdLojaExistente_DeveRetornarLinkLoja()
        {
            var lojas = await criarLojas();
            var lojaTeste = lojas.First();
            var (id, link) = await _lojaServico.ObterLinkContatoWhatsApp(lojaTeste.ID, 0, 0);

            var telefoneBancoResultante = lojaTeste.Telefones.First();

            Assert.NotNull(link);
            Assert.NotNull(id);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(string.Format(_linkWhatsApp, telefoneBancoResultante.Telefone, telefoneBancoResultante.MensagemApresentacao), link);
        }

        [Fact]
        public async Task ObterLinkContatoWhatsApp_QuandoPossuiMaisDeUmNumero_DeveRetornarLinkLojaComNumeroWhatsapp()
        {
            var lojas = await criarLojas();
            var lojaTesteCom2Numeros = lojas.FirstOrDefault(l => l.Telefones.Count() > 1 && l.Telefones.Count(t => t.PossuiContaWhatsApp) > 0);

            var (id, link) = await _lojaServico.ObterLinkContatoWhatsApp(lojaTesteCom2Numeros.ID, 0, 0);

            var telefoneBancoResultante = lojaTesteCom2Numeros.Telefones.FirstOrDefault(t => t.PossuiContaWhatsApp);

            Assert.NotNull(link);
            Assert.NotNull(id);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(string.Format(_linkWhatsApp, telefoneBancoResultante.Telefone, telefoneBancoResultante.MensagemApresentacao), link);
        }

        [Fact]
        public async Task ObterLinkContatoWhatsApp_PassandoIdLojaInexistente_DeveRetornarVazio()
        {
            var (id, link) = await _lojaServico.ObterLinkContatoWhatsApp(123456, 0, 0);

            Assert.Null(link);
            Assert.Null(id);
            Assert.True(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task ObterLinkContatoWhatsApp_SemIdLojaSemGeolocalizacao_DeveRetornarLinkLojaAleatoria()
        {
            var lojas = await criarLojas();

            var requisicoesAleatorias = Enumerable.Range(1, 10).Select(_ =>
            {
                return _lojaServico.ObterLinkContatoWhatsApp(null, 0, 0);
            });

            var resultadosUnicos = (await Task.WhenAll(requisicoesAleatorias))
                .Select(r => r.Item1).Distinct().Count();

            Assert.True(resultadosUnicos > 1);
        }

        [Fact]
        public async Task ObterLinkContatoWhatsApp_SemIdLojaPassandoGeoLocalizacao_DeveRetornarLojaProxima()
        {
            var lojas = await criarLojas();
            var geolocalizacoes = TestHelper.ObterGeolocalizacoes();

            var (id, link) = await _lojaServico.ObterLinkContatoWhatsApp(null, geolocalizacoes["centro"][0], geolocalizacoes["centro"][1]);

            Assert.NotNull(id);
            Assert.False(_mensagens.PossuiErros);
        }

        private async Task<IEnumerable<LojaDominio>> criarLojas()
        {
            var geolocalizacoes = TestHelper.ObterGeolocalizacoes();
            var lojas = new List<LojaDominio>();

            await criarDadosBase();

            lojas.Add(new LojaDominio(
                "loja do centro",
                1,
                "Centro Historico",
                1,
                "dos andradas",
                null,
                null,
                "90020006",
                "",
                new List<TelefoneLojaDominio>
                {
                    new TelefoneLojaDominio("47999142887", true, "")
                },
                new Point(geolocalizacoes["sapucaia"][0], geolocalizacoes["sapucaia"][1]) { SRID = 4326 }
            ));

            lojas.Add(new LojaDominio(
                "loja do guaíba",
                1,
                "Centro Historico",
                1,
                "siqueira campos",
                null,
                null,
                "90020001",
                "",
                new List<TelefoneLojaDominio>
                {
                    new TelefoneLojaDominio("51996976661", false, ""),
                    new TelefoneLojaDominio("5199925377", true, "")
                },
                new Point(geolocalizacoes["criciuma"][0], geolocalizacoes["criciuma"][1]) { SRID = 4326 }
            ));

            lojas.Add(new LojaDominio(
                "loja do litoral sc",
                1,
                "Centro",
                1,
                "avenida brasil",
                null,
                null,
                "88330112",
                "",
                new List<TelefoneLojaDominio>(),
                new Point(geolocalizacoes["moinhos"][0], geolocalizacoes["moinhos"][1]) { SRID = 4326 }
            ));

            lojas.Add(new LojaDominio(
                "loja do interior de sc",
                1,
                "São Francisco de Assis",
                1,
                "rua santa terezinha",
                null,
                null,
                "88340724",
                "",
                new List<TelefoneLojaDominio>
                {
                    new TelefoneLojaDominio("47996976661", true, "")
                },
                new Point(geolocalizacoes["cidreira"][0], geolocalizacoes["cidreira"][1]) { SRID = 4326 }
            ));

            await _contexto.Lojas.AddRangeAsync(lojas);

            await _contexto.SaveChangesAsync();

            return lojas;
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
