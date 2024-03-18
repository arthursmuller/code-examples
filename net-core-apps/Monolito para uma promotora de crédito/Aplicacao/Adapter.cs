using Aplicacao.Model.AcessoDadosPessoais;
using Aplicacao.Model.Autenticacao;
using Aplicacao.Model.Banco;
using Aplicacao.Model.Consignado;
using Aplicacao.Model.IntencaoOperacao;
using Aplicacao.Model.Proposta;
using Aplicacao.Model.Usuario;
using AutoMapper;
using Dominio;
using Infraestrutura.Providers.BemApi.Dto;
using Infraestrutura.Providers.Consignado.Dto;
using Infraestrutura.Providers.Consignado.Dto.SimulacaoPortabilidade;

namespace Aplicacao
{
    public static class Adapter
    {
        public static void Mapear()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<SimulacaoPortabilidadeEnvioModel, ParametrosSimulacaoPortabilidadeDto>(MemberList.None)
                    .ForMember(destino => destino.Conveniada, option => option.Ignore())
                    .ForMember(destino => destino.SimulacaoEspecial, option => option.Ignore())
                    .ReverseMap();

                config.CreateMap<ViabilidadePortabilidadeDto, ViabilidadePortabilidadeModel>(MemberList.None)
                    .ReverseMap();

                config.CreateMap<RetornoSimulacaoDto, SimulacaoRefinanciamentoModel>(MemberList.None)
                    .ReverseMap();

                config.CreateMap<RetornoSimulacaoPortabilidadeDto, SimulacaoPortabilidadeModel>(MemberList.None)
                    .ForMember(destino => destino.SimulacoesIntencaoRefinanciamento, option => option.MapFrom(origem => origem.Simulacao))
                    .ReverseMap();

                config.CreateMap<IntencaoOperacaoPortabilidadeModel, IntencaoOperacaoPortabilidadeDominio>(MemberList.None)
                    .ReverseMap();

                config.CreateMap<IntencaoOperacaoPortabilidadeDominio, IntencaoOperacaoPortabilidadeVisualizacaoModel>(MemberList.None)
                    .ReverseMap();

                config.CreateMap<BancoDominio, BancoModel>(MemberList.None)
                    .ReverseMap();

                config.CreateMap<ObtencaoSituacaoPropostaDto, SituacaoPropostaModel>(MemberList.None)
                    .ReverseMap();

                config.CreateMap<AutenticacaoModel, AutenticacaoLoginSocialModel>(MemberList.None)
                    .ForMember(destino => destino.UsuarioCadastrado, option => option.Ignore())
                    .ReverseMap();

                config.CreateMap<UsuarioLoginSocialCriacaoModel, LoginSocialModel>(MemberList.None)
                    .ReverseMap();

                config.CreateMap<SolicitacaoAcessoDadosPessoaisClienteDominio, SolicitacaoAcessoDadosPessoaisModel>(MemberList.None)
                    .ForMember(destino => destino.Id, option => option.MapFrom(origem => origem.ID))
                    .ReverseMap();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
