using B.Mensagens.Interfaces;
using Dominio;
using Infraestrutura;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Auth.Dto;
using Infraestrutura.Providers.Dto;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class ServicoBaseSuporteAutenticacao : ServicoBase
    {
        protected readonly IProviderAutenticacao _providerAutenticacao;
        protected readonly ConfiguracaoProviders _configuracaoProviders;

        public ServicoBaseSuporteAutenticacao(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto,
            IProviderAutenticacao providerAutenticacao, ConfiguracaoProviders configuracaoProviders) : base(mensagens, usuarioLogin, contexto)
        {
            _providerAutenticacao = providerAutenticacao;
            _configuracaoProviders = configuracaoProviders;
        }

        protected async Task<RetornoAtenticacaoDto> ObterAutenticacaoParaProviders()
        {
            return await _providerAutenticacao.Autenticar(
                new ParametroAutenticacaoDto
                {
                    Usuario = _configuracaoProviders?.ConsignadoUsuario ?? "",
                    Senha = _configuracaoProviders?.ConsignadoSenha ?? ""
                }
            );
        }
    }
}
