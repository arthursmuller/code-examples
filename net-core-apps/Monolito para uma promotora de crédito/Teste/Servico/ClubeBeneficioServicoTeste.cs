using Aplicacao.Servico;
using Dominio;
using Dominio.Resource;
using Infraestrutura.Providers.Kaledo;
using Infraestrutura.Providers.Kaledo.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class ClubeBeneficioServicoTeste : ServicoTesteBase
    {
        private readonly UsuarioDominio _usuarioTeste;
        private readonly Mock<IProviderKaledo> _providerKaledo = new Mock<IProviderKaledo>();
        private ClubeBeneficioServico _clubeBeneficioServico;

        private string _urlRetorno = "https://seu-dominio.com.br/login?token=token&activate=true";

        public ClubeBeneficioServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();
            _clubeBeneficioServico = new ClubeBeneficioServico(_contexto
                                                    , _usuarioLogin
                                                    , _mensagens
                                                    , _providerKaledo.Object);

        }

        [Fact]
        public async Task CriarAutenticarUsuario_DadosCorretos_RetornaSucesso()
        {
            DefineRetornoSucessoCriarAutenticarUsuarioKaledo();
            AtualizarCampoOperacaoAtivaCliente(true);

            var retorno = await _clubeBeneficioServico.CriarAutenticarUsuario();

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(_urlRetorno, retorno);
        }

        [Fact]
        public async Task CriarAutenticarUsuario_ClienteOperacaoAtivaFalso_RetornaFalha()
        {
            AtualizarCampoOperacaoAtivaCliente(false);

            var retorno = await _clubeBeneficioServico.CriarAutenticarUsuario();

            Assert.Null(retorno);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.Cliente_ClubeBeneficioOperacaoInativa));
        }

        [Fact]
        public async Task CriarAutenticarUsuario_ErroComunicacao_RetornaFalha()
        {
            DefineRetornoNullCriarAutenticarUsuarioKaledo();
            AtualizarCampoOperacaoAtivaCliente(true);

            var retorno = await _clubeBeneficioServico.CriarAutenticarUsuario();

            Assert.Null(retorno);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.ProviderKaledo_NaoHouveSucessoNoRetornoDoProvedorCriarAuntenticarUsuario));
        }

        [Fact]
        public async Task CriarAutenticarUsuario_RetornoComSuccessFalso_RetornaFalha()
        {
            DefineRetornoSuccessFalsoCriarAutenticarUsuarioKaledo();
            AtualizarCampoOperacaoAtivaCliente(true);

            var retorno = await _clubeBeneficioServico.CriarAutenticarUsuario();

            Assert.Null(retorno);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.ProviderKaledo_NaoHouveSucessoNoRetornoDoProvedorCriarAuntenticarUsuario));
        }

        private void DefineRetornoNullCriarAutenticarUsuarioKaledo() =>
            _providerKaledo
                .Setup(x => x.CriarAutenticarUsuario(It.IsAny<KaledoCriarAutenticarUsuarioDTO>()))
                .ReturnsAsync((KaledoResultadoCriarAuntenticarUsuarioDTO)null);

        private void DefineRetornoSucessoCriarAutenticarUsuarioKaledo() =>
            _providerKaledo
                .Setup(x => x.CriarAutenticarUsuario(It.IsAny<KaledoCriarAutenticarUsuarioDTO>()))
                .ReturnsAsync(new KaledoResultadoCriarAuntenticarUsuarioDTO()
                {
                    Data = _urlRetorno,
                    Success = true,
                    Message = string.Empty
                });

        private void DefineRetornoSuccessFalsoCriarAutenticarUsuarioKaledo() =>
            _providerKaledo
                .Setup(x => x.CriarAutenticarUsuario(It.IsAny<KaledoCriarAutenticarUsuarioDTO>()))
                .ReturnsAsync(new KaledoResultadoCriarAuntenticarUsuarioDTO()
                {
                    Data = "",
                    Success = false,
                    Message = string.Empty
                });

        private void AtualizarCampoOperacaoAtivaCliente(bool operacaoAtiva)
        {
            var cliente = _contexto.Clientes.Where(n => n.ID == _usuarioTeste.Cliente.ID).FirstOrDefault();
            cliente.SetOperacaoAtiva(operacaoAtiva);
            _contexto.SaveChanges();
        }
    }
}
