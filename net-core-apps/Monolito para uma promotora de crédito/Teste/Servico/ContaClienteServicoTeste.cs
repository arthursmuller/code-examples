using Aplicacao.Servico;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class ContaClienteServicoTeste : ServicoTesteBase
    {
        private ContaClienteServico _contaClienteServico;
        private readonly UsuarioDominio _usuarioTeste;

        public ContaClienteServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();

            _contaClienteServico = new ContaClienteServico(_mensagens, _usuarioLogin, _contexto);
        }

        [Fact]
        public async Task ConsultarContas_QuandoExistem_DeveRetornarContasDoCliente()
        {
            await criarDados();

            var retorno = await _contaClienteServico.ListarContasAutenticado();

            var banco = await consultarRegistrosBanco();

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(retorno);
            Assert.Equal(retorno.Count(), banco.Where(c => c.IdCliente == _usuarioTeste.Cliente.ID).Count());
        }

        [Fact]
        public async Task ExcluirConta_QuandoEstaEmUso_DeveRetornarErro()
        {
            var (__, _, contaEmUso) = await criarDados();

            var retorno = await _contaClienteServico.ExcluirContaAutenticado(contaEmUso.ID);

            Assert.False(retorno);
            Assert.Contains(_mensagens.BuscarErros(), e => e.Mensagem.Equals(Mensagens.Conta_EmUso));
        }

        [Fact]
        public async Task ExcluirConta_DeOutroUsuario_DeveRetornarErro()
        {
            var (__, contaOutroUsuario, _) = await criarDados();

            var retorno = await _contaClienteServico.ExcluirContaAutenticado(contaOutroUsuario.ID);

            Assert.False(retorno);
            Assert.Contains(_mensagens.BuscarErros(), e => e.Mensagem.Equals(Mensagens.Conta_NaoLocalizada));
        }

        [Fact]
        public async Task ExcluirConta_QuandoNaoEstaEmUso_DeveRemover()
        {
            var (conta, _, __) = await criarDados();

            var retorno = await _contaClienteServico.ExcluirContaAutenticado(conta.ID);

            var banco = await consultarRegistrosBanco();

            Assert.True(retorno);
            Assert.True(banco.Any(c => c.ID == conta.ID && c.Deletado == true));
        }

        [Fact]
        public async Task VerificarContaCliente_QuandoPertence_DeveRetornar()
        {
            var (conta, _, __) = await criarDados();

            var retorno = await _contaClienteServico.VerificarContaCliente(_usuarioTeste.ID, conta.ID);

            Assert.True(retorno);
        }

        [Fact]
        public async Task VerificarContaCliente_QuandoNaopPertence_DeveRetornarErro()
        {
            var (_, contaOutroUsuario, __) = await criarDados();

            var retorno = await _contaClienteServico.VerificarContaCliente(_usuarioTeste.ID, contaOutroUsuario.ID);

            Assert.False(retorno);
            Assert.Contains(_mensagens.BuscarErros(), e => e.Mensagem.Equals(Mensagens.Conta_NaoLocalizada));
        }

        private async Task<List<ContaClienteDominio>> consultarRegistrosBanco()
            => await _contexto.ContasCliente.ToListAsync();

        private async Task<(ContaClienteDominio, ContaClienteDominio, ContaClienteDominio)> criarDados()
        {
            var outroUsuario = CriarUsuarioTeste();

            var banco = new BancoDominio("123", "1234567890", "teste", false);
            await _contexto.AddAsync(banco);
            await _contexto.AddAsync(new FormaRecebimentoDominio(FormaRecebimento.TED, "TED"));
            await _contexto.AddAsync(new TipoContaDominio(TipoConta.Normal, "Normal", "N"));
            await _contexto.AddAsync(new ConvenioDominio(Convenio.INSS, "INSS", "23124", "3424"));
            await _contexto.AddAsync(new ConvenioOrgaoDominio("123", "INSS", "23124", Convenio.INSS));
            await _contexto.AddAsync(new UnidadeFederativaDominio("Test", "TT"));
            await _contexto.AddAsync(new InssEspecieBeneficioDominio("Test", "TT"));

            await _contexto.SaveChangesAsync();

            var contaOutroUsuario = new ContaClienteDominio(outroUsuario.Cliente.ID, banco.ID, TipoConta.Normal, "1234", "1234", FormaRecebimento.TED);
            await _contexto.AddAsync(contaOutroUsuario);
            var contaUsuario = new ContaClienteDominio(_usuarioTeste.Cliente.ID, banco.ID, TipoConta.Normal, "1234", "1234", FormaRecebimento.TED);
            await _contexto.AddAsync(contaUsuario);

            var contaEmUso = new ContaClienteDominio(_usuarioTeste.Cliente.ID, banco.ID, TipoConta.Normal, "1234", "1234", FormaRecebimento.TED);
            await _contexto.AddAsync(contaEmUso);

            await _contexto.SaveChangesAsync();

            await _contexto.AddAsync(new RendimentoClienteInssDominio(contaEmUso.ID, contaEmUso.ID, _usuarioTeste.Cliente.ID, Convenio.INSS, 1, 1, 12, "12312312", 1, DateTime.Now));

            await _contexto.SaveChangesAsync();

            return (contaUsuario, contaOutroUsuario, contaEmUso);
        }
    }
}
