using Aplicacao.Interfaces;
using Aplicacao.Model.Proposta;
using AutoMapper;
using B.Mensagens.Interfaces;
using Dominio;
using Infraestrutura;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Auth.Dto;
using Infraestrutura.Providers.BemApi;
using Infraestrutura.Providers.Dto;
using SharedKernel.ValueObjects.v2;
using System;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class PropostaServico : ServicoBase, IPropostaServico
    {
        private readonly IProviderBemApi _providerBemApi;
        private readonly IProviderAutenticacao _providerAutenticacao;
        private readonly ConfiguracaoProviders _configuracaoProviders;

        public PropostaServico(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto,
            IProviderBemApi providerBemApi, IProviderAutenticacao providerAutenticacao, ConfiguracaoProviders configuracaoProviders)
            : base(mensagens, usuarioLogin, contexto)
        {
            _providerBemApi = providerBemApi;
            _providerAutenticacao = providerAutenticacao;
            _configuracaoProviders = configuracaoProviders;
        }

        public async Task<SituacaoPropostaModel> ObterSituacaoProposta(string cpf, string token, DateTime dataNascimento)
        {
            if (!CPF.IsValid(cpf, _mensagens))
                return null;

            var autenticacao = await obterAutenticacao();

            if (_mensagens.PossuiErros)
                return null;

            var situacaoProposta = await _providerBemApi.ObterSituacaoProposta(new CPF(cpf), token, dataNascimento, autenticacao.JwtToken);

            return Mapper.Map<SituacaoPropostaModel>(situacaoProposta);
        }

        private async Task<RetornoAtenticacaoDto> obterAutenticacao()
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
