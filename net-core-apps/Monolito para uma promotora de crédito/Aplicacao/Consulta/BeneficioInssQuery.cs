using Aplicacao.Model.Beneficio;
using Aplicacao.Model.ContaCliente;
using Aplicacao.Model.RendimentoCliente;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Cliente;
using Infraestrutura.Providers.Cliente.Dto.ListaBeneficio;
using Infraestrutura.Providers.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Consulta
{
    public class BeneficioInssQuery : ServicoBaseSuporteAutenticacao
    {
        private readonly IProviderCliente _providerCliente;
        private readonly IRendimentoClienteServico _rendimentoClienteServico;

        public BeneficioInssQuery(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto, IProviderCliente providerCliente,
            IProviderAutenticacao providerAutenticacao, IRendimentoClienteServico rendimentoClienteServico, ConfiguracaoProviders configuracaoProviders)
            : base(mensagens, usuarioLogin, contexto, providerAutenticacao, configuracaoProviders)
        {
            _providerCliente = providerCliente;
            _rendimentoClienteServico = rendimentoClienteServico;
        }

        public async Task<IEnumerable<ConsultaBeneficioModel>> ConsultarBeneficiosInss(string chaveAutorizacao)
        {
            var dadosUsuarioAutenticado = await ObterDadosUsuarioAutenticado();

            if (_mensagens.PossuiErros)
            {
                _mensagens.AdicionarErro(Mensagens.Falha_auth_token, B.Mensagens.EnumMensagemTipo.exception);
                return null;
            }
                
            var autenticacaoProviders = await ObterAutenticacaoParaProviders();

            if (_mensagens.PossuiErros)
            {
                _mensagens.AdicionarErro(Mensagens.Falha_auth_token, B.Mensagens.EnumMensagemTipo.exception);
                return null;
            }

            var beneficiosInssCliente = await _providerCliente.ListarBeneficiosInss(chaveAutorizacao, autenticacaoProviders.JwtToken);

            if (_mensagens.PossuiErros)
            {
                _mensagens.AdicionarErro(Mensagens.Falha_auth_token, B.Mensagens.EnumMensagemTipo.exception);
                return null;
            }

            await gravarConsultaBeneficio(dadosUsuarioAutenticado.Cliente.ID, chaveAutorizacao);

            if (_mensagens.PossuiErros)
            {
                _mensagens.AdicionarErro(Mensagens.Falha_auth_token, B.Mensagens.EnumMensagemTipo.exception);
                return null;
            }

            await processarAtualizacaoRendimentos(beneficiosInssCliente);

            if (_mensagens.PossuiErros)
            {
                _mensagens.AdicionarErro(Mensagens.Falha_auth_token, B.Mensagens.EnumMensagemTipo.exception);
                return null;
            }

            return beneficiosInssCliente.Select(beneficio => converterParaModel(beneficio));
        }

        private async Task gravarConsultaBeneficio(int idCliente, string chaveAutorizacao)
        {
            var consultaBeneficio = await obterConsultaBeneficio(chaveAutorizacao);
            if (consultaBeneficio == null)
            {
                consultaBeneficio = new ConsultaBeneficioInssClienteDominio(idCliente, chaveAutorizacao);
                await _contexto.AddAsync(consultaBeneficio);
            }

            await _contexto.SaveChangesAsync();
        }

        private async Task<ConsultaBeneficioInssClienteDominio> obterConsultaBeneficio(string chaveAutorizacao)
        {
            return await _contexto.ConsultaBeneficiosInssCliente
                            .Where(c => c.ChaveAutorizacao.Equals(chaveAutorizacao) && c.Cliente.IdUsuario.Equals(_usuarioLogin.IdUsuario))
                            .FirstOrDefaultAsync();
        }

        private async Task processarAtualizacaoRendimentos(IEnumerable<ListagemBeneficiosInssDto> beneficiosInssCliente)
        {
            var rendimentosCliente = await _rendimentoClienteServico.BuscarRendimentosPorCliente();

            if (_mensagens.PossuiErros)
                return;

            foreach (var rendimento in rendimentosCliente)
            {
                var beneficio = beneficiosInssCliente.FirstOrDefault(b => b.NumeroBeneficio.Equals(rendimento.Matricula));
                if (beneficio != null)
                {
                    var rendimentoAtualizacao = await converterParaRendimentoModel(rendimento, beneficio);

                    await _rendimentoClienteServico.AtualizarRendimento(rendimento.Id, rendimentoAtualizacao);

                    if (_mensagens.PossuiErros)
                        return;
                }
            }
        }

        private async Task<RendimentoClienteModel> converterParaRendimentoModel(RendimentoClienteExibicaoModel rendimento, ListagemBeneficiosInssDto beneficio)
        {
            var banco = await obterBancoBeneficio(beneficio.CbcIFPagadora.PadLeft(4, '0').PadRight(4));

            var ufBeneficio = await obterUfBeneficio(beneficio.UfPagamento);

            return new RendimentoClienteModel
            {
                DataInscricaoBeneficio = beneficio.DataDespachoBeneficio ?? rendimento.DataInscricaoBeneficio,
                MargemDisponivel = beneficio.MargemDisponivel,
                MargemDisponivelCartao = beneficio.MargemDisponivelCartao,
                ContaCliente = new ContaClienteModel()
                {
                    Agencia = beneficio.AgenciaPagadora,
                    Conta = beneficio.ContaCorrente,
                    IdTipoConta = (int)converterParaTipoConta(beneficio.TipoCredito),
                    IdBanco = banco?.ID ?? rendimento.ContaCliente.Banco.Id,
                    IdFormaRecebimento = rendimento.ContaCliente.IdFormaRecebimento
                },

                ContaClienteRecebimento = new ContaClienteModel()
                {
                    IdContaCliente = rendimento.ContaClienteRecebimento.IdContaCliente,
                    Agencia = rendimento.ContaClienteRecebimento.Agencia,
                    Conta = rendimento.ContaClienteRecebimento.Conta,
                    IdTipoConta = rendimento.ContaClienteRecebimento.IdTipoConta,
                    IdBanco = rendimento.ContaClienteRecebimento.IdBanco,
                    IdFormaRecebimento = rendimento.ContaClienteRecebimento.IdFormaRecebimento
                },
                Convenio = (Convenio)rendimento.Convenio.ID,
                DataAdmissao = rendimento.DataAdmissao,
                IdConvenioOrgao = rendimento.ConvenioOrgao.Id,
                IdInssEspecieBeneficio = rendimento.InssEspecieBeneficio.Id,
                IdUf = ufBeneficio?.ID ?? rendimento.Uf.Id,
                Matricula = rendimento.Matricula,
                ValorRendimento = rendimento.ValorRendimento,
            };
        }

        private TipoConta converterParaTipoConta(TipoCreditoDto tipoCredito)
         => tipoCredito.Codigo.Equals(1) ? TipoConta.CartaoMagnetico : TipoConta.Normal;

        private async Task<BancoDominio> obterBancoBeneficio(string codigoBanco)
        {
            return await _contexto.Bancos
                            .AsNoTracking()
                            .FirstOrDefaultAsync(b => b.Codigo.Equals(codigoBanco));
        }

        private async Task<UnidadeFederativaDominio> obterUfBeneficio(string ufBanco)
        {
            return await _contexto.UnidadesFederativas
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Sigla.Equals(ufBanco));
        }

        private ConsultaBeneficioModel converterParaModel(ListagemBeneficiosInssDto beneficioDto)
        {
            var beneficioModel = new ConsultaBeneficioModel
            {
                Agencia = beneficioDto.AgenciaPagadora,
                ContaCorrente = beneficioDto.ContaCorrente,
                DataInscricao = beneficioDto.DataDespachoBeneficio,
                Especie = beneficioDto.EspecieBeneficio?.Codigo ?? 0,
                InstituicaoFinanceira = beneficioDto.CbcIFPagadora.PadLeft(4, '0'),
                MargemDisponivel = beneficioDto.MargemDisponivel,
                MargemDisponivelCartao = beneficioDto.MargemDisponivelCartao,
                NumeroBeneficio = beneficioDto.NumeroBeneficio,
                TipoConta = beneficioDto.TipoCredito?.Codigo ?? 0,
                UfRendimento = beneficioDto.UfPagamento
            };

            return beneficioModel;
        }
    }
}
