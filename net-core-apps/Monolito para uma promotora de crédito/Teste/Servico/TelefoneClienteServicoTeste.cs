using Aplicacao.Model.TelefoneCliente;
using Aplicacao.Servico;
using Dominio;
using Dominio.Resource;
using Infraestrutura.Providers.Cliente.Dto;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects.v2;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class TelefoneClienteServicoTeste : ServicoTesteBase
    {
        private readonly TelefoneClienteServico _telefoneClienteServico;
        private UsuarioDominio _usuarioTeste;

        public TelefoneClienteServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();

            _telefoneClienteServico = new TelefoneClienteServico(_mensagens, _usuarioLogin, _contexto);
        }

        [Fact]
        public async Task BuscarTelefonesPorCliente_QuandoAchado_Retornar()
        {
            var telefones = await criarTelefonesExistentes();

            _usuarioTeste.Cliente.SetTelefonePrincipal(telefones.First().ID);
            _usuarioTeste.Cliente.SetTelefoneSecundario(telefones.ElementAt(1).ID);
            await _contexto.SaveChangesAsync();

            var resultado = await _telefoneClienteServico.BuscarTelefonesPorCliente();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.Equal(resultado.First().Fone, telefones.First().Fone);
            Assert.Equal(resultado.First().DDD, telefones.First().DDD);
        }

        [Fact]
        public async Task GravarTelefones_QuandoNenhumForPrincipal_DeveCadastrarMarcandoUmComoPrincipal()
        {
            var requisicao = gerarRequisicaoSimples();
            requisicao.Principal = false;

            var resultado = await _telefoneClienteServico.GravarTelefone(requisicao);

            var resultadoBanco = await consultarBanco();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
            Assert.NotNull(resultadoBanco.IdTelefonePrincipal);
            Assert.Null(resultadoBanco.IdTelefoneSecundario);
            Assert.True(resultadoBanco.Telefones.Count() == 1);
        }


        [Fact]
        public async Task GravarTelefones_QuandoForPrincipal_DeveCadastrar()
        {
            var requisicao = gerarRequisicaoSimples();

            var resultado = await _telefoneClienteServico.GravarTelefone(requisicao);

            var resultadoBanco = await consultarBanco();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
            Assert.NotNull(resultadoBanco.IdTelefonePrincipal);
            Assert.Null(resultadoBanco.IdTelefoneSecundario);
            Assert.True(resultadoBanco.Telefones.Count() == 1);
        }

        [Fact]
        public async Task GravarTelefones_QuandoForSecundario_DeveCadastrar()
        {
            var requisicao = gerarRequisicaoSimples();

            await _telefoneClienteServico.GravarTelefone(requisicao);

            requisicao.Principal = false;
            var resultado = await _telefoneClienteServico.GravarTelefone(requisicao);

            var resultadoBanco = await consultarBanco();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
            Assert.NotNull(resultadoBanco.IdTelefonePrincipal);
            Assert.NotNull(resultadoBanco.IdTelefoneSecundario);
            Assert.True(resultadoBanco.Telefones.Count() == 2);
        }

        [Fact]
        public async Task GravarTelefones_QuantoAtualizar_DevePersistir()
        {
            var existente = await criarTelefonesExistentes();
            var requisicao = new TelefoneClienteModel()
            {
                Id = existente.First().ID,
                DDD = "66",
                Fone = existente.First().Fone,
                Principal = true
            };

            var resultado = await _telefoneClienteServico.GravarTelefone(requisicao);

            var resultadoBanco = await consultarBanco();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
            Assert.NotNull(resultadoBanco.IdTelefonePrincipal);
            Assert.Null(resultadoBanco.IdTelefoneSecundario);
            Assert.True(resultadoBanco.Telefones.Count() == existente.Count());
            Assert.True(resultadoBanco.TelefonePrincipal.DDD == "66");
        }

        [Fact]
        public async Task GravarTelefones_QuantoExisteNaoEncontrado_DeveRetornarErro()
        {
            var requisicao = gerarRequisicaoSimples();
            requisicao.Id = 2;

            var resultado = await _telefoneClienteServico.GravarTelefone(requisicao);

            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), e => e.Mensagem.Equals(Mensagens.Telefone_NaoLocalizado));
            Assert.False(resultado);
        }

        [Fact]
        public async Task ImportarTelefones_QuantoExiste_DevePersistir()
        {
            var requisicao = new ClienteContatosDto()
            {
                Telefone1 = new TelefoneDto()
                {
                    DDD = "11",
                    Telefone = "991272698",
                    Ramal = "55",
                    CodTipoFone = "55",
                },
                Telefone2 = new TelefoneDto()
                {
                    DDD = "11",
                    Telefone = "991272698",
                    Ramal = "55",
                    CodTipoFone = "55",
                },
            };

            await _telefoneClienteServico.ImportarTelefones(requisicao);

            var resultado = await consultarBanco();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado.IdTelefonePrincipal);
            Assert.NotNull(resultado.IdTelefoneSecundario);
        }

        [Fact]
        public async Task DeletarTelefone_QuandoExiste_DeveDeletar()
        {
            var telefones = await criarTelefonesExistentes();
            await vincularTelefonesPrincipalESecundario(telefones);

            var resultado = await _telefoneClienteServico.DeletarTelefone(telefones.Last().ID);

            var resultadoBanco = await consultarBanco();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
            Assert.Equal(telefones.First().ID, resultadoBanco.IdTelefonePrincipal);
            Assert.Null(resultadoBanco.IdTelefoneSecundario);
            Assert.Single(resultadoBanco.Telefones.Where(t => !t.Deletado));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public async Task DeletarTelefone_QuandoInvalido_DeveRetornarFalseComMensagemDeErro(int idTelefone)
        {
            var telefones = await criarTelefonesExistentes();
            await vincularTelefonesPrincipalESecundario(telefones);

            var resultado = await _telefoneClienteServico.DeletarTelefone(idTelefone);

            var resultadoBanco = await consultarBanco();

            Assert.False(resultado);
            Assert.True(_mensagens.PossuiErros);
            Assert.Equal(telefones.First().ID, resultadoBanco.IdTelefonePrincipal);
            Assert.Equal(telefones.Last().ID, resultadoBanco.IdTelefoneSecundario);
            Assert.Equal(telefones.Count(), resultadoBanco.Telefones.Where(t => !t.Deletado).Count());
        }

        private async Task<ClienteDominio> consultarBanco() => await _contexto.Clientes
            .Include(c => c.TelefonePrincipal)
            .Include(c => c.TelefoneSecundario)
            .Include(c => c.Telefones)
            .FirstOrDefaultAsync(c => c.IdUsuario == _usuarioTeste.ID);

        private TelefoneClienteModel gerarRequisicaoSimples() =>
            new TelefoneClienteModel
            {
                DDD = "51",
                Fone = "11111 1111",
                Principal = true,
            };

        private async Task<IEnumerable<TelefoneClienteDominio>> criarTelefonesExistentes()
        {
            IEnumerable<TelefoneClienteDominio> telefonesExistentes = new List<TelefoneClienteDominio>()
            {
                new TelefoneClienteDominio(_usuarioTeste.Cliente.ID, new Fone("51", "99999 9999")),
                new TelefoneClienteDominio(_usuarioTeste.Cliente.ID, new Fone("51", "88888 8888")),
            };

            await _contexto.AddRangeAsync(telefonesExistentes);
            await _contexto.SaveChangesAsync();

            return telefonesExistentes;
        }

        private async Task vincularTelefonesPrincipalESecundario(IEnumerable<TelefoneClienteDominio> telefones)
        {
            _usuarioTeste.Cliente.SetTelefonePrincipal(telefones.First().ID);
            _usuarioTeste.Cliente.SetTelefoneSecundario(telefones.Last().ID);
            await _contexto.SaveChangesAsync();
        }
    }
}
