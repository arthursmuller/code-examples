using Aplicacao.Model.ContaCliente;
using Aplicacao.Model.RendimentoCliente;
using Aplicacao.Servico;
using Dominio;
using Dominio.Enum;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class RendimentoClienteServicoTeste : ServicoTesteBase
    {
        private RendimentoClienteServico _rendimentoClienteServico;
        private readonly UsuarioDominio _usuarioTeste;

        public RendimentoClienteServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();

            var contaClienteServico = new ContaClienteServico(_mensagens, _usuarioLogin, _contexto);

            _rendimentoClienteServico = new RendimentoClienteServico(_mensagens, _usuarioLogin, _contexto, null, null, null, contaClienteServico);
        }

        [Fact]
        public async Task GravarRendimento_QuandoSIAPE_DevePersistir()
        {
            await criarDados();

            var requisicao = new RendimentoClienteModel
            {
                Convenio = Convenio.SIAPE,
                IdConvenioOrgao = 1,
                IdUf = 1,
                ValorRendimento = 800,
                Matricula = "0001",
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

            var retorno = await _rendimentoClienteServico.GravarRendimento(requisicao);

            var rendimentoNovoBanco = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteSiapeDominio>()
                .FirstOrDefault(r => r.ID == retorno.Id);

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(rendimentoNovoBanco);
        }

        [Fact]
        public async Task GravarRendimento_QuandoINSS_DevePersistir()
        {
            await criarDados();

            var requisicao = new RendimentoClienteModel
            {
                Convenio = Convenio.INSS,
                IdConvenioOrgao = 1,
                IdUf = 1,
                ValorRendimento = 800,
                Matricula = "0001",
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

            var retorno = await _rendimentoClienteServico.GravarRendimento(requisicao);

            var rendimentoNovoBanco = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteInssDominio>()
                .FirstOrDefault(r => r.ID == retorno.Id);

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(rendimentoNovoBanco);
        }

        [Fact]
        public async Task BuscarRendimentosPorCliente_QuandoEncontradosSIAPEINSS_DeveRetornar()
        {
            await criarDados();
            await criarRendimentos();

            var retorno = await _rendimentoClienteServico.BuscarRendimentosPorCliente();

            var rendimentosBanco = await consultarRegistrosBanco();

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(rendimentosBanco.Count(), retorno.Count());
            Assert.Contains(retorno, r => r.SiapeTipoFuncional != null);
            Assert.Contains(retorno, r => r.InssEspecieBeneficio != null);
        }

        [Fact]
        public async Task AtualizarRendimento_QuandoINSS_DevePersistir()
        {
            await criarDados();
            var (_, rendimentoInss, conta) = await criarRendimentos();

            var requisicao = new RendimentoClienteModel
            {
                Convenio = rendimentoInss.IdConvenio,
                IdConvenioOrgao = rendimentoInss.IdConvenioOrgao,
                IdUf = rendimentoInss.IdUf,
                ValorRendimento = 8000,
                Matricula = rendimentoInss.Matricula,
                IdInssEspecieBeneficio = 2,
                DataInscricaoBeneficio = rendimentoInss.DataInscricao,
                ContaCliente = new ContaClienteModel()
                {
                    IdContaCliente = rendimentoInss.ContaCliente.ID,
                    IdBanco = 2,
                    IdTipoConta = 1,
                    Agencia = "0001",
                    Conta = "1",
                    IdFormaRecebimento = 1,
                }
            };

            var retorno = await _rendimentoClienteServico.AtualizarRendimento(rendimentoInss.ID, requisicao);

            var rendimentoAtualizado = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteInssDominio>()
                .FirstOrDefault(r => r.ID == rendimentoInss.ID);

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(requisicao.ValorRendimento, rendimentoAtualizado.ValorRendimento);
            Assert.Equal(requisicao.ContaCliente.IdBanco, rendimentoAtualizado.ContaCliente.IdBanco);
            Assert.Equal(requisicao.IdInssEspecieBeneficio, rendimentoAtualizado.IdInssEspecieBeneficio);
        }

        [Fact]
        public async Task AtualizarRendimento_QuandoSIAPE_DevePersistir()
        {
            await criarDados();
            var (rendimentoSiape, _, conta) = await criarRendimentos();

            var requisicao = new RendimentoClienteModel
            {
                Convenio = rendimentoSiape.IdConvenio,
                IdConvenioOrgao = rendimentoSiape.IdConvenioOrgao,
                IdUf = rendimentoSiape.IdUf,
                ValorRendimento = 8000,
                IdSiapeTipoFuncional = rendimentoSiape.IdSiapeTipoFuncional,
                Matricula = rendimentoSiape.Matricula,
                DataAdmissao = rendimentoSiape.DataAdmissao,
                MatriculaInstituidor = rendimentoSiape.MatriculaInstituidor,
                NomeInstituidor = "Bob",
                PossuiRepresentacaoPorProcurador = rendimentoSiape.PossuiRepresentacaoPorProcurador,
                ContaCliente = new ContaClienteModel()
                {
                    IdContaCliente = conta.ID,
                    IdBanco = 2,
                    IdTipoConta = (int)conta.IdTipoConta,
                    Agencia = conta.Agencia,
                    Conta = conta.Conta,
                }
            };

            var retorno = await _rendimentoClienteServico.AtualizarRendimento(rendimentoSiape.ID, requisicao);

            RendimentoClienteSiapeDominio rendimentoAtualizado = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteSiapeDominio>()
                .FirstOrDefault(r => r.ID == rendimentoSiape.ID);

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(requisicao.ValorRendimento, rendimentoAtualizado.ValorRendimento);
            Assert.Equal(conta.IdBanco, rendimentoAtualizado.ContaCliente.IdBanco);
            Assert.Equal(requisicao.NomeInstituidor, rendimentoAtualizado.NomeInstituidor);
        }

        [Fact]
        public async Task AtualizarRendimento_QuandoMesmaConta_DeveAtualizar()
        {
            await criarDados();
            var (rendimentoSiape, _, conta) = await criarRendimentos();

            var requisicao = new RendimentoClienteModel
            {
                Convenio = rendimentoSiape.IdConvenio,
                IdConvenioOrgao = rendimentoSiape.IdConvenioOrgao,
                IdUf = rendimentoSiape.IdUf,
                ValorRendimento = 8000,
                IdSiapeTipoFuncional = rendimentoSiape.IdSiapeTipoFuncional,
                Matricula = rendimentoSiape.Matricula,
                DataAdmissao = rendimentoSiape.DataAdmissao,
                MatriculaInstituidor = rendimentoSiape.MatriculaInstituidor,
                NomeInstituidor = "Bob",
                PossuiRepresentacaoPorProcurador = rendimentoSiape.PossuiRepresentacaoPorProcurador,
                ContaCliente = new ContaClienteModel()
                {
                    IdContaCliente = conta.ID,
                    IdBanco = 2,
                    IdTipoConta = (int)conta.IdTipoConta,
                    Agencia = conta.Agencia,
                    Conta = conta.Conta,
                    IdFormaRecebimento = (int)conta.IdFormaRecebimento,
                }
            };

            var retorno = await _rendimentoClienteServico.AtualizarRendimento(rendimentoSiape.ID, requisicao);

            RendimentoClienteSiapeDominio rendimentoAtualizado = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteSiapeDominio>()
                .FirstOrDefault(r => r.ID == rendimentoSiape.ID);

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(requisicao.ContaCliente.IdContaCliente, rendimentoAtualizado.IdContaCliente);
            Assert.Equal(requisicao.ContaCliente.IdContaCliente, rendimentoAtualizado.IdContaClienteRecebimento);
        }

        [Fact]
        public async Task AtualizarRendimento_QuandoNovaConta_DeveCriar()
        {
            await criarDados();
            var (rendimentoSiape, _, conta) = await criarRendimentos();

            var requisicao = new RendimentoClienteModel
            {
                Convenio = rendimentoSiape.IdConvenio,
                IdConvenioOrgao = rendimentoSiape.IdConvenioOrgao,
                IdUf = rendimentoSiape.IdUf,
                ValorRendimento = 8000,
                IdSiapeTipoFuncional = rendimentoSiape.IdSiapeTipoFuncional,
                Matricula = rendimentoSiape.Matricula,
                DataAdmissao = rendimentoSiape.DataAdmissao,
                MatriculaInstituidor = rendimentoSiape.MatriculaInstituidor,
                NomeInstituidor = "Bob",
                PossuiRepresentacaoPorProcurador = rendimentoSiape.PossuiRepresentacaoPorProcurador,
                ContaCliente = new ContaClienteModel()
                {
                    IdBanco = 2,
                    IdTipoConta = (int)TipoConta.Normal,
                    Agencia = "4321",
                    Conta = "1234",
                    IdFormaRecebimento = (int)conta.IdFormaRecebimento,
                }
            };

            var retorno = await _rendimentoClienteServico.AtualizarRendimento(rendimentoSiape.ID, requisicao);

            RendimentoClienteSiapeDominio rendimentoAtualizado = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteSiapeDominio>()
                .FirstOrDefault(r => r.ID == rendimentoSiape.ID);

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.NotEqual(requisicao.ContaCliente.IdContaCliente, rendimentoAtualizado.IdContaCliente);
            Assert.NotEqual(requisicao.ContaCliente.IdContaCliente, rendimentoAtualizado.IdContaClienteRecebimento);
        }

        [Fact]
        public async Task AtualizarRendimento_QuandoNovaContaRecebimento_DeveCriar()
        {
            await criarDados();
            var (rendimentoSiape, _, conta) = await criarRendimentos();

            var requisicao = new RendimentoClienteModel
            {
                Convenio = rendimentoSiape.IdConvenio,
                IdConvenioOrgao = rendimentoSiape.IdConvenioOrgao,
                IdUf = rendimentoSiape.IdUf,
                ValorRendimento = 8000,
                IdSiapeTipoFuncional = rendimentoSiape.IdSiapeTipoFuncional,
                Matricula = rendimentoSiape.Matricula,
                DataAdmissao = rendimentoSiape.DataAdmissao,
                MatriculaInstituidor = rendimentoSiape.MatriculaInstituidor,
                NomeInstituidor = "Bob",
                PossuiRepresentacaoPorProcurador = rendimentoSiape.PossuiRepresentacaoPorProcurador,
                ContaCliente = new ContaClienteModel()
                {
                    IdContaCliente = conta.ID,
                    IdBanco = 2,
                    IdTipoConta = (int)conta.IdTipoConta,
                    Agencia = conta.Agencia,
                    Conta = conta.Conta,
                    IdFormaRecebimento = (int)conta.IdFormaRecebimento,
                },
                ContaClienteRecebimento = new ContaClienteModel()
                {
                    IdBanco = 2,
                    IdTipoConta = (int)TipoConta.Normal,
                    Agencia = "4321",
                    Conta = "1234",
                    IdFormaRecebimento = (int)conta.IdFormaRecebimento,
                }
            };

            var retorno = await _rendimentoClienteServico.AtualizarRendimento(rendimentoSiape.ID, requisicao);

            RendimentoClienteSiapeDominio rendimentoAtualizado = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteSiapeDominio>()
                .FirstOrDefault(r => r.ID == rendimentoSiape.ID);

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(requisicao.ContaCliente.IdContaCliente, rendimentoAtualizado.IdContaCliente);
            Assert.NotEqual(requisicao.ContaClienteRecebimento.IdContaCliente, rendimentoAtualizado.IdContaCliente);
        }

        [Fact]
        public async Task RemoverRendimento_QuandoExistente_DeveMarcarRemovido()
        {
            var (rendimentoSiape, _, __) = await criarRendimentos();
            var retorno = await _rendimentoClienteServico.RemoverRendimento(rendimentoSiape.ID);

            var rendimentosBanco = await consultarRegistrosBanco();

            Assert.True(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.Single(rendimentosBanco.Where(r => r.Deletado));
        }

        [Fact]
        public async Task ConsultarMargem_QuandoDadosEncontrados_DeveRetornarDadosSemErros()
        {
            await criarDados();
            await criarRendimentos();

            var consignadoServico = new Mock<IConsignadoServico>();
            consignadoServico
                .Setup(s => s.ConsultarMargemSiape(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new RendimentoSiapeConsultaMargemModel());
            _rendimentoClienteServico = new RendimentoClienteServico(_mensagens, _usuarioLogin, _contexto, null, null, consignadoServico.Object, null);

            var margem = await _rendimentoClienteServico.ConsultarMargem(1);

            Assert.NotNull(margem);
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task GravarRendimento_QuandoAeronautica_DevePersistir()
        {
            await criarDados();
            await criarDadosAeronautica();
            var requisicao = criarRencimentoClienteAeronauticaSucesso();

            var retorno = await _rendimentoClienteServico.GravarRendimento(requisicao);
            var rendimentoNovoBanco = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteAeronauticaDominio>()
                .FirstOrDefault(r => r.ID == retorno.Id);

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(rendimentoNovoBanco);
        }

        [Fact]
        public async Task GravarRendimento_QuandoAeronauticaComTipoFuncionalServidorESemCargo_DeveRetornarErro()
        {
            await criarDados();
            await criarDadosAeronautica();
            var requisicao = criarRencimentoClienteAeronauticaServidorSemCargo();

            var retorno = await _rendimentoClienteServico.GravarRendimento(requisicao);
            var rendimentoNovoBanco = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteAeronauticaDominio>()
                .FirstOrDefault(r => r.ID == retorno.Id);

            Assert.Null(retorno);
            Assert.True(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task GravarRendimento_QuandoAeronauticaComTipoFuncionalPensionistaESemCargo_DevePersistir()
        {
            await criarDados();
            await criarDadosAeronautica();
            var requisicao = criarRencimentoClienteAeronauticaPensionistaSemCargo();

            var retorno = await _rendimentoClienteServico.GravarRendimento(requisicao);
            var rendimentoNovoBanco = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteAeronauticaDominio>()
                .FirstOrDefault(r => r.ID == retorno.Id);

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(rendimentoNovoBanco);
        }

        [Fact]
        public async Task GravarRendimento_QuandoAeronauticaSemTipoFuncional_DeveRetornarErro()
        {
            await criarDados();
            await criarDadosAeronautica();
            var requisicao = criarRencimentoClienteAeronauticaSemTipoFuncional();

            var retorno = await _rendimentoClienteServico.GravarRendimento(requisicao);

            Assert.Null(retorno);
            Assert.True(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task AtualizarRendimento_QuandoAeronautica_DevePersistir()
        {
            await criarDados();
            await criarDadosAeronautica();
            var (rendimentoAeronautica, conta) = await criarRendimentoClienteAeronautica();
            var requisicao = new RendimentoClienteModel
            {
                Convenio = rendimentoAeronautica.IdConvenio,
                IdConvenioOrgao = rendimentoAeronautica.IdConvenioOrgao,
                IdUf = rendimentoAeronautica.IdUf,
                ValorRendimento = 8000,
                Matricula = rendimentoAeronautica.Matricula,
                IdAeronauticaTipoFuncional = rendimentoAeronautica.IdAeronauticaTipoFuncional,
                IdAeronauticaCargo = rendimentoAeronautica.IdAeronauticaCargo,
                ContaCliente = new ContaClienteModel()
                {
                    IdContaCliente = rendimentoAeronautica.ContaCliente.ID,
                    IdBanco = 2,
                    IdTipoConta = 1,
                    Agencia = "0001",
                    Conta = "1",
                    IdFormaRecebimento = 1,
                }
            };

            var retorno = await _rendimentoClienteServico.AtualizarRendimento(rendimentoAeronautica.ID, requisicao);
            var rendimentoAtualizado = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteAeronauticaDominio>()
                .FirstOrDefault(r => r.ID == rendimentoAeronautica.ID);

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(requisicao.ValorRendimento, rendimentoAtualizado.ValorRendimento);
            Assert.Equal(requisicao.ContaCliente.IdBanco, rendimentoAtualizado.ContaCliente.IdBanco);
        }

        [Fact]
        public async Task GravarRendimento_QuandoMarinha_DevePersistir()
        {
            await criarDados();
            await criarDadosMarinha();
            var requisicao = criarRencimentoClienteMarinhaSucesso();

            var retorno = await _rendimentoClienteServico.GravarRendimento(requisicao);
            var rendimentoNovoBanco = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteMarinhaDominio>()
                .FirstOrDefault(r => r.ID == retorno.Id);

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(rendimentoNovoBanco);
        }

        [Fact]
        public async Task GravarRendimento_QuandoMarinhaComTipoFuncionalServidorESemCargo_DeveRetornarErro()
        {
            await criarDados();
            await criarDadosMarinha();
            var requisicao = criarRencimentoClienteMarinhaServidorSemCargo();

            var retorno = await _rendimentoClienteServico.GravarRendimento(requisicao);
            var rendimentoNovoBanco = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteMarinhaDominio>()
                .FirstOrDefault(r => r.ID == retorno.Id);

            Assert.Null(retorno);
            Assert.True(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task GravarRendimento_QuandoMarinhaComTipoFuncionalPensionistaESemCargo_DevePersistir()
        {
            await criarDados();
            await criarDadosMarinha();
            var requisicao = criarRencimentoClienteMarinhaPensionistaSemCargo();

            var retorno = await _rendimentoClienteServico.GravarRendimento(requisicao);
            var rendimentoNovoBanco = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteMarinhaDominio>()
                .FirstOrDefault(r => r.ID == retorno.Id);

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(rendimentoNovoBanco);
        }

        [Fact]
        public async Task GravarRendimento_QuandoMarinhaSemTipoFuncional_DeveRetornarErro()
        {
            await criarDados();
            await criarDadosMarinha();
            var requisicao = criarRencimentoClienteMarinhaSemTipoFuncional();

            var retorno = await _rendimentoClienteServico.GravarRendimento(requisicao);

            Assert.Null(retorno);
            Assert.True(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task AtualizarRendimento_QuandoMarinha_DevePersistir()
        {
            await criarDados();
            await criarDadosMarinha();
            var (rendimentoMarinha, conta) = await criarRendimentoClienteMarinha();
            var requisicao = new RendimentoClienteModel
            {
                Convenio = rendimentoMarinha.IdConvenio,
                IdConvenioOrgao = rendimentoMarinha.IdConvenioOrgao,
                IdUf = rendimentoMarinha.IdUf,
                ValorRendimento = 8000,
                Matricula = rendimentoMarinha.Matricula,
                IdMarinhaTipoFuncional = rendimentoMarinha.IdMarinhaTipoFuncional,
                IdMarinhaCargo = rendimentoMarinha.IdMarinhaCargo,
                ContaCliente = new ContaClienteModel()
                {
                    IdContaCliente = rendimentoMarinha.ContaCliente.ID,
                    IdBanco = 2,
                    IdTipoConta = 1,
                    Agencia = "0001",
                    Conta = "1",
                    IdFormaRecebimento = 1,
                }
            };

            var retorno = await _rendimentoClienteServico.AtualizarRendimento(rendimentoMarinha.ID, requisicao);
            var rendimentoAtualizado = (await consultarRegistrosBanco())
                .OfType<RendimentoClienteMarinhaDominio>()
                .FirstOrDefault(r => r.ID == rendimentoMarinha.ID);

            Assert.NotNull(retorno);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(requisicao.ValorRendimento, rendimentoAtualizado.ValorRendimento);
            Assert.Equal(requisicao.ContaCliente.IdBanco, rendimentoAtualizado.ContaCliente.IdBanco);
        }

        private async Task<(RendimentoClienteSiapeDominio, RendimentoClienteInssDominio, ContaClienteDominio)> criarRendimentos()
        {
            var conta = new ContaClienteDominio(_usuarioTeste.Cliente.ID, 1, TipoConta.Normal, "0001", "000001", FormaRecebimento.TED);
            await _contexto.AddAsync(conta);
            await _contexto.SaveChangesAsync();
            
            var rendimentoSiape = new RendimentoClienteSiapeDominio(conta.ID, conta.ID, _usuarioTeste.Cliente.ID, Convenio.SIAPE, 2, 1, 1000, "1000000000", 1, "1", false, DateTime.Now, "Billy");
            var rendimentoInss = new RendimentoClienteInssDominio(conta.ID, conta.ID, _usuarioTeste.Cliente.ID, Convenio.INSS, 1, 1, 2000, "2000000000", 1, DateTime.Now);

            await _contexto.AddAsync(rendimentoSiape);
            await _contexto.AddAsync(rendimentoInss);
            await _contexto.SaveChangesAsync();

            return (rendimentoSiape, rendimentoInss, conta);
        }

        private async Task<(RendimentoClienteMarinhaDominio, ContaClienteDominio)> criarRendimentoClienteMarinha()
        {
            var tipoFuncional = _contexto.MarinhaTiposFuncionais.FirstOrDefault(n => n.Sigla == "S");
            var cargo = _contexto.MarinhaCargos.FirstOrDefault();

            var conta = new ContaClienteDominio(_usuarioTeste.Cliente.ID, 1, TipoConta.Normal, "0001", "000001", FormaRecebimento.TED);
            await _contexto.AddAsync(conta);
            await _contexto.SaveChangesAsync();

            var rendimentoMarinha = new RendimentoClienteMarinhaDominio(conta.ID, conta.ID, _usuarioTeste.Cliente.ID, Convenio.MARINHA, 3, 1, 2000, "2000000000", tipoFuncional.ID, cargo.ID);

            await _contexto.AddAsync(rendimentoMarinha);
            await _contexto.SaveChangesAsync();

            return (rendimentoMarinha, conta);
        }

        private async Task<(RendimentoClienteAeronauticaDominio, ContaClienteDominio)> criarRendimentoClienteAeronautica()
        {
            var tipoFuncional = _contexto.AeronauticaTiposFuncionais.FirstOrDefault(n => n.Sigla == "S");
            var cargo = _contexto.AeronauticaCargos.FirstOrDefault();

            var conta = new ContaClienteDominio(_usuarioTeste.Cliente.ID, 1, TipoConta.Normal, "0001", "000001", FormaRecebimento.TED);
            await _contexto.AddAsync(conta);
            await _contexto.SaveChangesAsync();

            var rendimentoAeronautica = new RendimentoClienteAeronauticaDominio(conta.ID, conta.ID, _usuarioTeste.Cliente.ID, Convenio.AERONAUTICA, 3, 1, 2000, "2000000000", tipoFuncional.ID, cargo.ID);

            await _contexto.AddAsync(rendimentoAeronautica);
            await _contexto.SaveChangesAsync();

            return (rendimentoAeronautica, conta);
        }

        private async Task<List<RendimentoClienteDominio>> consultarRegistrosBanco()
            => await _contexto.RendimentoCliente.ToListAsync();

        private async Task criarDados()
        {
            await _contexto.AddAsync(new ConvenioDominio(Convenio.INSS, "INSS", "000020", ""));
            await _contexto.AddAsync(new ConvenioDominio(Convenio.SIAPE, "SIAPE", "000021", ""));
            await _contexto.SaveChangesAsync();

            await _contexto.AddAsync(new ConvenioOrgaoDominio("00001", "00394411000109", "INSS", Convenio.INSS, null));
            await _contexto.SaveChangesAsync();

            await _contexto.AddAsync(new ConvenioOrgaoDominio("00002", "00394411000109", "SIAPE 1", Convenio.SIAPE, null));
            await _contexto.SaveChangesAsync();

            await _contexto.AddAsync(new ConvenioOrgaoDominio("00003", "00394411000109", "SIAPE 2", Convenio.SIAPE, null));

            await _contexto.AddAsync(new UnidadeFederativaDominio("Rio Grande do Sul", "RS"));
            await _contexto.AddAsync(new TipoContaDominio(TipoConta.Normal, "Conta Bacana", "CB"));
            await _contexto.AddAsync(new BancoDominio("1234", "123456789", "Hehe Bank", false));
            await _contexto.AddAsync(new BancoDominio("12345", "1234567890", "Jajaja Bank", false));
            await _contexto.AddAsync(new SiapeTipoFuncionalDominio("S", "Servidor"));
            await _contexto.AddAsync(new InssEspecieBeneficioDominio("04", "Aposentadoria por invalidez do trabalhador rural (Lei Complementar no 11/71)"));
            await _contexto.AddAsync(new InssEspecieBeneficioDominio("05", "Aposentadoria por invalidez do trabalhador urbano (Lei Complementar no 11/71)"));

            await _contexto.AddAsync(new FormaRecebimentoDominio(FormaRecebimento.TED, "TED"));
            
            await _contexto.SaveChangesAsync();
        }

        private async Task criarDadosMarinha()
        {
            await _contexto.AddAsync(new ConvenioDominio(Convenio.MARINHA, "MARINHA", "000014", ""));
            await _contexto.SaveChangesAsync();

            await _contexto.AddAsync(new ConvenioOrgaoDominio("00004", "00394411000109", "MARINHA", Convenio.MARINHA, null));
            await _contexto.SaveChangesAsync();

            await _contexto.AddAsync(new MarinhaTipoFuncionalDominio("S", "Servidor"));
            await _contexto.AddAsync(new MarinhaTipoFuncionalDominio("P", "Pensionista"));
            await _contexto.AddAsync(new MarinhaCargoDominio(1, "ALTE", "Almirante"));
            await _contexto.SaveChangesAsync();
        }

        private RendimentoClienteModel criarRencimentoClienteMarinhaSucesso()
        {
            var tipoFuncional = _contexto.MarinhaTiposFuncionais.FirstOrDefault(n => n.Sigla == "S");
            var cargo = _contexto.MarinhaCargos.FirstOrDefault();
            return new RendimentoClienteModel
            {
                Convenio = Convenio.MARINHA,
                IdConvenioOrgao = 4,
                IdUf = 1,
                ValorRendimento = 800,
                Matricula = "0001",
                IdMarinhaTipoFuncional = tipoFuncional.ID,
                IdMarinhaCargo = cargo.ID,
                ContaCliente = new ContaClienteModel()
                {
                    IdBanco = 1,
                    IdTipoConta = 1,
                    Agencia = "0001",
                    Conta = "1",
                    IdFormaRecebimento = 1,
                }
            };
        }

        private RendimentoClienteModel criarRencimentoClienteMarinhaServidorSemCargo()
        {
            var tipoFuncional = _contexto.MarinhaTiposFuncionais.FirstOrDefault(n => n.Sigla == "S");
            return new RendimentoClienteModel
            {
                Convenio = Convenio.MARINHA,
                IdConvenioOrgao = 4,
                IdUf = 1,
                ValorRendimento = 800,
                Matricula = "0001",
                IdMarinhaTipoFuncional = tipoFuncional.ID,
                ContaCliente = new ContaClienteModel()
                {
                    IdBanco = 1,
                    IdTipoConta = 1,
                    Agencia = "0001",
                    Conta = "1",
                    IdFormaRecebimento = 1,
                }
            };
        }

        private RendimentoClienteModel criarRencimentoClienteMarinhaPensionistaSemCargo()
        {
            var tipoFuncional = _contexto.MarinhaTiposFuncionais.FirstOrDefault(n => n.Sigla == "P");
            return new RendimentoClienteModel
            {
                Convenio = Convenio.MARINHA,
                IdConvenioOrgao = 4,
                IdUf = 1,
                ValorRendimento = 800,
                Matricula = "0001",
                IdMarinhaTipoFuncional = tipoFuncional.ID,
                ContaCliente = new ContaClienteModel()
                {
                    IdBanco = 1,
                    IdTipoConta = 1,
                    Agencia = "0001",
                    Conta = "1",
                    IdFormaRecebimento = 1,
                }
            };
        }

        private RendimentoClienteModel criarRencimentoClienteMarinhaSemTipoFuncional()
        {
            return new RendimentoClienteModel
            {
                Convenio = Convenio.MARINHA,
                IdConvenioOrgao = 4,
                IdUf = 1,
                ValorRendimento = 800,
                Matricula = "0001",
                ContaCliente = new ContaClienteModel()
                {
                    IdBanco = 1,
                    IdTipoConta = 1,
                    Agencia = "0001",
                    Conta = "1",
                    IdFormaRecebimento = 1,
                }
            };
        }


        private async Task criarDadosAeronautica()
        {
            await _contexto.AddAsync(new ConvenioDominio(Convenio.AERONAUTICA, "AERONAUTICA", "002265", ""));
            await _contexto.SaveChangesAsync();

            await _contexto.AddAsync(new ConvenioOrgaoDominio("00005", "00394411000109", "AERONAUTICA", Convenio.AERONAUTICA, null));
            await _contexto.SaveChangesAsync();

            await _contexto.AddAsync(new AeronauticaTipoFuncionalDominio("S", "Servidor"));
            await _contexto.AddAsync(new AeronauticaTipoFuncionalDominio("P", "Pensionista"));
            await _contexto.AddAsync(new AeronauticaCargoDominio(42, "MA", "01 MARECHAL-DO-AR"));
            await _contexto.SaveChangesAsync();
        }

        private RendimentoClienteModel criarRencimentoClienteAeronauticaSucesso()
        {
            var tipoFuncional = _contexto.AeronauticaTiposFuncionais.FirstOrDefault(n => n.Sigla == "S");
            var cargo = _contexto.AeronauticaCargos.FirstOrDefault();
            return new RendimentoClienteModel
            {
                Convenio = Convenio.AERONAUTICA,
                IdConvenioOrgao = 4,
                IdUf = 1,
                ValorRendimento = 800,
                Matricula = "0001",
                IdAeronauticaTipoFuncional = tipoFuncional.ID,
                IdAeronauticaCargo = cargo.ID,
                ContaCliente = new ContaClienteModel()
                {
                    IdBanco = 1,
                    IdTipoConta = 1,
                    Agencia = "0001",
                    Conta = "1",
                    IdFormaRecebimento = 1,
                }
            };
        }

        private RendimentoClienteModel criarRencimentoClienteAeronauticaServidorSemCargo()
        {
            var tipoFuncional = _contexto.AeronauticaTiposFuncionais.FirstOrDefault(n => n.Sigla == "S");
            return new RendimentoClienteModel
            {
                Convenio = Convenio.AERONAUTICA,
                IdConvenioOrgao = 4,
                IdUf = 1,
                ValorRendimento = 800,
                Matricula = "0001",
                IdAeronauticaTipoFuncional = tipoFuncional.ID,
                ContaCliente = new ContaClienteModel()
                {
                    IdBanco = 1,
                    IdTipoConta = 1,
                    Agencia = "0001",
                    Conta = "1",
                    IdFormaRecebimento = 1,
                }
            };
        }

        private RendimentoClienteModel criarRencimentoClienteAeronauticaPensionistaSemCargo()
        {
            var tipoFuncional = _contexto.AeronauticaTiposFuncionais.FirstOrDefault(n => n.Sigla == "P");
            return new RendimentoClienteModel
            {
                Convenio = Convenio.AERONAUTICA,
                IdConvenioOrgao = 4,
                IdUf = 1,
                ValorRendimento = 800,
                Matricula = "0001",
                IdAeronauticaTipoFuncional = tipoFuncional.ID,
                ContaCliente = new ContaClienteModel()
                {
                    IdBanco = 1,
                    IdTipoConta = 1,
                    Agencia = "0001",
                    Conta = "1",
                    IdFormaRecebimento = 1,
                }
            };
        }

        private RendimentoClienteModel criarRencimentoClienteAeronauticaSemTipoFuncional()
        {
            return new RendimentoClienteModel
            {
                Convenio = Convenio.AERONAUTICA,
                IdConvenioOrgao = 4,
                IdUf = 1,
                ValorRendimento = 800,
                Matricula = "0001",
                ContaCliente = new ContaClienteModel()
                {
                    IdBanco = 1,
                    IdTipoConta = 1,
                    Agencia = "0001",
                    Conta = "1",
                    IdFormaRecebimento = 1,
                }
            };
        }
    }
}
