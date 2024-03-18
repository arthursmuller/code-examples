using Aplicacao.Model.Banco;
using Aplicacao.Model.TelefoneCliente;
using Aplicacao.Servico;
using Dominio;
using Dominio.Resource;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernel.ValueObjects.v2;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class ClienteServicoTeste : ServicoTesteBase
    {
        private readonly ClienteServico _clienteServico;
        private UsuarioDominio _usuarioTeste;

        public ClienteServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();

            _clienteServico = new ClienteServico(_mensagens, _usuarioLogin, _contexto, It.IsAny<ILocalizacaoServico>());
        }

        [Fact]
        public async Task ConsultarClientePorCPF_QuandoValido_DeveRetornar()
        {
            var (cliente, usuario) = await criarDados();

            var resultado = await _clienteServico.ObterClientePorCpf(usuario.CPF);

            Assert.NotNull(resultado);
            Assert.Equal(cliente.Nome, resultado.Nome);
        }

        [Fact]
        public async Task ConsultarClientePorCPF_QuandoNaoExiste_DeveRetornar()
        {
            var (cliente, usuario) = await criarDados();

            var resultado = await _clienteServico.ObterClientePorCpf("000.000.000-00");

            Assert.Null(resultado);
            Assert.Contains(_mensagens.BuscarErros(), e => e.Mensagem.Equals(Mensagens.Cliente_NaoEncontrado));
        }

        [Fact]
        public async Task Listar_Generos_Deve_Listar_Generos()
        {
            await _contexto.AddRangeAsync(new[] { new GeneroDominio("Marculino", "M") });
            await SaveChangesAsync();
            var resultado = await _clienteServico.ListarGeneros();

            Assert.True(resultado.Count() >= 1);
        }

        [Fact]
        public async Task Adicionar_Conta_Bancaria_Deve_Adicionar_Conta()
        {
            await _contexto.AddRangeAsync(new[] { new GeneroDominio("Marculino", "M") });
            await SaveChangesAsync();
            var resultado = await _clienteServico.AdicionarContaBancaria(new ContaBancariaModel()
            {
                Agencia = "12321",
                NumeroConta = "232112",
                DigitoVerificadorAgencia = 1,
                IdBanco = 1
            });

            Assert.True(resultado);
        }

        [Fact]
        public async Task Atualizar_Conta_Bancaria_Deve_Adicionar_Conta()
        {
            await _contexto.AddRangeAsync(new[] { new GeneroDominio("Marculino", "M") });
            await SaveChangesAsync();
            await _clienteServico.AdicionarContaBancaria(new ContaBancariaModel()
            {
                Agencia = "12321",
                NumeroConta = "232112",
                DigitoVerificadorAgencia = 1,
                IdBanco = 1
            });

            var newModel = new ContaBancariaModel()
            {
                Agencia = "9999",
                NumeroConta = "99999",
                DigitoVerificadorAgencia = 2,
                IdBanco = 2
            };

            var resultado = await _clienteServico.AtualizarContaBancaria(newModel);

            var conta = await _contexto.ContasBancarias.FirstOrDefaultAsync(e => e.NumeroConta.Equals(newModel.NumeroConta));

            Assert.True(resultado);
            Assert.Equal(conta.NumeroConta, conta.NumeroConta);
            Assert.Equal(conta.Agencia, conta.Agencia);
            Assert.Equal(conta.DigitoVerificadorAgencia, conta.DigitoVerificadorAgencia);
            Assert.Equal(conta.IdBanco, conta.IdBanco);
        }

        private async Task<List<TelefoneClienteDominio>> consultarTelefones() => await _contexto.TelefonesCliente.Where(t => t.IdCliente == _usuarioTeste.Cliente.ID).ToListAsync();

        private async Task<(ClienteDominio, UsuarioDominio)> criarDados()
        {
            var cliente = new ClienteDominio("test");
            var usuario = new UsuarioDominio("test", "test@test.com", true, new CPF("123.123.123-12"), "1234abcd", cliente);
            var banco = new BancoDominio("2", "12321313213", "satander", true);
            await _contexto.AddRangeAsync(usuario);
            await _contexto.AddRangeAsync(banco);
            await _contexto.SaveChangesAsync();

            return (cliente, usuario);
        }
    }
}
