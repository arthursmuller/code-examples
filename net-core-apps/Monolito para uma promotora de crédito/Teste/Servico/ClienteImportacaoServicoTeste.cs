using Aplicacao.Servico;
using Dominio.Resource;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Auth.Dto;
using Infraestrutura.Providers.Cliente;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class ClienteImportacaoServicoTeste : ServicoTesteBase
    {
        private ClienteImportacaoServico _clienteImportacaoServico;
        private IClienteServico _clienteServico;
        private IProviderCliente _providerCliente;
        private IProviderAutenticacao _providerAutenticacao;


        [Fact]
        public async Task ImportarDadosCliente_QuandoRecusado_DeveRetornar()
        {       
            await CriarUsuarioTesteAsync();

            _clienteServico = new ClienteServico(_mensagens, _usuarioLogin, _contexto, null);
            instanciarClienteImportacaoServico();
 
            var resultado = await _clienteImportacaoServico.AutorizarImportacaoDadosCliente(CPF_USUARIO_TESTE, false);

            Assert.True(resultado);
            Assert.False(_mensagens.PossuiErros);
            Assert.False(_contexto.Clientes.FirstOrDefault(c => c.IdUsuario == _usuarioLogin.IdUsuario).ImportacaoDadosAutorizada);
        }

        [Fact]
        public async Task ImportarDadosCliente_QuandoClienteInexistente_DeveRetornarNullComErro()
        {
            _clienteServico = new ClienteServico(_mensagens, _usuarioLogin, _contexto, null);
            instanciarClienteImportacaoServico();

            var resultado = await _clienteImportacaoServico.AutorizarImportacaoDadosCliente(CPF_USUARIO_TESTE, true);

            Assert.False(resultado);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.Usuario_NaoEncontrado));
        }

        [Fact]
        public async Task ImportarDadosCliente_QuandoDadosValidos_DeveRegistrarAutorizacaoImportacao()
        {
            await CriarUsuarioTesteAsync();

            _clienteServico = new ClienteServico(_mensagens, _usuarioLogin, _contexto, null);
            _providerCliente = new Mock<IProviderCliente>().Object;

            var providerAutenticacaoMock = new Mock<IProviderAutenticacao>();
            providerAutenticacaoMock
                .Setup(s => s.Autenticar(It.IsAny<ParametroAutenticacaoDto>()))
                .ReturnsAsync(new RetornoAtenticacaoDto());

            _providerAutenticacao = providerAutenticacaoMock.Object;
            instanciarClienteImportacaoServico();

            await _clienteImportacaoServico.AutorizarImportacaoDadosCliente(CPF_USUARIO_TESTE, true);

            var dadosImportados = await _clienteServico.ObterClienteAutenticado();

            Assert.NotNull(dadosImportados);
            Assert.NotNull(dadosImportados);
            Assert.NotNull(dadosImportados.DataAutorizacaoImportacaoDados);
            Assert.True(dadosImportados.ImportacaoDadosAutorizada);
            Assert.False(_mensagens.PossuiErros);
        }

        private void instanciarClienteImportacaoServico()
        {
            _clienteImportacaoServico = new ClienteImportacaoServico(_mensagens, _usuarioLogin, _contexto, _providerAutenticacao, _providerCliente, _clienteServico, null, null, null, null, null, null, new Mock<INotificacaoServico>().Object);
        }
    }
}
