using Aplicacao.Model.Beneficio;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Consignado;
using Infraestrutura.Providers.Consignado.Dto;
using Infraestrutura.Providers.Dto;
using Infraestrutura.Providers.Paperless;
using Infraestrutura.Providers.Paperless.Dto;
using Infraestrutura.Providers.Paperless.Enum;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Comando
{
    public class BeneficioInssSolicitacaoAutorizacaoCommand : ServicoBaseBeneficioInss
    {
        private readonly IProviderPaperless _providerPaperless;
        private readonly IProviderConsignado _providerConsignado;
        private readonly IProviderMaxMind _providerMaxMind;

        public BeneficioInssSolicitacaoAutorizacaoCommand(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto, IProviderPaperless providerPaperless
            , IProviderConsignado providerConsignado, IProviderAutenticacao providerAutenticacao, IProviderMaxMind providerMaxMind, ConfiguracaoProviders configuracaoProviders)
            : base(mensagens, usuarioLogin, contexto, providerAutenticacao, configuracaoProviders)
        {
            _providerPaperless = providerPaperless;
            _providerConsignado = providerConsignado;
            _providerMaxMind = providerMaxMind;
        }

        public async Task<SolicitacaoAutorizacaoConsultaBeneficioModel> SolicitarAutorizacaoConsultaBeneficioInss(SolicitacaoAutorizacaoConsultaBeneficioEnvioModel parametrosSolicitacao)
        {
            var dadosUsuarioAutenticado = await ObterDadosUsuarioAutenticado();

            if (_mensagens.PossuiErros)
                return null;

            var autenticacaoProviders = await ObterAutenticacaoParaProviders();

            if (_mensagens.PossuiErros)
                return null;

            var arquivoTermoAutorizacao = await obterArquivoTermoAutorizacao(dadosUsuarioAutenticado, autenticacaoProviders.JwtToken);

            if (_mensagens.PossuiErros)
                return null;

            var idPaperlessDocumento = await enviarTermoParaAssinatura(parametrosSolicitacao, dadosUsuarioAutenticado, arquivoTermoAutorizacao, autenticacaoProviders.JwtToken);

            if (_mensagens.PossuiErros)
                return null;

            var idConsultaBeneficio = await gravarConsultaBeneficio(dadosUsuarioAutenticado.Cliente.ID, idPaperlessDocumento);

            if (_mensagens.PossuiErros)
                return null;

            return new SolicitacaoAutorizacaoConsultaBeneficioModel(idConsultaBeneficio);
        }

        private async Task<byte[]> obterArquivoTermoAutorizacao(UsuarioDominio usuarioAutenticado, string tokenProvider)
        {
            var parametrosObtencaoTermoAutorizacao = new ParametrosAutorizacaoBeneficiarioDto { Cpf = new CPF(usuarioAutenticado.CPF), NomeCliente = usuarioAutenticado.Cliente.Nome };
            var arquivoTermoAutorizacao = await _providerConsignado.ObterTermoAutorizacaoBeneficiario(parametrosObtencaoTermoAutorizacao, tokenProvider);

            if (_mensagens.PossuiErros)
                return null;

            if (arquivoTermoAutorizacao == null)
            {
                _mensagens.AdicionarErro(Mensagens.Beneficio_NaoFoiPossivelObterArquivoTermoAutorizacao, B.Mensagens.EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            return arquivoTermoAutorizacao;
        }

        private async Task<int> enviarTermoParaAssinatura(SolicitacaoAutorizacaoConsultaBeneficioEnvioModel parametrosSolicitacao, UsuarioDominio dadosUsuarioAutenticado, byte[] arquivoTermoAutorizacao, string tokenProviders)
        {
            var parametrosEnvioDocumento = await obterParametrosEnvioDocumento(parametrosSolicitacao, dadosUsuarioAutenticado, arquivoTermoAutorizacao);
            var idPaperlessDocumento = await _providerPaperless.EnviarDocumentoParaAssinatura(parametrosEnvioDocumento, tokenProviders);

            if (_mensagens.PossuiErros)
                return 0;

            if ((idPaperlessDocumento ?? 0) == 0)
            {
                _mensagens.AdicionarErro(Mensagens.Beneficio_NaoHouveRetornoValidoProvedorAssinaturaTermoInss, B.Mensagens.EnumMensagemTipo.negocio);
                return 0;
            }

            return idPaperlessDocumento.Value;
        }

        private async Task<int> gravarConsultaBeneficio(int idCliente, int idPaperlessDocumento)
        {
            var novaConsultaBeneficio = new ConsultaBeneficioInssClienteDominio(idCliente, idPaperlessDocumento);

            await _contexto.AddAsync(novaConsultaBeneficio);
            await _contexto.SaveChangesAsync();

            return novaConsultaBeneficio.ID;
        }

        private async Task<InformacoesEnvioDocumentoParaAssinaturaDto> obterParametrosEnvioDocumento(SolicitacaoAutorizacaoConsultaBeneficioEnvioModel parametrosSolicitacao,
            UsuarioDominio dadosUsuarioAutenticado, byte[] arquivoTermoAutorizacao)
        {
            var enderecoPricipal = await obterEnderecoPrincipal(dadosUsuarioAutenticado.Cliente.ID);

            if (_mensagens.PossuiErros)
                return null;

            var telefoneCelular = await ObterTelefoneCelular(parametrosSolicitacao.IdTelefoneEnvioSolicitacao, dadosUsuarioAutenticado.Cliente.ID);

            if (_mensagens.PossuiErros)
                return null;

            if (verificarSeGeolocalizacaoNaoInformada(parametrosSolicitacao.Latitude, parametrosSolicitacao.Longitude))
            {
                (parametrosSolicitacao.Latitude, parametrosSolicitacao.Longitude) = _providerMaxMind.ObterLatitudeLongitude();
            }

            return new InformacoesEnvioDocumentoParaAssinaturaDto
            {
                CodigoDaCertificadora = Certificadora.Paperless,
                DadosDoDocumento = new DocumentoParaAssinaturaDto
                {
                    MetodoDeCaptura = MetodoCaptura.Base64,
                    IdentificacaoDocumento = Convert.ToBase64String(arquivoTermoAutorizacao),
                    NomeDoDocumento = $"{_configuracaoProviders.Paperless.PrefixoTermoAutorizacao}{dadosUsuarioAutenticado.Cliente.Nome}",
                    ExtensaoDoDocumento = _configuracaoProviders.Paperless.ExtensaoDocumentoTermo
                },
                Signatarios = new List<SignatarioDocumentoDto>
                {
                    new SignatarioDocumentoDto
                    {
                        Assinado = true,
                        Email = dadosUsuarioAutenticado.Email,
                        DDD = telefoneCelular.DDD,
                        Celular = telefoneCelular.Fone,
                        Nome = dadosUsuarioAutenticado.Cliente.Nome,
                        Cpf = dadosUsuarioAutenticado.CPF,
                        Nascimento = dadosUsuarioAutenticado.Cliente.DataNascimento,
                        TipoDeAutenticacao = new TipoDeAutenticacaoAssinaturaDocumentoDto { AutenticacaoPorLiveness = false, AutenticacaoPorSMS = true },
                        EnvioPorSms = true,
                        EnvioPorEmail = true,
                        SequenciaDaAssinatura = 0,
                        Cidade = enderecoPricipal.Municipio.Descricao,
                        Evidencias = new SignatarioEvidenciaDto
                        {
                            Latitude = parametrosSolicitacao.Latitude,
                            Longitude = parametrosSolicitacao.Longitude,
                            EnderecoIp = _usuarioLogin.EnderecoIpOrigemRequisicao
                        }
                    }
                }
            };
        }

        private async Task<EnderecoClienteDominio> obterEnderecoPrincipal(int idCliente)
        {
            var enderecoPrincipal = await _contexto.EnderecosCliente
                                            .Include(e => e.Municipio)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(e => e.IdCliente.Equals(idCliente) && !e.Deletado && e.Principal);

            if (enderecoPrincipal == null)
            {
                _mensagens.AdicionarErro(Mensagens.Endereco_PrincipalNaoLocalizado);
                return null;
            }

            return enderecoPrincipal;
        }

        private bool verificarSeGeolocalizacaoNaoInformada(double? latitude, double? longitude)
        {
            return latitude == null || longitude == null || latitude == 0 || longitude == 0;
        }
    }
}
