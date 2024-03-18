using Aplicacao.Model.EnderecoCliente;
using Aplicacao.Servico;
using Dominio;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class EnderecoClienteServicoTeste : ServicoTesteBase
    {
        private readonly EnderecoClienteServico _enderecoClienteServico;
        private UsuarioDominio _usuarioTeste;

        public EnderecoClienteServicoTeste() : base()
        {
            criarDadosRelacionamentos();

            _usuarioTeste = CriarUsuarioTeste();

            var localizacaoServicoMock = new Mock<ILocalizacaoServico>();

            _enderecoClienteServico = new EnderecoClienteServico(_mensagens, _usuarioLogin, _contexto, localizacaoServicoMock.Object);
        }

        [Fact]
        public async Task GravarEndereco_SendoNovoPrincipal_DevePersistirComoUnicoPrincipal()
        {
            var enderecosExistentes = criarEnderecos();
            var requisicao = new EnderecoClienteModel()
            {
                Titulo = "Novo",
                IdMunicipio = 1,
                Bairro = "Centro Novo",
                IdTipoLogradouro = 1,
                Logradouro = "",
                Numero = 999,
                Complemento = null,
                Cep = "90000999",
                Principal = true,
            };

            var resultado = await _enderecoClienteServico.GravarEndereco(requisicao);

            var resultadoBanco = consultarEnderecosBanco();

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(resultadoBanco.Count(), enderecosExistentes.Count() + 1);
            Assert.Equal(1, resultadoBanco.Count(e => e.Principal));
            Assert.Equal(resultadoBanco.First(e => e.Principal).Numero, 999);
        }

        [Fact]
        public async Task GravarEndereco_DeveAtualizar_Endereco_Se_Ja_Existir()
        {
            var enderecosExistentes = criarEnderecos();
            var requisicao = new EnderecoClienteModel()
            {
                Id = enderecosExistentes.FirstOrDefault().ID,
                Titulo = "Novo",
                IdMunicipio = 1,
                Bairro = "Centro Novo",
                IdTipoLogradouro = 1,
                Logradouro = "",
                Numero = 999,
                Complemento = null,
                Cep = "90000999",
                Principal = true,
            };

            var resultado = await _enderecoClienteServico.GravarEndereco(requisicao);

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(resultado.Titulo, requisicao.Titulo);
            Assert.Equal(resultado.Bairro, requisicao.Bairro);
            Assert.Equal(resultado.Municipio.Id, requisicao.IdMunicipio);
            Assert.Equal(resultado.Complemento, requisicao.Complemento);
            Assert.Equal(resultado.Cep, requisicao.Cep);
            Assert.Equal(resultado.Logradouro, requisicao.Logradouro);
            Assert.Equal(resultado.Principal, requisicao.Principal);
        }

        [Fact]
        public async Task Deletar_Endereco_Deve_Remover_Se_Nao_For_Principal()
        {
            var enderecosExistentes = criarEnderecos();
            var enderecoId = enderecosExistentes.LastOrDefault().ID;
            var resultado = await _enderecoClienteServico
                    .DeletarEndereco(enderecoId);
            
            var resultadoBanco = consultarEnderecosBanco();
            var enderecoAtualizado = resultadoBanco.FirstOrDefault(e => e.ID == enderecoId);

            Assert.True(enderecoAtualizado.Deletado);
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task Obter_Endereco_Principal_Deve_Retornar_Endereco()
        {
            criarEnderecos();
            var resultado = await _enderecoClienteServico
                    .ObterEnderecoPrincipal();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
        }

        [Fact]
        public async Task GravarEndereco_SendoSecundario_DevePersistirTendoUnicoPrincipal()
        {
            var enderecosExistentes = criarEnderecos();
            var requisicao = new EnderecoClienteModel()
            {
                Titulo = "Novo",
                IdMunicipio = 1,
                Bairro = "Centro Novo",
                IdTipoLogradouro = 1,
                Logradouro = "",
                Numero = 999,
                Complemento = null,
                Cep = "90000999",
                Principal = false,
            };

            var resultado = await _enderecoClienteServico.GravarEndereco(requisicao);

            var resultadoBanco = consultarEnderecosBanco();

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(resultadoBanco.Count(), enderecosExistentes.Count() + 1);
            Assert.Equal(1, resultadoBanco.Count(e => e.Principal));
            Assert.Equal(resultadoBanco.First(e => e.Principal).Numero, enderecosExistentes.First(e => e.Principal).Numero);
        }

        [Fact]
        public async Task ObterEnderecos_Deve_Retornar_Enderecos()
        {
            var enderecosExistentes = criarEnderecos();
 
            var resultado = await _enderecoClienteServico.BuscarEnderecosPorCliente();

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(resultado.Count(), enderecosExistentes.Count());
        }

        [Fact]
        public async Task AtualizarEndereco_IdInvalida_DeveRetornarErro()
        {
            var enderecosExistentes = criarEnderecos();
            var requisicao = new EnderecoClienteModel()
            {
                Titulo = "Novo",
                IdMunicipio = 1,
                Bairro = "Centro Novo",
                IdTipoLogradouro = 1,
                Logradouro = "",
                Numero = 999,
                Complemento = null,
                Cep = "90000999",
                Principal = false,
            };

            var resultado = await _enderecoClienteServico.AtualizarEndereco(99999, requisicao);

            var resultadoBanco = consultarEnderecosBanco();

            Assert.True(_mensagens.PossuiErros);
            Assert.Equal(resultadoBanco.Count(), enderecosExistentes.Count());
        }

        [Fact]
        public async Task AtualizarEndereco_SendoSecundarioParaPrincipal_DevePersistirComoUnicoPrincipal()
        {
            var enderecosExistentes = criarEnderecos();
            var enderecoAtualizar = enderecosExistentes.First(e => !e.Principal);

            var requisicao = new EnderecoClienteModel()
            {
                Titulo = "Novo",
                IdMunicipio = 1,
                Bairro = "Centro Novo",
                IdTipoLogradouro = 1,
                Logradouro = "",
                Numero = 999,
                Complemento = null,
                Cep = "90000999",
                Principal = true,
            };

            var resultado = await _enderecoClienteServico.AtualizarEndereco(enderecoAtualizar.ID, requisicao);

            var resultadoBanco = consultarEnderecosBanco();

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(resultadoBanco.Count(), enderecosExistentes.Count());
            Assert.Equal(1, resultadoBanco.Count(e => e.Principal));
            Assert.Equal(resultadoBanco.First(e => e.Principal).ID, enderecoAtualizar.ID);
        }

        [Fact]
        public async Task AtualizarEndereco_SendoSecundario_DevePersistirTendoUnicoPrincipal()
        {
            var enderecosExistentes = criarEnderecos();
            var enderecoAtualizar = enderecosExistentes.First(e => !e.Principal);

            var requisicao = new EnderecoClienteModel()
            {
                Titulo = "Novo",
                IdMunicipio = 1,
                Bairro = "Centro Novo",
                IdTipoLogradouro = 1,
                Logradouro = "",
                Numero = 999,
                Complemento = null,
                Cep = "90000999",
                Principal = false,
            };

            var resultado = await _enderecoClienteServico.AtualizarEndereco(enderecoAtualizar.ID, requisicao);

            var resultadoBanco = consultarEnderecosBanco();

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(resultadoBanco.Count(), enderecosExistentes.Count());
            Assert.Equal(1, resultadoBanco.Count(e => e.Principal));
            Assert.NotEqual(resultadoBanco.First(e => e.Principal).ID, enderecoAtualizar.ID);
        }

        private List<EnderecoClienteDominio> consultarEnderecosBanco() => _contexto.EnderecosCliente.ToList();

        private List<EnderecoClienteDominio> criarEnderecos()
        {
            var enderecos = new List<EnderecoClienteDominio>()
            {
                new EnderecoClienteDominio(_usuarioTeste.Cliente.ID, "Teste 1", 1, "centro", 1, null, 100, null, "90000111", true),
                new EnderecoClienteDominio(_usuarioTeste.Cliente.ID, "Teste 2", 1, "centro", 1, null, 200, null, "90000222", false),
            };

            _contexto.EnderecosCliente.AddRange(enderecos);
            _contexto.SaveChanges();
            return enderecos;
        }

        private void criarDadosRelacionamentos()
        {
            _contexto.UnidadesFederativas.Add(new UnidadeFederativaDominio("Rio Grande do Sul", "RS"));
            _contexto.SaveChanges();

            _contexto.Municipios.Add(new MunicipioDominio("Porto Alegre", 1));
            _contexto.TiposLogradouro.Add(new TipoLogradouroDominio("Rua", "R"));
            _contexto.SaveChanges();
        }
    }
}
