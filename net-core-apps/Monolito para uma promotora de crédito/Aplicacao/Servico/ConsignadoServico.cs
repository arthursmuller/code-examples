using Aplicacao.Model.Consignado;
using Aplicacao.Model.RendimentoCliente;
using AutoMapper;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Auth.Dto;
using Infraestrutura.Providers.Consignado;
using Infraestrutura.Providers.Consignado.Dto;
using Infraestrutura.Providers.Consignado.Dto.SimulacaoPortabilidade;
using Infraestrutura.Providers.Dto;
using Infraestrutura.Utils;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class ConsignadoServico : ServicoBase, IConsignadoServico
    {
        private readonly IProviderConsignado _providerConsignado;
        private readonly IProviderAutenticacao _providerAutenticacao;
        private readonly IParametroOperacaoServico _parametroOperacaoServico;
        private readonly ConfiguracaoProviders _configuracaoProviders;

        public ConsignadoServico(
            IBemMensagens mensagens,
            IUsuarioLogin usuarioLogin,
            PlataformaClienteContexto contexto,
            IProviderConsignado providerConsignado,
            IProviderAutenticacao providerAutenticacao,
            IParametroOperacaoServico parametroOperacaoServico,
            ConfiguracaoProviders configuracaoProviders) : base(mensagens, usuarioLogin, contexto)
        {
            _providerConsignado = providerConsignado;
            _providerAutenticacao = providerAutenticacao;
            _parametroOperacaoServico = parametroOperacaoServico;
            _configuracaoProviders = configuracaoProviders;
        }

        public async Task<IEnumerable<RetornoSimulacaoDto>> SimularNovo(SimulacaoNovoEnvioModel parametros)
        {
            var parametrosSimulacao = new ParametrosSimulacaoNovoDto
            {
                Conveniada = ((Convenio)parametros.IdConvenio).GetDescription(),
                ValorOperacao = parametros.ValorOperacao,
                Prestacao = parametros.ValorPrestacao,
                RetornarSomenteOperacoesViaveis = parametros.RetornarSomenteOperacoesViaveis
            };

            var validacaoParametroSimulacaoNovo = new ParametroSimulacaoNovoValidacao().Validate(parametrosSimulacao);

            if (!validacaoParametroSimulacaoNovo.IsValid)
            {
                foreach (var erro in validacaoParametroSimulacaoNovo.Errors)
                {
                    _mensagens.AdicionarErro(erro.ErrorMessage, EnumMensagemTipo.formulario);
                }

                return null;
            }

            var operacoesPermitidas = await _parametroOperacaoServico.BuscarParametrosOperacao();
            if (operacoesPermitidas == null)
                return null;

            var autenticacao = await obterAutenticacao();

            if (_mensagens.PossuiErros)
                return null;

            var simulacoes = await _providerConsignado.SimularNovo(parametrosSimulacao, autenticacao.JwtToken);

            if (simulacoes == null || !simulacoes.Any())
            {
                _mensagens.AdicionarAlerta(Mensagens.Simulacao_NaoRetornouResultadosComParametrosInformados, EnumMensagemTipo.negocio);

                return null;
            }

            var simulacoesPermitidas =
                from simulacao in simulacoes
                join operacao in operacoesPermitidas on
                    new { QuantidadeParcelas = simulacao.Prazo, TaxaPlano = simulacao.TaxaMes.ToString().Replace(",", ".") } equals new { operacao.QuantidadeParcelas, operacao.TaxaPlano }
                where
                    operacao.TipoOperacao.ID == (int)TipoOperacao.Novo
                    && operacao.Convenio.ID == parametros.IdConvenio
                select simulacao;

            if (simulacoesPermitidas == null)
                _mensagens.AdicionarAlerta(Mensagens.Simulacao_NaoRetornouResultadosComParametrosConfigurados, EnumMensagemTipo.negocio);

            return simulacoesPermitidas;
        }

        public async Task<IEnumerable<RetornoSimulacaoDto>> SimularRefinanciamento(SimulacaoRefinanciamentoEnvioModel parametros)
        {
            var usuario = await ObterDadosUsuarioAutenticado();

            if (_mensagens.PossuiErros)
                return null;

            var autenticacao = await obterAutenticacao();

            if (_mensagens.PossuiErros)
                return null;

            var parametrosSimulacao = new ParametrosSimulacaoRefinanciamentoDto
            {
                CPF = usuario.CPF,
                Conveniada = ((Convenio)parametros.IdConvenio).GetDescription(),
                ValorOperacao = parametros.ValorOperacao,
                Prestacao = parametros.Prestacao,
                Plano = parametros.Plano,
                Prazo = parametros.Prazo,
                Prazos = parametros.Prazos,
                DataNascimento = usuario.Cliente?.DataNascimento,
                RetornarSomenteOperacoesViaveis = parametros.RetornarSomenteOperacoesViaveis,
                Proposta = parametros.Proposta,
                ContratosRefinanciamento = parametros?.ContratosRefinanciamento?.Select(contrato => new ContratoSimulacaoRefinDto { Contrato = contrato.Contrato })
            };

            var simulacoes = await _providerConsignado.SimularRefinanciamento(parametrosSimulacao, autenticacao.JwtToken);

            if (simulacoes == null || !simulacoes.Any())
            {
                _mensagens.AdicionarAlerta(Mensagens.Simulacao_NaoRetornouResultadosComParametrosInformados, EnumMensagemTipo.negocio);

                return null;
            }

            simulacoes = simulacoes
                .GroupBy(g => g.Plano)
                .SelectMany(s => s
                    .Where(i => i.TaxaMes == s.Max(m => m.TaxaMes)))
                .Distinct();

            return simulacoes;
        }

        public async Task<SimulacaoPortabilidadeModel> SimularPortabilidade(SimulacaoPortabilidadeEnvioModel parametrosSimulacao)
        {
            var usuario = await ObterDadosUsuarioAutenticado();

            if (_mensagens.PossuiErros)
                return null;

            var rendimento = await obterRendimentoCliente(parametrosSimulacao.IdRendimentoCliente);

            if (_mensagens.PossuiErros)
                return null;

            var autenticacao = await obterAutenticacao();

            if (_mensagens.PossuiErros)
                return null;

            var parametrosDto = obterParametrosSimulacaoPortabilidade(parametrosSimulacao, rendimento.IdConvenio, usuario.Cliente);
            var simulacaoPortabilidade = await _providerConsignado.SimularPropostaPortabilidade(parametrosDto, autenticacao.JwtToken);

            if (_mensagens.PossuiErros)
                return null;

            var simulacaoModel = obterRetornoSimulacaoPortabilidadeModel(simulacaoPortabilidade);

            return simulacaoModel;
        }

        public async Task<IEnumerable<ContratoClienteModel>> ListarContratosCliente(ContratoClienteEnvioModel parametros)
        {
            var usuario = await ObterDadosUsuarioAutenticado();

            if (_mensagens.PossuiErros)
                return null;

            if (!verificarAutorizacaoParaImportacaoDeDados(usuario))
                return null;

            var parametrosContrato = obterParametrosProviderContrato(parametros, usuario);

            var autenticacao = await obterAutenticacao();

            if (_mensagens.PossuiErros)
                return null;

            var contratos = await _providerConsignado.ListarContratosCliente(parametrosContrato, autenticacao.JwtToken);

            return converterParaClienteContratoModel(contratos);
        }

        public async Task<RendimentoSiapeConsultaMargemModel> ConsultarMargemSiape(string orgao, string matricula)
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            if (_mensagens.PossuiErros)
                return null;

            var autenticacao = await obterAutenticacao();

            if (_mensagens.PossuiErros)
                return null;

            var parametrosSimulacao = new ParametrosConsultaMargemDto
            {
                Cpf = usuario.CPF,
                Orgao = orgao,
                Matricula = matricula,
            };

            var resultado = await _providerConsignado.ConsultarMargemSiape(parametrosSimulacao, autenticacao.JwtToken);

            if (resultado == null || _mensagens.PossuiErros)
            {
                _mensagens.AdicionarErro(Mensagens.ConsultaMargemSiape_Indisponivel, EnumMensagemTipo.comunicacaoapi);
                return null;
            }

            if (!resultado.Sucesso)
            {
                if (resultado.MensagemRetorno.Contains("SIGEPE"))
                {
                    _mensagens.AdicionarErro(Mensagens.ConsultaMargemSiape_CadastroBancoPendente, EnumMensagemTipo.comunicacaoapi);

                    return new RendimentoSiapeConsultaMargemModel { PendenteInformacoesBanco = true };
                }
                else
                {
                    _mensagens.AdicionarErro(Mensagens.ConsultaMargemSiape_Indisponivel, EnumMensagemTipo.comunicacaoapi);

                    return null;
                }
            }

            if (!resultado.MargemSiapeItens.Any())
                return null;

            return new RendimentoSiapeConsultaMargemModel
            {
                Items = resultado.MargemSiapeItens.Select(item => new RendimentoSiapeConsultaMargemItemModel
                {
                    Orgao = item.Orgao,
                    Matricula = item.Matricula,
                    Instituidor = item.Instituidor,
                    ValorMaximoParcela = item.ValorMaximoParcela,
                    EmprestimoAutorizado = item.EmprestimoAutorizado,
                    PortabilidadeAutorizada = item.PortabilidadeAutorizada,
                })
            };
        }

        private ParametrosContratoClienteDto obterParametrosProviderContrato(ContratoClienteEnvioModel parametros, UsuarioDominio usuario)
        {
            return new ParametrosContratoClienteDto
            {
                CpfCliente = usuario.CPF,
                Matricula = parametros.Matricula,
            };
        }

        private IEnumerable<ContratoClienteModel> converterParaClienteContratoModel(IEnumerable<RetornoContratoClienteDto> contratos)
        {
            return contratos.Select(contrato => new ContratoClienteModel
            {
                Matricula = contrato.Matricula,
                Contrato = contrato.ContratoCodigo,
                Prestacao = contrato.PMTOriginal,
                QtdParcelas = contrato.PrazoCodigo,
                QtdParcelasPagas = contrato.ParcelasPagas,
                Taxa = contrato.Taxa,
                SaldoTotal = contrato.ValorQuitacao
            });
        }

        private async Task<RendimentoClienteDominio> obterRendimentoCliente(int idRendimentoCliente)
        {
            var usuario = await ObterDadosUsuarioAutenticado();

            if (_mensagens.PossuiErros)
                return null;

            var rendimentoCliente = await _contexto.RendimentoCliente
                            .AsNoTracking()
                            .FirstOrDefaultAsync(r => r.ID.Equals(idRendimentoCliente) && r.IdCliente.Equals(usuario.Cliente.ID) && !r.Deletado);

            if (rendimentoCliente == null)
            {
                _mensagens.AdicionarErro(Mensagens.Rendimento_NaoLocalizado, EnumMensagemTipo.banco);
                return null;
            }

            return rendimentoCliente;
        }

        private ParametrosSimulacaoPortabilidadeDto obterParametrosSimulacaoPortabilidade(SimulacaoPortabilidadeEnvioModel parametrosSimulacao, Convenio convenio, ClienteDominio cliente)
        {
            var parametrosDto = Mapper.Map<ParametrosSimulacaoPortabilidadeDto>(parametrosSimulacao);
            parametrosDto.Conveniada = convenio.GetDescription();
            parametrosDto.SimulacaoEspecial = false;
            parametrosDto.DataNascimento = cliente.DataNascimento?.Date;

            return parametrosDto;
        }

        private SimulacaoPortabilidadeModel obterRetornoSimulacaoPortabilidadeModel(RetornoSimulacaoPortabilidadeDto simulacaoPortabilidade)
        {
            var simulacao = Mapper.Map<SimulacaoPortabilidadeModel>(simulacaoPortabilidade);

            simulacao.SimulacoesIntencaoRefinanciamento = simulacao.SimulacoesIntencaoRefinanciamento
                                                            .GroupBy(g => g.Plano)
                                                            .SelectMany(s => s
                                                                .Where(i => i.TaxaMes == s.Max(m => m.TaxaMes)))
                                                            .Distinct();

            return simulacao;
        }

        private bool verificarAutorizacaoParaImportacaoDeDados(UsuarioDominio usuario)
        {
            if (!(usuario.Cliente.ImportacaoDadosAutorizada ?? false))
            {
                _mensagens.AdicionarAlerta(Mensagens.Cliente_NaoHaRegistrodeAutorizacaoParaImportacaoDeDados, EnumMensagemTipo.negocio);
                return false;
            }

            return true;
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
