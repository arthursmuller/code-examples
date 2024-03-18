using Aplicacao.Model.Beneficio;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using Dominio;
using Infraestrutura;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Cliente;
using Infraestrutura.Providers.Dto;
using SharedKernel.ValueObjects.v2;
using System.Threading.Tasks;

namespace Aplicacao.Consulta
{
    public class BeneficioInssAutorizacaoVigenteQuery : ServicoBaseSuporteAutenticacao
    {
        private readonly IProviderCliente _providerCliente;

        public BeneficioInssAutorizacaoVigenteQuery(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto,
            IProviderCliente providerCliente, IProviderAutenticacao providerAutenticacao, ConfiguracaoProviders configuracaoProviders)
            : base(mensagens, usuarioLogin, contexto, providerAutenticacao, configuracaoProviders)
            => _providerCliente = providerCliente;

        public async Task<ObtencaoAutorizacaoConsultaBeneficioModel> ObterAutorizacaoVigente()
        {
            var dadosUsuarioAutenticado = await ObterDadosUsuarioAutenticado();

            if (_mensagens.PossuiErros)
                return null;

            var autenticacaoProviders = await ObterAutenticacaoParaProviders();

            if (_mensagens.PossuiErros)
                return null;

            var chaveAutorizacao = await _providerCliente.ObterAutorizacaoConsultaBeneficioExistente(new CPF(dadosUsuarioAutenticado.CPF), autenticacaoProviders.JwtToken);

            if (_mensagens.PossuiErros)
                return null;

            return new ObtencaoAutorizacaoConsultaBeneficioModel(chaveAutorizacao);
        }
    }
}
