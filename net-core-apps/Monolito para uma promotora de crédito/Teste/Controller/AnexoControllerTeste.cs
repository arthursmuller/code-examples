using Aplicacao;
using Aplicacao.Model.Autenticacao;
using Aplicacao.Servico;
using B.Models;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Controller
{
    public class AnexoControllerTeste : BaseControllerTeste
    {
        private Mock<IAnexoServico> _anexoServicoMock = new Mock<IAnexoServico>();

        public AnexoControllerTeste()
        {
            _anexoServicoMock
                .Setup(s => s.BuscarAnexosPorCpfUsuario(CPF_USUARIO))
                .ReturnsAsync(It.IsAny<IEnumerable<AnexoModel>>());
        }

        [Fact]
        public async Task ObterPorCpf_EntradaComUsuarioNaoAdministrador_RetornaNaoAutorizado()
        {
            CriarUsuarioTestePadrao();

            var autenticacao = await obterAutenticacao("login");

            ClienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", autenticacao.Retorno?.Token);
            var anexosResponse = await ClienteHttp.GetAsync($"/usuarios/{CPF_USUARIO}/anexos");

            Assert.NotNull(autenticacao.Retorno?.Token);
            Assert.Equal(HttpStatusCode.Forbidden, anexosResponse.StatusCode);
        }

        [Fact]
        public async Task ObterPorCpf_EntradaComUsuarioAdministrador_RetornaHttpStatusOk()
        {
            var usuario = "Tenant";
            var email = "admin@bempromotora.com.br";

            CriarUsuarioTeste(usuario, email, null, SENHA_ENCRIPTADA, concederPermissaodministrador: true);

            var autenticacao = await obterAutenticacao("login/administrador", email, usuario);

            ClienteHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", autenticacao.Retorno?.Token);
            var anexosResponse = await ClienteHttp.GetAsync($"/usuarios/{CPF_USUARIO}/anexos");

            Assert.NotNull(autenticacao.Retorno?.Token);
            Assert.Equal(HttpStatusCode.OK, anexosResponse.StatusCode);
        }

        private async Task<RetornoApi<AutenticacaoModel>> obterAutenticacao(string rota, string email = EMAIL_USUARIO, string usuarioTenant = null)
        {
            var parametroAutenticacao = new LoginModel { Email = email, Senha = SENHA_DECRIPTADA, UsuarioTenant = usuarioTenant };
            var responseAutenticacao = await ClienteHttp.PostAsJsonAsync(rota, parametroAutenticacao);
            var autenticacao = JsonConvert.DeserializeObject<RetornoApi<AutenticacaoModel>>(await responseAutenticacao.Content.ReadAsStringAsync());
            return autenticacao;
        }

        public override IServiceCollection CriarServicosTeste()
        {
            var service = new ServiceCollection();
            service.AddDbContext<PlataformaClienteContexto>(options => options.UseInMemoryDatabase("Teste"));
            service.AddScoped(s => _anexoServicoMock);

            _serviceProvider = service.BuildServiceProvider();

            return service;
        }
    }
}
