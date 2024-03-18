using Aplicacao.Model.IntencaoOperacao;
using Aplicacao.Servico;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Infraestrutura.Utils;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernel.ValueObjects.v2;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class IntencaoOperacaoServicoTeste : ServicoTesteBase
    {
        private readonly UsuarioDominio _usuarioTeste;
        private readonly IntencaoOperacaoServico _intencaoOperacaoServico;

        public IntencaoOperacaoServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();

            var usuarioLogado = new UsuarioLoginDominio()
            {
                IdUsuario = _usuarioTeste.ID,
            };

            var rendimentoClienteServico = new RendimentoClienteServico(
                _mensagens,
                _usuarioLogin,
                _contexto,
                It.IsAny<ConvenioServico>(),
                It.IsAny<BancarioServico>(),
                It.IsAny<IConsignadoServico>(),
                It.IsAny<IContaClienteServico>()
            );

            _intencaoOperacaoServico = new IntencaoOperacaoServico(_contexto, _mensagens, usuarioLogado, rendimentoClienteServico);
        }

        [Fact]
        public async Task ListarIntencoesOperacaoAutenticado_QuandoNaoExistir_DeveRetornarErro()
        {
            var resultado = await _intencaoOperacaoServico.ListarIntencoesOperacaoAutenticado();

            Assert.Contains(_mensagens.BuscarAlertas(), m => m.Mensagem.Equals(Mensagens.IntencaoOperacao_NenhumaEncontrada));
            Assert.Null(resultado);
        }

        [Fact]
        public async Task GravarIntencaoOperacao_ProdutoValido_DeveCadastrarAsync()
        {
            await criarDados();

            var requisicao = new IntencaoOperacaoCriacaoModel()
            {
                IdTipoOperacao = (int)TipoOperacao.Novo,
                IdProduto = (int)Produto.CartaoCreditoConsignado,
                Prestacao = 999,
                ValorAuxilioFinanceiro = 999,
                TaxaMes = 999,
                TaxaAno = 9,
                ValorFinanciado = 99,
            };

            var resultado = await _intencaoOperacaoServico.GravarIntencaoOperacao(requisicao);

            var resultadoBanco = await consultarBancoIntencao();
            var passosProduto = await consultarPassos();

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultadoBanco);
            Assert.Single(resultadoBanco.Historico);
            Assert.Equal(passosProduto.First(p => p.PassoInicial).SituacaoIntencaoOperacao.ID, resultadoBanco.Situacao.ID);
            Assert.NotNull(resultado?.Id);
        }

        [Fact]
        public async Task GravarIntencaoOperacao_RendimentoNaoExistenteParaCliente_RetornaNullComMensagemDeErro()
        {
            await criarDados();

            var requisicao = new IntencaoOperacaoCriacaoModel()
            {
                IdTipoOperacao = (int)TipoOperacao.Novo,
                IdProduto = (int)Produto.CartaoCreditoConsignado,
                Prestacao = 999,
                ValorAuxilioFinanceiro = 999,
                TaxaMes = 999,
                TaxaAno = 9,
                ValorFinanciado = 99,
                IdRendimentoCliente = 1
            };

            var resultado = await _intencaoOperacaoServico.GravarIntencaoOperacao(requisicao);

            Assert.Null(resultado);
            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), m => m.Mensagem.Equals(Mensagens.Rendimento_NaoLocalizadoParaUsuario));
        }

        [Fact]
        public async Task GravarIntencaoOperacao_PortabilidadeDadosFaltantes_RetornaNullComMensagemDeErro()
        {
            await criarDados();

            var requisicao = new IntencaoOperacaoCriacaoModel()
            {
                IdTipoOperacao = (int)TipoOperacao.Portabilidade,
                IdProduto = (int)Produto.CartaoCreditoConsignado,
                Prestacao = 999,
                ValorAuxilioFinanceiro = 999,
                TaxaMes = 999,
                TaxaAno = 9,
                ValorFinanciado = 99,
                Portabilidade = new IntencaoOperacaoPortabilidadeModel()
            };

            var resultado = await _intencaoOperacaoServico.GravarIntencaoOperacao(requisicao);

            Assert.Null(resultado);
            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.True(_mensagens.PossuiErros);
            Assert.True(_mensagens.BuscarErros().Count > 1);
        }

        [Fact]
        public async Task GravarIntencaoOperacao_OriginadorNaoPermitePortabilidade_RetornaNullComMensagemDeErro()
        {
            await criarDados();

            var requisicao = new IntencaoOperacaoCriacaoModel()
            {
                IdTipoOperacao = (int)TipoOperacao.Portabilidade,
                IdProduto = (int)Produto.CartaoCreditoConsignado,
                Prestacao = 999,
                ValorAuxilioFinanceiro = 999,
                TaxaMes = 999,
                TaxaAno = 9,
                ValorFinanciado = 99,
                Portabilidade = new IntencaoOperacaoPortabilidadeModel
                {
                    IdBancoOriginador = 1,
                    PrazoTotal = 84,
                    PrazoRestante = 60,
                    SaldoDevedor = 5000,
                },
                Contratos = new List<IntencaoOperacaoContratoModel> { new IntencaoOperacaoContratoModel { Contrato = "0000000001" } }
            };

            var resultado = await _intencaoOperacaoServico.GravarIntencaoOperacao(requisicao);

            Assert.Null(resultado);
            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), m => m.Mensagem.Equals(Mensagens.IntencaoOperacao_InstituicaoFinanceiraOriginadoraNaoPermitePortabilidade));
        }

        [Fact]
        public async Task GravarIntencaoOperacao_ComDadosPortabilidadeValidos_DeveCadastrarAsync()
        {
            InstanciarAdapter();
            await criarDados();

            var requisicao = new IntencaoOperacaoCriacaoModel()
            {
                IdTipoOperacao = (int)TipoOperacao.Portabilidade,
                IdProduto = (int)Produto.CartaoCreditoConsignado,
                Prestacao = 999,
                ValorAuxilioFinanceiro = 999,
                TaxaMes = 999,
                TaxaAno = 9,
                ValorFinanciado = 99,
                Portabilidade = new IntencaoOperacaoPortabilidadeModel
                {
                    IdBancoOriginador = 2,
                    PrazoTotal = 84,
                    PrazoRestante = 60,
                    SaldoDevedor = 5000,
                },
                Contratos = new List<IntencaoOperacaoContratoModel> { new IntencaoOperacaoContratoModel { Contrato = "0000000001" } }
            };

            var resultado = await _intencaoOperacaoServico.GravarIntencaoOperacao(requisicao);

            var resultadoBanco = await consultarBancoIntencao();
            var passosProduto = await consultarPassos();
            var portabilidade = await consultarPortabilidade(resultado?.Id ?? 0);

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultadoBanco);
            Assert.Single(resultadoBanco.Historico);
            Assert.NotNull(portabilidade);
            Assert.Equal(passosProduto.First(p => p.PassoInicial).SituacaoIntencaoOperacao.ID, resultadoBanco.Situacao.ID);
            Assert.NotNull(resultado?.Id);
        }

        [Fact]
        public async Task AtualizarIntencaoOperacao_QuandoSituacaoPermite_DeveAtualizarAsync()
        {
            await criarDados();

            var situacao = await criarIntencaoOperacaoTeste();

            var requisicao = new IntencaoOperacaoAtualizacaoModel()
            {
                IdTipoOperacao = (int)situacao.IdTipoOperacao,
                IdProduto = (int)Produto.CartaoCreditoConsignado,
                Prestacao = 1,
                ValorAuxilioFinanceiro = 1,
                TaxaMes = 1,
                TaxaAno = 1,
                ValorFinanciado = 1,
            };

            var resultado = await _intencaoOperacaoServico.AtualizarIntencaoOperacao(requisicao, situacao.ID);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(1, resultadoBanco.ValorFinanciado);
            Assert.True(resultadoBanco.DataAtualizacao > situacao.DataAtualizacao);
            Assert.True(resultado);
        }

        [Fact]
        public async Task AtualizarIntencaoOperacao_RendimentoNaoExistenteParaCliente_RetornaNullComMensagemDeErro()
        {
            await criarDados();

            var situacao = await criarIntencaoOperacaoTeste();

            var requisicao = new IntencaoOperacaoAtualizacaoModel()
            {
                IdTipoOperacao = (int)situacao.IdTipoOperacao,
                IdProduto = (int)Produto.CartaoCreditoConsignado,
                Prestacao = 1,
                ValorAuxilioFinanceiro = 1,
                TaxaMes = 1,
                TaxaAno = 1,
                ValorFinanciado = 1,
                IdRendimentoCliente = 1
            };

            var resultado = await _intencaoOperacaoServico.AtualizarIntencaoOperacao(requisicao, situacao.ID);

            Assert.False(resultado);
            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), m => m.Mensagem.Equals(Mensagens.Rendimento_NaoLocalizadoParaUsuario));
        }

        [Fact]
        public async Task AtualizarIntencaoOperacao_QuandoSituacaoNaoPermite_NaoDeveAtualizar()
        {
            await criarDados();

            var situacao = await criarIntencaoOperacaoTeste(false);

            var requisicao = new IntencaoOperacaoAtualizacaoModel()
            {
                IdTipoOperacao = (int)situacao.IdTipoOperacao,
                Prestacao = 1,
                ValorAuxilioFinanceiro = 1,
                TaxaMes = 1,
                TaxaAno = 1,
                ValorFinanciado = 1,
            };

            var resultado = await _intencaoOperacaoServico.AtualizarIntencaoOperacao(requisicao, situacao.ID);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.True(_mensagens.PossuiErros);
            Assert.Equal(99, resultadoBanco.ValorFinanciado);
            Assert.Equal(resultadoBanco.DataAtualizacao, situacao.DataAtualizacao);
            Assert.False(resultado);
        }

        [Fact]
        public async Task AtualizarIntencaoOperacao_ComDadosPortabilidadeValidos_DeveAtualizarAsync()
        {
            InstanciarAdapter();
            await criarDados();
            await criarIntencaoOperacaoTeste(true, null, TipoOperacao.Portabilidade);

            var requisicao = new IntencaoOperacaoAtualizacaoModel()
            {
                IdTipoOperacao = (int)TipoOperacao.Portabilidade,
                IdProduto = (int)Produto.CartaoCreditoConsignado,
                Prestacao = 999,
                ValorAuxilioFinanceiro = 999,
                TaxaMes = 999,
                TaxaAno = 9,
                ValorFinanciado = 99,
                Portabilidade = new IntencaoOperacaoPortabilidadeModel
                {
                    IdBancoOriginador = 2,
                    PrazoTotal = 72,
                    PrazoRestante = 40,
                    SaldoDevedor = 3000,
                },
                Contratos = new List<IntencaoOperacaoContratoModel> { new IntencaoOperacaoContratoModel { Contrato = "0000000001" } }
            };

            var resultado = await _intencaoOperacaoServico.AtualizarIntencaoOperacao(requisicao, 1);

            var intencaoOperacaoAtualizada = await _intencaoOperacaoServico.BuscarIntencaoOperacaoAutenticado(1);

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
            Assert.NotNull(intencaoOperacaoAtualizada);
            Assert.NotNull(intencaoOperacaoAtualizada.Portabilidade);
            Assert.Equal(intencaoOperacaoAtualizada.Portabilidade.BancoOriginador.Id, requisicao.Portabilidade.IdBancoOriginador);
            Assert.Equal(intencaoOperacaoAtualizada.Portabilidade.PrazoTotal, requisicao.Portabilidade.PrazoTotal);
            Assert.Equal(intencaoOperacaoAtualizada.Portabilidade.PrazoRestante, requisicao.Portabilidade.PrazoRestante);
            Assert.Equal(intencaoOperacaoAtualizada.Portabilidade.SaldoDevedor, requisicao.Portabilidade.SaldoDevedor);
        }

        [Fact]
        public async Task ProsseguirIntencaoOperacao_ParaPassoPrimario_DeveAtualizar()
        {
            var (passoPrimario, _, _) = await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var requisicao = new IntencaoOperacaoProsseguirSituacaoModel();

            var resultado = await _intencaoOperacaoServico.ProsseguirIntencaoOperacao(situacao.ID, requisicao);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultadoBanco.DataAtualizacao > situacao.DataAtualizacao);
            Assert.True(resultado);
            Assert.Equal(2, resultadoBanco.Historico.Count());
            Assert.Equal(passoPrimario, resultadoBanco.Situacao.ID);
        }

        [Fact]
        public async Task ProsseguirIntencaoOperacao_ComDadosAdicionais_DeveAtualizar()
        {
            var (passoPrimario, _, _) = await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var requisicao = new IntencaoOperacaoProsseguirSituacaoModel() { PendenciaUsuario = true, DescricaoEspecifica = "test" };

            var resultado = await _intencaoOperacaoServico.ProsseguirIntencaoOperacao(situacao.ID, requisicao);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultadoBanco.DataAtualizacao > situacao.DataAtualizacao);
            Assert.True(resultado);
            Assert.Equal(2, resultadoBanco.Historico.Count());
            Assert.Equal(passoPrimario, resultadoBanco.Situacao.ID);
            Assert.True(resultadoBanco.Historico.Last().PendenciaUsuario);
            Assert.Equal("test", resultadoBanco.Historico.Last().Descricao);
        }

        [Fact]
        public async Task ProsseguirIntencaoOperacao_ParaPassoExcecao_DeveAtualizar()
        {
            var (_, passoExcecao, _) = await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var requisicao = new IntencaoOperacaoProsseguirSituacaoModel() { ProsseguirParaPassoExcecao = true };

            var resultado = await _intencaoOperacaoServico.ProsseguirIntencaoOperacao(situacao.ID, requisicao);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultadoBanco.DataAtualizacao > situacao.DataAtualizacao);
            Assert.True(resultado);
            Assert.Equal(2, resultadoBanco.Historico.Count());
            Assert.Equal(passoExcecao, resultadoBanco.Situacao.ID);
        }

        [Fact]
        public async Task ProsseguirIntencaoOperacao_ParaPassoExcepcional_DeveAtualizar()
        {
            var (_, _, passoExcepcional) = await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var requisicao = new IntencaoOperacaoProsseguirSituacaoModel() { IdSituacaoExcepcional = passoExcepcional };

            var resultado = await _intencaoOperacaoServico.ProsseguirIntencaoOperacao(situacao.ID, requisicao);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultadoBanco.DataAtualizacao > situacao.DataAtualizacao);
            Assert.True(resultado);
            Assert.Equal(2, resultadoBanco.Historico.Count());
            Assert.Equal(passoExcepcional, resultadoBanco.Situacao.ID);
        }

        [Fact]
        public async Task ProsseguirIntencaoOperacao_ParaPassoInvalido_NaoDeveAtualizar()
        {
            var (passoPrimario, _, _) = await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var requisicao = new IntencaoOperacaoProsseguirSituacaoModel() { IdSituacaoExcepcional = passoPrimario };

            var resultado = await _intencaoOperacaoServico.ProsseguirIntencaoOperacao(situacao.ID, requisicao);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.True(_mensagens.PossuiErros);
            Assert.Equal(resultadoBanco.DataAtualizacao, situacao.DataAtualizacao);
            Assert.False(resultado);
            Assert.Single(resultadoBanco.Historico);
        }

        [Fact]
        public async Task ConsultarIntencaoOperacao_QuandoPossuiPassosPendentes_DeveRetornarTodosOsPassosDoProduto()
        {
            InstanciarAdapter();
            var (passoPrimario, _, _) = await criarDados();
            var situacaoComUmPassoRealizado = await criarIntencaoOperacaoTeste();

            var resultado = await _intencaoOperacaoServico.BuscarIntencaoOperacaoAutenticado(situacaoComUmPassoRealizado.ID);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.Equal(4, resultado.PassosProduto.Count());
            Assert.Single(resultado.PassosProduto.Where(p => p.Completo));
        }

        [Fact]
        public async Task ConsultarIntencaoOperacao_QuandoPassosCompleto_DeveRetornarTodosOsPassosDoProduto()
        {
            InstanciarAdapter();
            await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var opcoesSituacoes = await _contexto.IntencaoOperacaoSituacoes.ToDictionaryAsync(s => s.Nome, s => s.ID);
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Analise"]));
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Assinatura"]));
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Aprovada"], "test"));
            await _contexto.SaveChangesAsync();

            var resultado = await _intencaoOperacaoServico.BuscarIntencaoOperacaoAutenticado(situacao.ID);

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.Equal(4, resultado.PassosProduto.Count());
            Assert.Equal(4, resultado.PassosProduto.Count(p => p.Completo));
            Assert.Equal(Mensagens.PassosProduto_StatusAprovada, resultado.PassosProduto.Last().Descricao);
        }

        [Fact]
        public async Task ConsultarIntencaoOperacao_QuandoPossuiPassoExcecao_DeveRetornarApenasPassosPrincipais()
        {
            InstanciarAdapter();
            await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var opcoesSituacoes = await _contexto.IntencaoOperacaoSituacoes.ToDictionaryAsync(s => s.Nome, s => s.ID);
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Analise"]));
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Bencao"]));
            await _contexto.SaveChangesAsync();

            var resultado = await _intencaoOperacaoServico.BuscarIntencaoOperacaoAutenticado(situacao.ID);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.Equal(4, resultado.PassosProduto.Count());
            Assert.Equal(2, resultado.PassosProduto.Count(p => p.Completo));
        }

        [Fact]
        public async Task ConsultarIntencaoOperacao_QuandoPossuiPassoExcepcional_DeveRetornarApenasPassosPrincipaisRealizados()
        {
            InstanciarAdapter();
            await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var opcoesSituacoes = await _contexto.IntencaoOperacaoSituacoes.ToDictionaryAsync(s => s.Nome, s => s.ID);
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Analise"]));
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Reprovada"]));
            await _contexto.SaveChangesAsync();

            var resultado = await _intencaoOperacaoServico.BuscarIntencaoOperacaoAutenticado(situacao.ID);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.PassosProduto.Count());
            Assert.Equal(3, resultado.PassosProduto.Count(p => p.Completo));
        }

        [Fact]
        public async Task ConsultarIntencaoOperacao_QuandoCompletoSemTodosOsPassos_DeveRetornarApenasPassosPrincipaisRealizados()
        {
            InstanciarAdapter();
            await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var opcoesSituacoes = await _contexto.IntencaoOperacaoSituacoes.ToDictionaryAsync(s => s.Nome, s => s.ID);
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Analise"]));
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Aprovada"]));
            await _contexto.SaveChangesAsync();

            var resultado = await _intencaoOperacaoServico.BuscarIntencaoOperacaoAutenticado(situacao.ID);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.PassosProduto.Count());
            Assert.Equal(3, resultado.PassosProduto.Count(p => p.Completo));
        }

        [Fact]
        public async Task ConsultarIntencaoOperacao_QuandoRetornarPassos_DeveRetornarPassosRelativosAoRetorno()
        {
            InstanciarAdapter();
            await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var opcoesSituacoes = await _contexto.IntencaoOperacaoSituacoes.ToDictionaryAsync(s => s.Nome, s => s.ID);
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Analise"]));
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Solicitacao"]));
            await _contexto.SaveChangesAsync();

            var resultado = await _intencaoOperacaoServico.BuscarIntencaoOperacaoAutenticado(situacao.ID);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.Equal(4, resultado.PassosProduto.Count());
            Assert.Equal(1, resultado.PassosProduto.Count(p => p.Completo));
        }

        [Fact]
        public async Task ConsultarTodasIntencoesOperacao_QuandoFiltrar_DeveRetornarFiltrado()
        {
            InstanciarAdapter();
            await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();
            var situacao2 = await criarIntencaoOperacaoTeste(tipoOperacao: TipoOperacao.Refinanciamento);

            var filtros = new IntencaoOperacaoConsultaModel()
            {
                IdTipoOperacao = (int)TipoOperacao.Refinanciamento,
            };

            var resultado = await _intencaoOperacaoServico.ListarIntencoesOperacao(filtros);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Count());
        }

        [Fact]
        public async Task ConsultarTodasIntencoesOperacao_QuandoSolicitarOpcoesProximosPassos_DeveRetornarOpcoesProximosPassos()
        {
            InstanciarAdapter();
            await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var filtros = new IntencaoOperacaoConsultaModel()
            {
                ListarOpcoesProximoPasso = true,
            };

            var resultado = await _intencaoOperacaoServico.ListarIntencoesOperacao(filtros);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.FirstOrDefault().OpcoesProximoPasso.Count());
        }

        [Fact]
        public async Task AtendimentoIntencaoOperacao_QuandoCorreto_DeveAdicionarEAtualizar()
        {
            InstanciarAdapter();
            await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var requisicao = new IntencaoOperacaoAtendimentoModel
            {
                IdLoja = 1,
                Proposta = "12456",
                Observacao = "Teste",
            };

            var resultado = await _intencaoOperacaoServico.AtenderIntencaoOperacao(requisicao, situacao.ID);

            var resultadoBanco = await consultarBancoIntencao();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
            Assert.Equal(1, resultadoBanco.Observacoes.Count());
            Assert.Equal(requisicao.Observacao, resultadoBanco.Observacoes.First().Observacao);
            Assert.Equal(requisicao.IdLoja, resultadoBanco.IdLoja);
            Assert.Equal(requisicao.Proposta, resultadoBanco.Proposta);
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("12345123456")]
        public async Task ConsultarIntencaoOperacao_QuandoCpfInvalido_DeveRetornarErro(string cpf)
        {
            var resultado = await _intencaoOperacaoServico.ListarIntencoesOperacao(new CPF(cpf));

            Assert.True(_mensagens.PossuiErros);
            Assert.Null(resultado);
        }

        [Fact]
        public async Task ConsultarIntencaoOperacao_QuandoCpfValido_DeveRetornarSomenteDadosParaEsteCpf()
        {
            InstanciarAdapter();
            await criarDados();

            var usuarioOutroCpf = await criarUsuarioDiferenteUsuarioTeste();

            await criarIntencaoOperacaoTeste();

            await criarIntencaoOperacaoTeste(false, usuarioOutroCpf.ID);

            var resultado = await _intencaoOperacaoServico.ListarIntencoesOperacao(new CPF(CPF_USUARIO_TESTE));

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.DoesNotContain(resultado, r => r.Usuario.CPF.Equals(usuarioOutroCpf.CPF));
        }

        [Fact]
        public async Task ConsultarIntencaoOperacao_QuandoStatusSolicitcao_DeveRetornarMensagemStatusSolicitacao()
        {
            InstanciarAdapter();
            await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var opcoesSituacoes = await _contexto.IntencaoOperacaoSituacoes.ToDictionaryAsync(s => s.Nome, s => s.ID);
            await _contexto.SaveChangesAsync();

            var resultado = await _intencaoOperacaoServico.BuscarIntencaoOperacaoAutenticado(situacao.ID);

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal("Solicitacao", resultado.PassosProduto.Last(n => n.Completo).Titulo);
            Assert.Equal(Mensagens.PassosProduto_StatusSolicitacao, resultado.PassosProduto.Last(n => n.Completo).Descricao);
        }

        [Fact]
        public async Task ConsultarIntencaoOperacao_QuandoStatusAnalise_DeveRetornarMensagemStatusAnalise()
        {
            InstanciarAdapter();
            await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var opcoesSituacoes = await _contexto.IntencaoOperacaoSituacoes.ToDictionaryAsync(s => s.Nome, s => s.ID);
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Analise"]));
            await _contexto.SaveChangesAsync();

            var resultado = await _intencaoOperacaoServico.BuscarIntencaoOperacaoAutenticado(situacao.ID);

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal("Analise", resultado.PassosProduto.Last(n => n.Completo).Titulo);
            Assert.Equal(Mensagens.PassosProduto_StatusAnalise, resultado.PassosProduto.Last(n => n.Completo).Descricao);
        }

        [Fact]
        public async Task ConsultarIntencaoOperacao_QuandoStatusAnalise_DeveRetornarMensagemStatusAssinatura()
        {
            InstanciarAdapter();
            await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var opcoesSituacoes = await _contexto.IntencaoOperacaoSituacoes.ToDictionaryAsync(s => s.Nome, s => s.ID);
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Analise"]));
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Assinatura"]));
            await _contexto.SaveChangesAsync();

            var resultado = await _intencaoOperacaoServico.BuscarIntencaoOperacaoAutenticado(situacao.ID);

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal("Assinatura", resultado.PassosProduto.Last(n => n.Completo).Titulo);
            Assert.Equal(Mensagens.PassosProduto_StatusAssinaturaDigital, resultado.PassosProduto.Last(n => n.Completo).Descricao);
        }

        [Fact]
        public async Task ConsultarIntencaoOperacao_QuandoStatusAnalise_DeveRetornarMensagemStatusAprovada()
        {
            InstanciarAdapter();
            await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var opcoesSituacoes = await _contexto.IntencaoOperacaoSituacoes.ToDictionaryAsync(s => s.Nome, s => s.ID);
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Aprovada"]));
            await _contexto.SaveChangesAsync();

            var resultado = await _intencaoOperacaoServico.BuscarIntencaoOperacaoAutenticado(situacao.ID);

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal("Aprovada", resultado.PassosProduto.Last(n => n.Completo).Titulo);
            Assert.Equal(Mensagens.PassosProduto_StatusAprovada, resultado.PassosProduto.Last(n => n.Completo).Descricao);
        }

        [Fact]
        public async Task ConsultarIntencaoOperacao_QuandoStatusAnalise_DeveRetornarMensagemStatusReprovada()
        {
            InstanciarAdapter();
            await criarDados();
            var situacao = await criarIntencaoOperacaoTeste();

            var opcoesSituacoes = await _contexto.IntencaoOperacaoSituacoes.ToDictionaryAsync(s => s.Nome, s => s.ID);
            await _contexto.AddAsync(new IntencaoOperacaoHistoricoDominio(situacao.ID, opcoesSituacoes["Reprovada"]));
            await _contexto.SaveChangesAsync();

            var resultado = await _intencaoOperacaoServico.BuscarIntencaoOperacaoAutenticado(situacao.ID);

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(Mensagens.PassosProduto_StatusReprovada, resultado.PassosProduto.Last(n => n.Completo).Descricao);
        }

        private async Task<IntencaoOperacaoDominio> criarIntencaoOperacaoTeste(bool podeAtualizar = true, int? idUsuario = null, TipoOperacao tipoOperacao = TipoOperacao.Novo)
        {
            var intencaoTeste = new IntencaoOperacaoDominio(
                tipoOperacao,
                _contexto.Produtos.First().ID,
                null,
                null,
                idUsuario ?? _usuarioTeste.ID,
                999,
                999,
                9,
                99,
                99,
                9,
                new System.DateTime(),
                null,
                1,
                999,
                999,
                null
            );

            if (tipoOperacao == TipoOperacao.Portabilidade)
                intencaoTeste.SetPortabilidade(new IntencaoOperacaoPortabilidadeDominio(2, 84, 60, 5000, null, null, null));

            await _contexto.AddAsync(intencaoTeste);
            await _contexto.SaveChangesAsync();

            var intencaoSituacao = new IntencaoOperacaoHistoricoDominio(
                intencaoTeste.ID,
                (await _contexto.IntencaoOperacaoSituacoes.FirstAsync(s => s.PermiteAtualizacoes)).ID
            );

            await _contexto.AddAsync(intencaoSituacao);
            await _contexto.SaveChangesAsync();

            if (!podeAtualizar)
            {
                var intencaoSituacaoNova = new IntencaoOperacaoHistoricoDominio(
                    intencaoTeste.ID,
                    (await _contexto.IntencaoOperacaoSituacoes.FirstAsync(s => !s.PermiteAtualizacoes)).ID
                );

                await _contexto.AddAsync(intencaoSituacaoNova);
                await _contexto.SaveChangesAsync();
            }

            return await consultarBancoIntencao();
        }

        private async Task<IntencaoOperacaoDominio> consultarBancoIntencao() =>
            await _contexto.IntencoesOperacao
                .Include(i => i.Observacoes)
                .Include(i => i.Historico)
                    .ThenInclude(i => i.SituacaoIntencaoOperacao)
                .AsNoTracking()
                .FirstAsync();

        private async Task<List<IntencaoOperacaoSituacaoPassoDominio>> consultarPassos() =>
            await _contexto.IntencaoOperacaoSituacaoPassos
                .Include(p => p.ProximoPasso.SituacaoIntencaoOperacao)
                .Include(p => p.ProximoPassoExcecao.SituacaoIntencaoOperacao)
                .ToListAsync();

        private async Task<IntencaoOperacaoPortabilidadeDominio> consultarPortabilidade(int idIntencaoOperacao) =>
            await _contexto.IntencoesOperacaoPortabilidade
                .AsNoTracking()
                .FirstOrDefaultAsync(intencao => intencao.IdIntencaoOperacao.Equals(idIntencaoOperacao));

        private async Task<(int, int, int)> criarDados()
        {
            var tiposOperacao = EnumExtensions.ValoresEnum<Dominio.Enum.TipoOperacao>();
            if (_contexto.TiposOperacao.Count() < tiposOperacao.Count())
            {
                var novosTiposOperacao = tiposOperacao
                    .Select(e => new TipoOperacaoDominio(e, e.ToString(), e.GetDescription()))
                    .Where(e => !_contexto.TiposOperacao.Any(t => t.ID == e.ID));

                await _contexto.TiposOperacao.AddRangeAsync(novosTiposOperacao);
            }

            var produto = new ProdutoDominio(Produto.CartaoCreditoConsignado, "Cartão Crédito Consignado", "CCC", false);
            await _contexto.AddAsync(produto);

            var opcoesSituacaoIntencaoOperacao = new IntencaoOperacaoSituacaoDominio[] {
                new IntencaoOperacaoSituacaoDominio("Solicitacao", "default", true, true),
                new IntencaoOperacaoSituacaoDominio("Analise", "default", false, true),
                new IntencaoOperacaoSituacaoDominio("Assinatura", "default", false, true),
                new IntencaoOperacaoSituacaoDominio("Aprovada", "default", false, false),
                new IntencaoOperacaoSituacaoDominio("Reprovada", "default", false, false, true),
                new IntencaoOperacaoSituacaoDominio("Bencao", "default", false, false),
            };
            await _contexto.IntencaoOperacaoSituacoes.AddRangeAsync(opcoesSituacaoIntencaoOperacao);

            var bancos = new List<BancoDominio>
            {
                new BancoDominio("01", "00000000001", "Banco Teste 1", false),
                new BancoDominio("01", "00000000002", "Banco Teste 2", true)
            };
            await _contexto.AddRangeAsync(bancos);

            await _contexto.SaveChangesAsync();

            Dictionary<string, int> situacaoOperacao = await _contexto.IntencaoOperacaoSituacoes.ToDictionaryAsync(situacao => situacao.Nome, situacao => situacao.ID);

            var passoAprovado = await _contexto.IntencaoOperacaoSituacaoPassos.AddAsync(
            new IntencaoOperacaoSituacaoPassoDominio(produto.ID, TipoOperacao.Novo, situacaoOperacao["Aprovada"]));
            await _contexto.SaveChangesAsync();

            var passoAssinatura = await _contexto.IntencaoOperacaoSituacaoPassos.AddAsync(
                new IntencaoOperacaoSituacaoPassoDominio(produto.ID, TipoOperacao.Novo, situacaoOperacao["Assinatura"], passoAprovado.Entity.ID));
            await _contexto.SaveChangesAsync();

            var passoAnalise = await _contexto.IntencaoOperacaoSituacaoPassos.AddAsync(
                new IntencaoOperacaoSituacaoPassoDominio(produto.ID, TipoOperacao.Novo, situacaoOperacao["Analise"], passoAssinatura.Entity.ID));
            await _contexto.SaveChangesAsync();

            var passoExcecaoBencao = await _contexto.IntencaoOperacaoSituacaoPassos.AddAsync(
                new IntencaoOperacaoSituacaoPassoDominio(produto.ID, TipoOperacao.Novo, situacaoOperacao["Bencao"], passoAnalise.Entity.ID, passoAssinatura.Entity.ID));
            await _contexto.SaveChangesAsync();

            await _contexto.IntencaoOperacaoSituacaoPassos.AddAsync(
                new IntencaoOperacaoSituacaoPassoDominio(produto.ID, TipoOperacao.Novo, situacaoOperacao["Solicitacao"], passoAnalise.Entity.ID, passoExcecaoBencao.Entity.ID, passoInicial: true));
            await _contexto.SaveChangesAsync();

            await _contexto.IntencaoOperacaoSituacaoPassos.AddAsync(
                new IntencaoOperacaoSituacaoPassoDominio(produto.ID, TipoOperacao.Portabilidade, situacaoOperacao["Solicitacao"], null, null, passoInicial: true));
            await _contexto.SaveChangesAsync();

            return (passoAnalise.Entity.IdIntencaoOperacaoSituacao, passoExcecaoBencao.Entity.IdIntencaoOperacaoSituacao, opcoesSituacaoIntencaoOperacao.First(s => s.SituacaoExtraordinaria).ID);
        }

        private async Task<UsuarioDominio> criarUsuarioDiferenteUsuarioTeste()
        {
            var usuario = new UsuarioDominio("Arnaldo", null, true, new CPF("92731365080"), "123", null);
            await _contexto.AddAsync(usuario);
            await _contexto.SaveChangesAsync();

            return usuario;
        }
    }
}
