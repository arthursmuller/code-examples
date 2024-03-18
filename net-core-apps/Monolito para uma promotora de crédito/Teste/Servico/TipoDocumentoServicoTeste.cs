using Aplicacao.Servico;
using Dominio;
using Dominio.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class TipoDocumentoServicoTeste : ServicoTesteBase
    {
        private readonly TipoDocumentoServico _servico;
        private UsuarioDominio _usuarioTeste;

        public TipoDocumentoServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();

            _servico = new TipoDocumentoServico(_mensagens, _usuarioLogin, _contexto);
        }

        [Fact]
        public async Task Listar_Regimes_Deve_Retornar_Regimes()
        {
            criarEntidades();

            var resultado = await _servico.ListarIdentificaoPessoal();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado.Count() > 0);
        }

        private async Task<List<TipoDocumentoDominio>> criarEntidades()
        {
            var regimes = new List<TipoDocumentoDominio>()
            {
                new TipoDocumentoDominio(TipoDocumento.CarteiraIdentidade, "Carteira", "1", true),
                new TipoDocumentoDominio(TipoDocumento.CarteiraDeTrabalho, "Carteira2", "2", true)
            };

            await _contexto.AddRangeAsync(regimes);
            await SaveChangesAsync();

            return regimes;
        }
    }
}
