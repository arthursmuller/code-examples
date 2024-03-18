using Aplicacao.Interfaces;
using Aplicacao.Model.SeguroBeneficiario;
using Aplicacao.Model.SeguroCliente;
using Aplicacao.Model.SeguroEndereco;
using Aplicacao.Model.SeguroProposta;
using Aplicacao.Model.SeguroTelefoneCliente;
using Aplicacao.Servico;
using B.Configuracao;
using Dominio;
using Dominio.Enum;
using Infraestrutura.Providers.IcatuApi;
using Infraestrutura.Providers.IcatuApi.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class SeguroPropostaServicoTeste : ServicoTesteBase
    {
        private readonly SeguroPropostaServico _seguroPropostaServico;
        private UsuarioDominio _usuarioTeste;
        private ILogger<ISeguroPropostaServico> _loggerMock = new Mock<ILogger<ISeguroPropostaServico>>().Object;

        private readonly string jsonNumeroProposta = @" {
            ""numeracaoProposta"": {
                ""proposta"": ""820052656831"",
                ""linhaDigitavel"": ""0"",
                ""codigoBarras"": ""0"",
                ""formaPagamento"": 93,
                ""nossoNumero"": ""820052656831001"",
                ""certificadoApolice"": ""820052656831"",
                ""grupoApolice"": 57162,
                ""subestipulante"": 0,
                ""apolice"": ""82007701"",
                ""modulo"": 1,
                ""produto"": 261,
                ""parceiro"": 428,
                ""nomeParceiro"": ""Bem Promotora de Vendas e Serviços"",
                ""dataVencimento"": ""2022-02-17T18:11:07.66""
            }
        }";
        private readonly string jsonCriarPedidoPagamento = @" {
            ""identificador"": ""or_n3a78exfPfW9x6l1"",
            ""codigo"": ""756QS7S351"",
            ""valor"": 599,
            ""fechado"": false,
            ""status"": ""Pendente"",
            ""dataCriacao"": ""2022 - 03 - 04T19: 23:14Z"",
            ""dataAlteracao"": ""2022 - 03 - 04T19: 23:15Z"",
            ""checkouts"": [
                    {
                    ""identificador"": ""chk_WDY9EVWiLi5z7rA2"",
                    ""valor"": 599,
                    ""status"": ""Aberto"",
                    ""url"": ""https://api.pagar.me/checkout/v1/orders/chk_WDY9EVWiLi5z7rA2"",
                    ""dataCriacao"": ""2022 - 03 - 04T19: 23:15Z"",
                    ""dataAlteracao"": ""2022 - 03 - 04T19: 23:15Z"",
                    ""dataExpiracao"": ""2022 - 03 - 04T21: 23:15Z"",
                    ""metadado"": {
                        ""empresa"": ""Icatu"",
                        ""codigoProposta"": ""99989898998"",
                        ""codigoCobranca"": ""99989898998001"",
                        ""origem"": ""BEM PROMOTORA"",
                        ""destino"": ""VIDA""
                    }
                }
            ],
            ""metadado"": {
            ""empresa"": ""Icatu"",
                ""codigoProposta"": ""99989898998"",
                ""codigoCobranca"": ""99989898998001"",
                ""origem"": ""BEM PROMOTORA"",
                ""destino"": ""VIDA""
            }
        }";
        private readonly string jsonConsultarPedidoPagamento = @"{
            ""identificador"": ""or_n3a78exfPfW9x6l1"",
            ""codigo"": ""756QS7S351"",
            ""valor"": 599,
            ""fechado"": false,
            ""status"": ""Cancelado"",
            ""dataCriacao"": ""2022 - 03 - 04T19: 23:14Z"",
            ""dataAlteracao"": ""2022 - 03 - 04T19: 23:14Z"",
            ""cobrancas"": [
                {
                    ""identificador"": ""ch_XY5M6O5mc2f9K7PD"",
                    ""codigo"": ""756QS7S351"",
                    ""valor"": 599,
                    ""status"": ""Falhou"",
                    ""metodoPagamento"": ""CartaoCredito"",
                    ""dataCriacao"": ""2022 - 03 - 04T19: 23:14Z"",
                    ""dataAlteracao"": ""2022 - 03 - 04T19: 23:14Z"",
                    ""transacao"": {
                        ""chaveOperacao"": ""4256191497788"",
                        ""identificador"": ""tran_n81X8ynIpofnNZqW"",
                        ""tipoTransacao"": ""CartaoCredito"",
                        ""valor"": 599,
                        ""status"": ""not_authorized"",
                        ""sucesso"": false,
                        ""parcelas"": 1,
                        ""descricaoFatura"": ""Icatu Seguros"",
                        ""nsuAdquirente"": ""71289"",
                        ""cartaoCredito"": {
                            ""identificador"": ""card_RBeERyJuWgFk3lkp"",
                            ""primeirosSeisDigitos"": ""435087"",
                            ""ultimosQuatroDigitos"": ""0197"",
                            ""bandeira"": ""visa"",
                            ""nomeCliente"": ""erica helena ongaro"",
                            ""documentoTitular"": ""02981603078"",
                            ""mesExpiracao"": 5,
                            ""anoExpiracao"": 2026,
                            ""status"": ""Ativo"",
                            ""tipo"": ""Credito"",
                            ""dataCriacao"": ""2022 - 03 - 04T19: 23:14Z"",
                            ""dataAlteracao"": ""2022 - 03 - 04T19: 23:14Z"",
                            ""enderecoCobranca"": {
                                ""logradouro"": ""teste"",
                                ""complemento"": ""teste"",
                                ""cep"": ""93336221"",
                                ""cidade"": ""ARRAIAS"",
                                ""uf"": ""TO"",
                                ""pais"": ""BR"",
                                ""logradouro"": ""teste"",
                                ""complemento"": ""teste""
                            }
                        },
                        ""dataCriacao"": ""2022 - 03 - 04T19: 23:14Z"",
                        ""dataAlteracao"": ""2022 - 03 - 04T19: 23:14Z"",
                        ""respostaGateway"": {
                            ""codigo"": ""200"",
                            ""erros"": []
                        },
                        ""metadado"": {
                            ""empresa"": ""Icatu"",
                            ""codigoProposta"": ""99989898998"",
                            ""codigoCobranca"": ""99989898998001"",
                            ""origem"": ""BEM PROMOTORA"",
                            ""destino"": ""VIDA"",
                            ""idUsuario"": ""5f2090db84c3250e9cb6f4ae"",
                            ""nomeUsuario"": ""EZEQUIEL MUSSATTO CITOLIN""
                        }
                    }
                },
                {
                    ""identificador"": ""ch_KNWOwMDhBRfBa6Rb"",
                    ""codigo"": ""756QS7S351"",
                    ""valor"": 599,
                    ""status"": ""Falhou"",
                    ""metodoPagamento"": ""CartaoCredito"",
                    ""dataCriacao"": ""2022 - 03 - 04T19: 23:14Z"",
                    ""dataAlteracao"": ""2022 - 03 - 04T19: 23:14Z"",
                    ""transacao"": {
                        ""chaveOperacao"": ""8350333542727"",
                        ""identificador"": ""tran_pjA2rxOHbLijode3"",
                        ""tipoTransacao"": ""CartaoCredito"",
                        ""valor"": 599,
                        ""status"": ""not_authorized"",
                        ""sucesso"": false,
                        ""parcelas"": 1,
                        ""descricaoFatura"": ""Icatu Seguros"",
                        ""nsuAdquirente"": ""89097"",
                        ""cartaoCredito"": {
                            ""identificador"": ""card_RBeERyJuWgFk3lkp"",
                            ""primeirosSeisDigitos"": ""435087"",
                            ""ultimosQuatroDigitos"": ""0197"",
                            ""bandeira"": ""visa"",
                            ""nomeCliente"": ""erica helena ongaro"",
                            ""documentoTitular"": ""02981603078"",
                            ""mesExpiracao"": 5,
                            ""anoExpiracao"": 2026,
                            ""status"": ""Ativo"",
                            ""tipo"": ""Credito"",
                            ""dataCriacao"": ""2022 - 03 - 04T19: 23:14Z"",
                            ""dataAlteracao"": ""2022 - 03 - 04T19: 23:14Z"",
                            ""enderecoCobranca"": {
                                ""logradouro"": ""teste"",
                                ""complemento"": ""teste"",
                                ""cep"": ""93336221"",
                                ""cidade"": ""ARRAIAS"",
                                ""uf"": ""TO"",
                                ""pais"": ""BR"",
                                ""logradouro"": ""teste"",
                                ""complemento"": ""teste""
                            }
                        },
                        ""dataCriacao"": ""2022 - 03 - 04T19: 23:14Z"",
                        ""dataAlteracao"": ""2022 - 03 - 04T19: 23:14Z"",
                        ""respostaGateway"": {
                            ""codigo"": ""200"",
                            ""erros"": []
                        },
                        ""metadado"": {
                            ""empresa"": ""Icatu"",
                            ""codigoProposta"": ""99989898998"",
                            ""codigoCobranca"": ""99989898998001"",
                            ""origem"": ""BEM PROMOTORA"",
                            ""destino"": ""VIDA"",
                            ""idUsuario"": ""5f2090db84c3250e9cb6f4ae"",
                            ""nomeUsuario"": ""EZEQUIEL MUSSATTO CITOLIN""
                        }
                    }
                }
            ],
            ""checkouts"": [
                {
                    ""identificador"": ""chk_WDY9EVWiLi5z7rA2"",
                    ""valor"": 599,
                    ""status"": ""Cancelado"",
                    ""url"": ""https://api.pagar.me/checkout/v1/orders/chk_WDY9EVWiLi5z7rA2"",
                    ""dataCriacao"": ""2022 - 03 - 04T19: 23:14Z"",
                    ""dataAlteracao"": ""2022 - 03 - 04T19: 23:14Z"",
                    ""dataCancelamento"": ""2022 - 03 - 04T19: 23:14Z"",
                    ""dataExpiracao"": ""2022 - 03 - 04T19: 23:14Z"",
                    ""metadado"": {
                        ""empresa"": ""Icatu"",
                        ""codigoProposta"": ""99989898998"",
                        ""codigoCobranca"": ""99989898998001"",
                        ""origem"": ""BEM PROMOTORA"",
                        ""destino"": ""VIDA""
                    }
                }
            ],
            ""metadado"": {
                ""empresa"": ""Icatu"",
                ""codigoProposta"": ""99989898998"",
                ""codigoCobranca"": ""99989898998001"",
                ""origem"": ""BEM PROMOTORA"",
                ""destino"": ""VIDA""
            }
        } ";
        private readonly string jsonCriarProposta = @" {
            ""id"": ""820051305350"",
            ""status"": ""201"",
            ""mensagem"": ""Proposta recebida com sucesso e em análise para aprovação.""
        }";

        public SeguroPropostaServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();
            criarDadosRelacionamentos();

            var provider = new Mock<IProviderIcatu>();
            var documentoServico = new Mock<IDocumentoServico>();
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("ambiente-frontend-url", "teste")
            }, null, null);

            provider
                .Setup(x => x.CriarNumeroProposta(It.IsAny<CriarNumeroPropostaDto>()))
                .ReturnsAsync(JsonConvert.DeserializeObject<CriarNumeroPropostaRespostaDto>(jsonNumeroProposta));

            provider
              .Setup(x => x.CriarPedidoPagamento(It.IsAny<CriarPedidoPagamentoDto>()))
              .ReturnsAsync(JsonConvert.DeserializeObject<PedidoPagamentoRespostaDto>(jsonCriarPedidoPagamento));

            provider
                .Setup(x => x.CriarProposta(It.IsAny<CriarPropostaDto>()))
                .ReturnsAsync(JsonConvert.DeserializeObject<CriarPropostaRespostaDto>(jsonCriarProposta));

            provider
                .Setup(x => x.ConsultarPedidoPagamento(It.IsAny<string>()))
                .ReturnsAsync(JsonConvert.DeserializeObject<ConsultarPedidoPagamentoDto>(jsonConsultarPedidoPagamento));

            _seguroPropostaServico = new SeguroPropostaServico(_contexto, _usuarioLogin, _mensagens, configuracao, _loggerMock, provider.Object, documentoServico.Object);
        }

        [Fact]
        public async Task Listar_MeioPagamento_Deve_Retornar_MeioPagamento()
        {
            await criarEntidades();

            var resultado = await _seguroPropostaServico.ListarMeioPagamentos();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado.Count() > 0);
        }

        [Fact]
        public async Task Listar_Proposta_Deve_Retornar_Proposta()
        {
            await criarEntidades();

            var resultado = await _seguroPropostaServico.Listar();

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(resultado.IdCliente, _usuarioTeste.Cliente.ID);
        }

        [Fact]
        public async Task Criar_Proposta_Deve_Retornar_True()
        {
            var resultado = await criarProposta();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
        }

        [Fact]
        public async Task Baixar_Termo_Deve_Retornar_Link()
        {
            var resultado = await criarProposta();

            var documento = await _seguroPropostaServico.BaixarTermo();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
        }

        [Fact]
        public async Task Criar_Proposta_Sem_Beneficiario_Deve_Retortnar_True()
        {
            var resultado = await criarProposta(usarBeneficiarios: false);

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
        }

        [Fact]
        public async Task Criar_Proposta_Deve_Criar_Endereco_Principal_Com_Id_Na_Model()
        {
            AddRangeAndSave(new[] {
                new EnderecoClienteDominio(1, "teste", 1, "teste", 1, "asddsa", 0, "asda", "93331231", false),
            });
            _usuarioTeste.Cliente.SetEnderecoPrincipal(1);
            await SaveChangesAsync();

            var resultado = await criarProposta(1);

            var seguroCliente = await _contexto
                .SeguroClienteIcatu
                .Include(c => c.EnderecoPrincipal)
                .Include(c => c.EnderecoCobranca)
                .FirstOrDefaultAsync(e => e.IdCliente.Equals(_usuarioTeste.Cliente.ID));

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.NotNull(seguroCliente.EnderecoPrincipal);
        }

        [Fact]
        public async Task Criar_Proposta_Deve_Criar_Endereco_Cobranca_Com_Id_Na_Model()
        {
            AddRangeAndSave(new[] {
                new EnderecoClienteDominio(1, "teste", 1, "teste", 1, "asddsa", 0, "asda", "93331231", false),
            });
            _usuarioTeste.Cliente.SetEnderecoSecundario(1);
            await SaveChangesAsync();

            var resultado = await criarProposta(1);

            var seguroCliente = await _contexto
                .SeguroClienteIcatu
                .Include(c => c.EnderecoPrincipal)
                .Include(c => c.EnderecoCobranca)
                .FirstOrDefaultAsync(e => e.IdCliente.Equals(_usuarioTeste.Cliente.ID));

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.NotNull(seguroCliente.EnderecoPrincipal);
        }

        [Fact]
        public async Task Buscar_Link_Pagamento_Deve_Retornar_Link()
        {
            await criarProposta();

            var resultado = await _seguroPropostaServico.ConsultarLinkPagamento();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
        }

        [Fact]
        public async Task Buscar_Link_Pagamento_Deve_Retornar_Null_Quando_Sem_Proposta()
        {
            var resultado = await _seguroPropostaServico.ConsultarLinkPagamento();

            Assert.True(_mensagens.PossuiErros);
            Assert.Null(resultado);
        }

        [Fact]
        public async Task Enviar_Proposta_Icatu_Deve_Retornar_True_E_Criar_Com_EnderecoPrincipal()
        {
            AddRangeAndSave(new[] {
                new EnderecoClienteDominio(1, "teste", 1, "teste", 1, "asddsa", 0, "asda", "93331231", false),
            });
            _usuarioTeste.Cliente.SetEnderecoPrincipal(1);
            await SaveChangesAsync();

            await criarProposta(1);

            var resultado = await _seguroPropostaServico.EnviarPropostaIcatu();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
        }

        [Fact]
        public async Task Enviar_Proposta_Icatu_Deve_Retornar_True_E_Criar_Com_Endereco_Secundario()
        {
            await criarProposta();

            var resultado = await _seguroPropostaServico.EnviarPropostaIcatu();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
        }

        private async Task<string> criarProposta(int? idEnderecoCliente = null, bool usarBeneficiarios = true)
        {
            await criarEntidades();

           var propostaModel = preencherPropostaModel(idEnderecoCliente);

            var resultado = await _seguroPropostaServico.Criar(propostaModel);

            return resultado;
        }
        private CriarSeguroPropostaModel preencherPropostaModel(int? idEnderecoCliente = null, bool usarBeneficiarios = true)
        {
            var model = new CriarSeguroPropostaModel();
            model.IdSeguroProduto = 1;
            model.PPE = 'B';
            model.RendaMensal = 123;
            model.RelacionamentoEletronico = true;
            model.Aposentado = true;

            if(!(idEnderecoCliente is null))
                model.IdEnderecoCliente = idEnderecoCliente;

            var seguroEnderecoModel = new SeguroEnderecoModel();
            seguroEnderecoModel.Bairro = "teste";
            seguroEnderecoModel.Cep = "teste";
            seguroEnderecoModel.Complemento = "teste";
            seguroEnderecoModel.Logradouro = "teste";
            seguroEnderecoModel.Principal = true;
            seguroEnderecoModel.IdTipoLogradouro = 1;
            seguroEnderecoModel.IdMunicipio = 1;

            var seguroBeneficiarioModel = new SeguroBeneficiarioModel();
            seguroBeneficiarioModel.Nome = "teste";
            seguroBeneficiarioModel.CPF = "teste";
            seguroBeneficiarioModel.DataNascimento = DateTime.Now;
            seguroBeneficiarioModel.ValorPercentual = 123;
            seguroBeneficiarioModel.IdSeguroParentesco = 1;

            var seguroTelefone = new SeguroClienteTelefoneModel();
            seguroTelefone.Principal = true;
            seguroTelefone.Fone = "999999999";
            seguroTelefone.DDD= "051";

            model.EnderecoCobranca = seguroEnderecoModel;
            model.Telefones = new[] { seguroTelefone }.ToList();
            
            if(usarBeneficiarios)
             model.Beneficiarios = new[] { seguroBeneficiarioModel }.ToList();

            return model;
        }

        private async Task criarEntidades()
        {
            await AddRangeAndSaveAsync(new [] {
                new SeguroPropostaDominio(12, false, 1, _usuarioTeste.Cliente.ID, (int) MeioPagamentoSeguro.CartaoCredito),
                new SeguroPropostaDominio(22, false, 2, 2, (int) MeioPagamentoSeguro.CartaoCredito)
            });
            await AddRangeAndSaveAsync(new [] {
                new SeguroCoberturaDominio(1, 'c', 21, 21, 'c', 1),
                new SeguroCoberturaDominio(2, 'b', 41, 41, 'b', 2)
            });
        }

        private void criarDadosRelacionamentos()
        {

            AddRangeAndSave(new[] {
                new TipoLogradouroDominio("t", "teste"),
                new TipoLogradouroDominio("t2", "test2")
            });

            AddRangeAndSave(new[] {
                new ClienteDominio("test"),
                new ClienteDominio("test2")
            });

            AddRangeAndSave(new[] {
                new ProdutoDominio(Produto.CartaoCreditoConsignado, "testep", "ss", false),
                new ProdutoDominio(Produto.CreditoConsignado, "testeste", "tt", false),
            });

            AddRangeAndSave(new[] {
                new SeguroProdutoDominio("TETESTE", "TESTE", DateTime.Now, DateTime.Now, Produto.CartaoCreditoConsignado, 1),
                new SeguroProdutoDominio("TETESTE222", "TESTE222", DateTime.Now, DateTime.Now, Produto.CreditoConsignado, 2),
            });

            AddRangeAndSave(new[] {
                new SeguroProdutoIcatuDominio(1, DateTime.Now, DateTime.Now, 1, 1, 1, "teste", 1, 1, 1),
                new SeguroProdutoIcatuDominio(2, DateTime.Now, DateTime.Now, 2, 2, 2, "teste2", 2, 2, 2),
            });

            AddRangeAndSave(new[] {
                new SeguroCoberturaDominio(1, 'c', 2222, 22222 , 'c', 1)
            });

            AddRangeAndSave(new[] {
                new SeguroCoberturaIcatuDominio(1, 'c', 2222, 22222 , 'c', 1)
            });

            AddRangeAndSave(new[] {
                new EstadoCivilDominio("solteiro", "solt")
            });

            AddRangeAndSave(new[] {
                new GeneroDominio("masculino", "masc")
            });

            AddRangeAndSave(new[] {
                new SeguroProfissaoDominio(1, "dev")
            });

            AddRangeAndSave(new[] {
                new SeguroProfissaoIcatuDominio(1, "dev", 1)
            });

            AddRangeAndSave(new[] {
                new UnidadeFederativaDominio("Rio grande", "RS")
            });

            AddRangeAndSave(new[] {
                new MunicipioDominio("POA", 1)
            });

            AddRangeAndSave(new[] {
                new SeguroParentescoDominio("Teste", 1)
            });

            AddRangeAndSave(new[] {
                new SeguroParentescoIcatuDominio(1, "Teste", 1)
            });

            AddRangeAndSave(new[] {
                new SeguroProfissaoDominio(1, "Teste"),
                new SeguroProfissaoDominio(2, "Teste2")
            });

            _usuarioTeste.Cliente.SetProfissao(1);
         
            _contexto.Entry(_usuarioTeste.Cliente).State = EntityState.Modified;
            SaveChanges();
        }
    }
}
