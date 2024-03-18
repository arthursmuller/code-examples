using Aplicacao.Model.EnderecoCliente;
using Aplicacao.Model.SeguroParentescoBem;
using Aplicacao.Servico;
using Dominio;
using Dominio.Enum;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class SeguroParentescoServicoTeste : ServicoTesteBase
    {
        private readonly SeguroParentescoServico _seguroParentescoServico;
        private UsuarioDominio _usuarioTeste;

        public SeguroParentescoServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();

            _seguroParentescoServico = new SeguroParentescoServico(
                _mensagens,
                _usuarioLogin,
                _contexto);
        }

        [Fact]
        public async Task Listar_Parentescos_Deve_Retornar_Parentescos()
        {
            await criarDadosRelacionamentos();
            var resultado = await _seguroParentescoServico.Listar();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado.Any());
        }

        [Fact]
        public async Task Listar_ParentescosIcatu_Deve_Retornar_ParentescosIcatu()
        {
            await criarDadosRelacionamentos();

            await AddRangeAndSaveAsync(new[] {
                new SeguroParentescoIcatuDominio(1, "test", 1),
                new SeguroParentescoIcatuDominio(2, "test2", 2)
            });
            
            var resultado = await _seguroParentescoServico.ListarIcatu();

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado.Any());
        }

        [Fact]
        public async Task Criar_Parentescos_Deve_Retornar_Parentescos()
        {
            var resultado = await _seguroParentescoServico.Adicionar(new[] { new CriarSeguroParentescoModel(1, "teste") });

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado.Any());
        }

        [Fact]
        public async Task Criar_ParentescosIcatu_Deve_Retornar_True()
        {
            await criarDadosRelacionamentos();
           
            var resultado = await _seguroParentescoServico.Adicionar(new[] { new CriarSeguroParentescoIcatuModel(1, "teste", 1) });

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
        }


        private async Task criarDadosRelacionamentos()
        {
            await AddRangeAndSaveAsync(new[] {
                new SeguroParentescoDominio("test", 1),
                new SeguroParentescoDominio("test2", 2)
            });
        }
    }
}
