using Aplicacao.Model.Beneficio;
using Aplicacao.Servico;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Dto;
using Infraestrutura.Providers.Paperless;
using Infraestrutura.Providers.Paperless.Dto.ReenvioToken;
using System.Threading.Tasks;

namespace Aplicacao.Comando
{
    public class BeneficioInssReenvioTokenCommand : ServicoBaseBeneficioInss
    {
        private readonly IProviderPaperless _providerPaperless;

        public BeneficioInssReenvioTokenCommand(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto,
            IProviderPaperless providerPaperless, IProviderAutenticacao providerAutenticacao, ConfiguracaoProviders configuracaoProviders)
            : base(mensagens, usuarioLogin, contexto, providerAutenticacao, configuracaoProviders)
            => _providerPaperless = providerPaperless;

        public async Task<bool> ReenviarTokenParaAssinatura(SolicitacaoReenvioTokenAssinaturaModel parametrosSolicitacao)
        {
            var consultaBeneficio = await ObterConsultaBeneficio(parametrosSolicitacao.IdConsultaBeneficio);

            if (!verificarSeConsultaBeneficioValida(consultaBeneficio))
                return false;

            var telefoneCelular = await ObterTelefoneCelular(parametrosSolicitacao.IdTelefoneEnvioSolicitacao, consultaBeneficio.IdCliente);

            if (_mensagens.PossuiErros)
                return false;

            var autenticacaoProviders = await ObterAutenticacaoParaProviders();

            var parametrosReenvio = new ReenvioTokenParametrosDto { IdPaperlessDocumento = consultaBeneficio.IdPaperlessDocumento.Value, Celular = telefoneCelular.Fone };
            await _providerPaperless.ReenviarToken(parametrosReenvio, autenticacaoProviders.JwtToken);

            if (_mensagens.PossuiErros)
                return false;

            return true;
        }

        private bool verificarSeConsultaBeneficioValida(ConsultaBeneficioInssClienteDominio consultaBeneficio)
        {
            if (consultaBeneficio == null || !consultaBeneficio.IdPaperlessDocumento.HasValue)
            {
                _mensagens.AdicionarErro(Mensagens.Beneficio_RegistroDeConsultaDeBeneficioNaoLocalizada, EnumMensagemTipo.banco);
                return false;
            }

            return true;
        }
    }
}
