using Aplicacao.Model.FeatureFlags;
using Aplicacao.Servico;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class FeatureFlagServicoTeste : ServicoTesteBase
    {
        private readonly FeatureFlagServico _featureFlagServico;

        public FeatureFlagServicoTeste()
            => _featureFlagServico = new FeatureFlagServico(_mensagens, _usuarioLogin, _contexto);

        [Fact]
        public async Task ConsultarChaves_QuandoPossuiDados_DeveRetornarDadosSemErros()
        {
            var flagsIniciais = await adicionarChavesTeste();

            var flags = await _featureFlagServico.ConsultarChaves();

            Assert.NotNull(flags);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(flagsIniciais.Count(), flags.Count());
        }

        [Fact]
        public async Task PersistirChave_QuandoNaoExiste_DeveAdicionar()
        {
            var flagsIniciais = await adicionarChavesTeste();

            var flag = await _featureFlagServico.AdicionarOuAtualizar(new FeatureFlagModel("FlagNova", true));

            var flagsBanco = await _contexto.FeatureFlags.ToListAsync();

            Assert.NotNull(flag);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(flagsIniciais.Count() + 1, flagsBanco.Count());
        }

        [Fact]
        public async Task PersistirChave_QuandoExiste_DeveAtualizar()
        {
            var flagsIniciais = await adicionarChavesTeste();

            var primeiraFlag = flagsIniciais.First();
            var valorInicial = primeiraFlag.Habilitado;

            var flag = await _featureFlagServico.AdicionarOuAtualizar(new FeatureFlagModel(primeiraFlag.Chave.ToLower(), !valorInicial));

            var flagsBanco = await _contexto.FeatureFlags.ToListAsync();

            Assert.NotNull(flag);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(flagsIniciais.Count(), flagsBanco.Count());
            Assert.Equal(flag.Habilitado, !valorInicial);
        }

        private async Task<IEnumerable<FeatureFlagDominio>> adicionarChavesTeste()
        {
            var featuresFlag = new List<FeatureFlagDominio> {
                new FeatureFlagDominio("FLAG1", true),
                new FeatureFlagDominio("FLAG2", true)
            };

            await _contexto.FeatureFlags.AddRangeAsync(featuresFlag);
            await _contexto.SaveChangesAsync();

            return featuresFlag;
        }
    }
}
