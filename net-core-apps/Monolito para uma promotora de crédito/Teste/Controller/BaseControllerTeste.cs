using Api;
using B.Testes.API;
using Dominio;
using Infraestrutura;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.ValueObjects.v2;

namespace Teste.Controller
{
    public class BaseControllerTeste : BaseTesteAPIController<Startup>
    {
        public const string NOME_USUARIO = "Teste";
        public const string EMAIL_USUARIO = "teste@teste.com";
        public const string CPF_USUARIO = "00011122233";
        public const string SENHA_ENCRIPTADA = "DQQldMckbltVJKYqtDP1ig==";
        public const string SENHA_DECRIPTADA = "bem@123";

        protected ServiceProvider _serviceProvider;

        public UsuarioDominio CriarUsuarioTestePadrao(bool concederPermissaodministrador = false)
            => CriarUsuarioTeste(NOME_USUARIO, EMAIL_USUARIO, new CPF(CPF_USUARIO), SENHA_ENCRIPTADA, concederPermissaodministrador);

        public UsuarioDominio CriarUsuarioTeste(string nome, string email, CPF cpf, string senhaEncriptada, bool concederPermissaodministrador = false)
        {
            var usuario = new UsuarioDominio(nome, email, true, cpf, senhaEncriptada, new ClienteDominio(nome));
            usuario.SetPermissaoAdministrador(concederPermissaodministrador);

            using (var scope = _serviceProvider.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetService<PlataformaClienteContexto>();
                ctx.Usuarios.Add(usuario);
                ctx.SaveChanges();
            }

            return usuario;
        }
    }
}
