using Aplicacao.Model.Anexo;
using Aplicacao.Model.Beneficio;
using Aplicacao.Servico;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Cliente;
using Infraestrutura.Providers.Cliente.Dto.NovaAutorizacao;
using Infraestrutura.Providers.Dto;
using Infraestrutura.Providers.Paperless;
using Infraestrutura.Providers.Paperless.Dto.AssinaturaDocumento;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Comando
{
    public class BeneficioInssValidacaoTokenCommand : ServicoBaseBeneficioInss
    {
        private readonly IProviderPaperless _providerPaperless;
        private readonly IAnexoServico _anexoServico;
        private readonly IProviderCliente _providerCliente;

        public BeneficioInssValidacaoTokenCommand(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto, IProviderPaperless providerPaperless,
            IProviderAutenticacao providerAutenticacao, IAnexoServico anexoServico, IProviderCliente providerCliente, ConfiguracaoProviders configuracaoProviders)
            : base(mensagens, usuarioLogin, contexto, providerAutenticacao, configuracaoProviders)
        {
            _providerPaperless = providerPaperless;
            _anexoServico = anexoServico;
            _providerCliente = providerCliente;
        }

        public async Task<ValidacaoTokenAssinaturaModel> ValidarTokenAssinatura(ValidacaoTokenAssinaturaEnvioModel parametros)
        {
            var autenticacaoProviders = await ObterAutenticacaoParaProviders();

            if (_mensagens.PossuiErros)
                return null;

            var consultaBeneficio = await ObterConsultaBeneficio(parametros.IdConsultaBeneficio);

            if (!verificarSeConsultaBeneficioValida(consultaBeneficio))
                return null;

            var idPaperlessDocumento = Convert.ToInt32(consultaBeneficio.IdPaperlessDocumento);

            var documentoAssinado = await assinarTermoConsultaBeneficioInss(idPaperlessDocumento, parametros.TokenConsulta, autenticacaoProviders.JwtToken);

            if (_mensagens.PossuiErros)
                return null;

            var anexo = await anexarTermoAutorizacaoAssinado(documentoAssinado);

            if (_mensagens.PossuiErros)
                return null;

            await vincularAnexoComConsultaDoBeneficio(consultaBeneficio, anexo.Id);

            var chaveAutorizacao = await obterChaveAutorizacaoParaConsultaBeneficioInss(consultaBeneficio, documentoAssinado.ToString(), autenticacaoProviders.JwtToken);

            if (_mensagens.PossuiErros)
                return null;

            return new ValidacaoTokenAssinaturaModel(true, chaveAutorizacao);
        }

        private async Task<string> obterChaveAutorizacaoParaConsultaBeneficioInss(ConsultaBeneficioInssClienteDominio consultaBeneficio, string termoAutorizacaoAssinado, string jwtToken)
        {
            var parametrosNovaAutorizacao = new NovaAutorizacaoParametrosDto { IdPaperlessDocumento = Convert.ToInt32(consultaBeneficio.IdPaperlessDocumento), TermoAutorizacao = termoAutorizacaoAssinado };
            var chaveAutorizacao = await _providerCliente.ObterNovaAutorizacaoParaConsultaBeneficioInss(parametrosNovaAutorizacao, jwtToken);

            if (_mensagens.PossuiErros)
                return null;

            if (string.IsNullOrWhiteSpace(chaveAutorizacao))
                _mensagens.AdicionarErro(Mensagens.Beneficio_NaoHouveRetornoValidoNovaAutorizacaoConsultaBeneficio, B.Mensagens.EnumMensagemTipo.negocio);

            await vincularChaveAutorizacaoComConsultaDoBeneficio(consultaBeneficio, chaveAutorizacao);

            return chaveAutorizacao;
        }

        private async Task vincularChaveAutorizacaoComConsultaDoBeneficio(ConsultaBeneficioInssClienteDominio consultaBeneficio, string chaveAutorizacao)
        {
            consultaBeneficio.SetChaveAutorizacao(chaveAutorizacao);
            await _contexto.SaveChangesAsync();
        }

        private async Task<byte[]> assinarTermoConsultaBeneficioInss(int idPaperlessDocumento, string tokenConsulta, string jwtToken)
        {
            var parametrosAssinatura = new AssinaturaDocumentoDto { IdPaperlessDocumento = idPaperlessDocumento, Token = tokenConsulta };
            var documentoAssinado = await _providerPaperless.AssinarDocumento(parametrosAssinatura, jwtToken);

            if (_mensagens.PossuiErros)
                return null;

            if (documentoAssinado is null || !documentoAssinado.Any())
            {
                _mensagens.AdicionarErro(Mensagens.Beneficio_NaoFoiPossivelObterTermoAssinado, B.Mensagens.EnumMensagemTipo.negocio);
                return null;
            }

            return documentoAssinado;
        }

        private async Task<AnexoModel> anexarTermoAutorizacaoAssinado(byte[] documentoAssinado)
        {
            var parametrosAnexo = new AnexoCriacaoModel
            {
                AnexoBase64 = Convert.ToBase64String(documentoAssinado),
                Extensao = _configuracaoProviders.Paperless.ExtensaoDocumentoTermo,
                IdTipoDocumento = (int)TipoDocumento.TermoAutorizacaoBeneficiario
            };

            var anexo = await _anexoServico.GravarArquivo(parametrosAnexo);

            if (_mensagens.PossuiErros)
                return null;

            if (anexo is null)
            {
                _mensagens.AdicionarErro(Mensagens.Beneficio_NaoHouveRetornoValidoAoAnexarTermoAssinado, EnumMensagemTipo.negocio);
                return null;
            }

            return anexo;
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

        private async Task vincularAnexoComConsultaDoBeneficio(ConsultaBeneficioInssClienteDominio consultaBeneficio, int idAnexo)
        {
            consultaBeneficio.SetAnexoArquivoTermo(idAnexo);
            await _contexto.SaveChangesAsync();
        }
    }
}
