using Aplicacao.Consulta;
using Aplicacao.Model.Beneficio;
using Aplicacao.Model.ContaCliente;
using Aplicacao.Model.RendimentoCliente;
using Aplicacao.Servico;
using Dominio;
using Dominio.Enum;
using Infraestrutura.Providers;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Auth.Dto;
using Infraestrutura.Providers.Cliente;
using Infraestrutura.Providers.Cliente.Dto.ListaBeneficio;
using Infraestrutura.Providers.Dto;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Servico;
using Xunit;

namespace Teste.Consulta
{
    public class BeneficioConsultasTeste : ServicoTesteBase
    {
        private BeneficioInssQuery _beneficioInssQuery;
        private BeneficioInssAutorizacaoVigenteQuery _beneficioInssAutorizacaoVigenteQuery;
        private ConfiguracaoProviders _configuracaoProviders;
        private UsuarioDominio _usuarioTeste;
        private Mock<IProviderAutenticacao> _providerAutenticacao = new Mock<IProviderAutenticacao>();
        private Mock<IProviderCliente> _providerCliente = new Mock<IProviderCliente>();
        private IRendimentoClienteServico _rendimentoClienteServico;

        public BeneficioConsultasTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();

            _providerAutenticacao
                .Setup(s => s.Autenticar(It.IsAny<ParametroAutenticacaoDto>()))
                .ReturnsAsync(new RetornoAtenticacaoDto());

            _rendimentoClienteServico = new Mock<IRendimentoClienteServico>().Object;
            _configuracaoProviders = new ConfiguracaoProviders { Paperless = new ConfiguracaoProviderPaperless() };
        }

        [Fact]
        public async Task ConsultarBeneficiosInss_EntradaComChaveValidaNaoGeradaNoPCF_DeveGravarLogConsultaSemErros()
        {
            #region Arrange

            var chaveAutorizacao = "54321";

            instanciarBeneficioInssQuery();

            #endregion

            #region Act

            var beneficios = await _beneficioInssQuery.ConsultarBeneficiosInss(chaveAutorizacao);

            await criarDadosConsultaBeneficioInss();
            var logConsultabeneficio = await obterLogConsultaBeneficioInss();

            #endregion

            #region Assert

            Assert.NotNull(beneficios);
            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(logConsultabeneficio);
            Assert.Equal(chaveAutorizacao, logConsultabeneficio.ChaveAutorizacao);
            Assert.Equal(1, logConsultabeneficio.ID);
            Assert.Equal(1, logConsultabeneficio.IdCliente);
            Assert.Null(logConsultabeneficio.IdPaperlessDocumento);

            #endregion
        }

        [Fact]
        public async Task ConsultarBeneficiosInss_EntradaComChaveValidaGeradaNoPCF_DeveGravarLogConsultaSemErros()
        {
            #region Arrange

            var chaveAutorizacao = "12345";

            await criarDadosConsultaBeneficioInss();
            instanciarBeneficioInssQuery();

            #endregion

            #region Act

            var beneficios = await _beneficioInssQuery.ConsultarBeneficiosInss(chaveAutorizacao);
            var logConsultabeneficio = await obterLogConsultaBeneficioInss();

            #endregion

            #region Assert

            Assert.NotNull(beneficios);
            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(logConsultabeneficio);
            Assert.Equal(chaveAutorizacao, logConsultabeneficio.ChaveAutorizacao);
            Assert.Equal(1, logConsultabeneficio.IdCliente);
            Assert.Equal(1, logConsultabeneficio.IdPaperlessDocumento);

            #endregion
        }

        [Fact]
        public async Task ConsultarBeneficiosInss_ClientePossuiRendimento_RendimentoAtualizadoSemErros()
        {
            #region Arrange

            var listaBeneficios = new List<ListagemBeneficiosInssDto>
            {
                new ListagemBeneficiosInssDto
                {
                    CbcIFPagadora = "12",
                    AgenciaPagadora = "0001",
                    ContaCorrente = "000001",
                    DataDespachoBeneficio = DateTime.Now.AddDays(-5),
                    MargemDisponivel = 300,
                    MargemDisponivelCartao = 90,
                    NumeroBeneficio = "0000000000",
                    UfPagamento = "RS",
                    TipoCredito = new TipoCreditoDto{ Codigo = 2 }
                }
            };
            _providerCliente
                .Setup(s => s.ListarBeneficiosInss(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(listaBeneficios);

            _rendimentoClienteServico = new RendimentoClienteServico(_mensagens, _usuarioLogin, _contexto, null, null, null, new ContaClienteServico(_mensagens, _usuarioLogin, _contexto));
            instanciarBeneficioInssQuery();

            await criarDadosConsultaBeneficioInss();
            await criarDadosRendimento();

            #endregion

            #region Act

            var beneficios = await _beneficioInssQuery.ConsultarBeneficiosInss("12345");

            #endregion

            #region Assert

            Assert.NotNull(beneficios);
            Assert.False(_mensagens.PossuiErros);
            Assert.True(await compararDadosRendimentoAtualizado(beneficios));

            #endregion
        }

        [Fact]
        public async Task ObterAutorizacaoVigente_QuandoDadosValidos_DeveRetornarDadosSemErro()
        {
            _beneficioInssAutorizacaoVigenteQuery = new BeneficioInssAutorizacaoVigenteQuery(_mensagens, _usuarioLogin, _contexto, _providerCliente.Object, _providerAutenticacao.Object, _configuracaoProviders);

            var autorizacaoVigente = await _beneficioInssAutorizacaoVigenteQuery.ObterAutorizacaoVigente();

            Assert.NotNull(autorizacaoVigente);
            Assert.False(_mensagens.PossuiErros);
        }

        private async Task<bool> compararDadosRendimentoAtualizado(IEnumerable<ConsultaBeneficioModel> beneficios)
        {
            var rendimentosCliente = await _rendimentoClienteServico.BuscarRendimentosPorCliente();
            var rendimentoInss = rendimentosCliente.First(r => r.Convenio.ID.Equals((int)Convenio.INSS));

            var beneficioInss = beneficios.First();

            if (rendimentoInss.ContaCliente.Banco.Codigo.Equals(beneficioInss.InstituicaoFinanceira)
                && rendimentoInss.ContaCliente.Agencia.Equals(beneficioInss.Agencia)
                && rendimentoInss.ContaCliente.Conta.Equals(beneficioInss.ContaCorrente)
                && rendimentoInss.DataInscricaoBeneficio.Equals(beneficioInss.DataInscricao)
                && rendimentoInss.MargemDisponivel.Equals(beneficioInss.MargemDisponivel)
                && rendimentoInss.MargemDisponivelCartao.Equals(beneficioInss.MargemDisponivelCartao)
                && rendimentoInss.Uf.Sigla.Equals(beneficioInss.UfRendimento)
                && rendimentoInss.Matricula.Equals(beneficioInss.NumeroBeneficio))
                return true;

            return false;
        }

        private void instanciarBeneficioInssQuery()
            => _beneficioInssQuery = new BeneficioInssQuery(_mensagens, _usuarioLogin, _contexto, _providerCliente.Object, _providerAutenticacao.Object, _rendimentoClienteServico, _configuracaoProviders);

        private async Task criarDadosConsultaBeneficioInss()
        {
            var consultaBeneficio = new ConsultaBeneficioInssClienteDominio(_usuarioTeste.Cliente.ID, 1);
            consultaBeneficio.SetChaveAutorizacao("12345");

            await _contexto.ConsultaBeneficiosInssCliente.AddAsync(consultaBeneficio);
            await _contexto.SaveChangesAsync();
        }

        private async Task<ConsultaBeneficioInssClienteDominio> obterLogConsultaBeneficioInss()
        {
            return await _contexto.ConsultaBeneficiosInssCliente
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(c => c.ID.Equals(1));
        }

        private async Task criarDadosRendimento()
        {
            await _contexto.AddAsync(new ConvenioDominio(Convenio.INSS, "INSS", "000020", ""));
            await _contexto.AddAsync(new ConvenioDominio(Convenio.SIAPE, "SIAPE", "000021", ""));
            await _contexto.SaveChangesAsync();

            await _contexto.AddAsync(new ConvenioOrgaoDominio("00001", "00394411000109", "INSS", Convenio.INSS, null));
            await _contexto.AddAsync(new ConvenioOrgaoDominio("00002", "00394411000109", "SIAPE 1", Convenio.SIAPE, null));
            await _contexto.AddAsync(new ConvenioOrgaoDominio("00003", "00394411000109", "SIAPE 2", Convenio.SIAPE, null));
            await _contexto.SaveChangesAsync();

            await _contexto.AddAsync(new UnidadeFederativaDominio("Rio Grande do Sul", "RS"));
            await _contexto.AddAsync(new TipoContaDominio(TipoConta.Normal, "Conta Bacana", "CB"));
            await _contexto.AddAsync(new BancoDominio("0012", "123456789", "Hehe Bank", false));
            await _contexto.AddAsync(new BancoDominio("12345", "1234567890", "Jajaja Bank", false));
            await _contexto.AddAsync(new SiapeTipoFuncionalDominio("S", "Servidor"));
            await _contexto.AddAsync(new InssEspecieBeneficioDominio("04", "Aposentadoria por invalidez do trabalhador rural (Lei Complementar no 11/71)"));
            await _contexto.AddAsync(new InssEspecieBeneficioDominio("05", "Aposentadoria por invalidez do trabalhador urbano (Lei Complementar no 11/71)"));

            await _contexto.AddAsync(new FormaRecebimentoDominio(FormaRecebimento.TED, "TED"));

            await _contexto.SaveChangesAsync();

            var requisicaoInss = new RendimentoClienteModel
            {
                Convenio = Convenio.INSS,
                IdConvenioOrgao = 1,
                IdUf = 1,
                ValorRendimento = 800,
                Matricula = "0000000000",
                IdInssEspecieBeneficio = 1,
                DataInscricaoBeneficio = DateTime.Now,
                ContaCliente = new ContaClienteModel()
                {
                    IdBanco = 1,
                    IdTipoConta = 1,
                    Agencia = "0001",
                    Conta = "1",
                    IdFormaRecebimento = 1,
                }
            };

            await _rendimentoClienteServico.GravarRendimento(requisicaoInss);

            var requisicaoSiape = new RendimentoClienteModel
            {
                Convenio = Convenio.SIAPE,
                IdConvenioOrgao = 1,
                IdUf = 1,
                ValorRendimento = 800,
                Matricula = "0002",
                DataAdmissao = DateTime.Now,
                MatriculaInstituidor = "00001",
                NomeInstituidor = "Bob",
                PossuiRepresentacaoPorProcurador = false,
                ContaCliente = new ContaClienteModel()
                {
                    IdBanco = 1,
                    IdTipoConta = 1,
                    Agencia = "0001",
                    Conta = "1",
                    IdFormaRecebimento = 1,
                }
            };

            await _rendimentoClienteServico.GravarRendimento(requisicaoSiape);
        }
    }
}
