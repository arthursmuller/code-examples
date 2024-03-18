using Aplicacao.Servico;
using Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class TipoRegimeCasamentoServicoTeste : ServicoTesteBase
    {
        private readonly TipoRegimeCasamentoServico _servico;
        private UsuarioDominio _usuarioTeste;

        public TipoRegimeCasamentoServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();

            _servico = new TipoRegimeCasamentoServico(_mensagens, _usuarioLogin, _contexto);
        }

        [Fact]
        public async Task Listar_Regimes_Deve_Retornar_Regimes()
        {
            criarEntidades();

            var resultado = await _servico.Listar();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado.Count() > 0);
        }

        private List<TipoRegimeCasamentoDominio> criarEntidades()
        {
            var regimes = new List<TipoRegimeCasamentoDominio>()
            {
                new TipoRegimeCasamentoDominio() { Descricao = "Teste1" },
                new TipoRegimeCasamentoDominio() { Descricao = "Teste2" }
            };

            _contexto.TipoRegimeCasamento.AddRange(regimes);
            SaveChanges();

            return regimes;
        }
    }
}
