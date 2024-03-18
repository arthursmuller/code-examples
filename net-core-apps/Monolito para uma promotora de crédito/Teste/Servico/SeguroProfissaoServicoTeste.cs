using Aplicacao.Model.SeguroProfissao;
using Aplicacao.Servico;
using Dominio;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class SeguroProfissaoServicoTeste : ServicoTesteBase
    {
        private readonly SeguroProfissaoServico _servico;
        private UsuarioDominio _usuarioTeste;

        public SeguroProfissaoServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();
            
            _servico = new SeguroProfissaoServico(_mensagens, _usuarioLogin, _contexto);
        }

        [Fact]
        public async Task Listar_Profissao_Deve_Retornar_Profissao()
        {
            await criarEntidades();

            var resultado = await _servico.Listar();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado.Count() > 0);
        }

        [Fact]
        public async Task Listar_ProfissaoIcatu_Deve_Retornar_ProfissaoIcatu()
        {
            await criarEntidades();

            var resultado = await _servico.ListarIcatu();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado.Count() > 0);
        }

        [Fact]
        public async Task Criar_Profissao_Deve_Retornar_Profissao()
        {
            var resultado = await _servico.Adicionar(new [] { 
                new CriarSeguroProfissaoModel { CodigoProfissao = 1, NomeProfissao = "teste"}
            });

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado.Count() > 0);
        }

        [Fact]
        public async Task Criar_ProfissaoIcatu_Deve_Retornar_ProfissaoIcatu()
        {
            await AddRangeAndSaveAsync(new[] {
                new SeguroProfissaoDominio(1, "teste")
            });

            var resultado = await _servico.Adicionar(new[] {
                new CriarSeguroProfissaoIcatuModel { IdSeguroProfissao = 1, Codigo = 1, Descricao = "Teste", }
            });

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
        }

        private async Task criarEntidades()
        {
            await AddRangeAndSaveAsync(new[] {
                new SeguroProfissaoDominio(1, "teste"),
                new SeguroProfissaoDominio(2, "teste2")
            });

            await AddRangeAndSaveAsync(new[] {
                new SeguroProfissaoIcatuDominio(1, "teste", 1),
                new SeguroProfissaoIcatuDominio(2, "teste2", 2)
            });
        }
    }
}
