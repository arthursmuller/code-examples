using Aplicacao.Model.Conjuge;
using Aplicacao.Servico;
using Dominio;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class ConjugeServicoTeste : ServicoTesteBase
    {
        private readonly ConjugeServico _servico;
        private UsuarioDominio _usuarioTeste;

        public ConjugeServicoTeste() : base()
        {
            criarEntidades();

            _usuarioTeste = CriarUsuarioTeste();

            _servico = new ConjugeServico(_mensagens, _usuarioLogin, _contexto);
        }

        [Fact]
        public async Task Criar_Conjuge_Deve_Adicionar()
        {

            var resultado = await criarConjuge();

            var entidadeAdicionada = await _contexto.Conjuge
                .Include(c => c.Cliente)
                .Where(conjuge => conjuge.Cliente.ID.Equals(_usuarioTeste.Cliente.ID))
                .FirstOrDefaultAsync();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.NotNull(entidadeAdicionada);
        }

        [Fact]
        public async Task Atualizar_Conjuge_Deve_Atualizar()
        {
            await criarConjuge();

            var conjugeModel = new ConjugeModel("000.000.000-00", "teste2", DateTime.Now, _usuarioTeste.Cliente.ID, 2, 2);

            var resultado = await _servico.Atualizar(conjugeModel);

            var entidadeAdicionada = await _contexto.Conjuge
                .Include(c => c.Cliente)
                .Where(conjuge => conjuge.Cliente.ID.Equals(_usuarioTeste.Cliente.ID))
                .FirstOrDefaultAsync();

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.Equal(entidadeAdicionada.Nome, conjugeModel.Nome);
            Assert.Equal(entidadeAdicionada.CPF, new CPF(conjugeModel.CPF).ToString());
            Assert.Equal(entidadeAdicionada.DataNascimento, conjugeModel.DataNascimento);
            Assert.Equal(entidadeAdicionada.IdGenero, conjugeModel.IdGenero);
            Assert.Equal(entidadeAdicionada.IdTipoRegimeCasamento, conjugeModel.IdTipoRegimeCasamento);
        }

        [Fact]
        public async Task Atualizar_Conjuge_Com_CPF_Invalido_Deve_Retornar_Null()
        {
            await criarConjuge();

            var conjugeModel = new ConjugeModel("12312312312", "teste2", DateTime.Now, _usuarioTeste.Cliente.ID, 2, 2);

            var resultado = await _servico.Atualizar(conjugeModel);

            var entidadeAdicionada = await _contexto.Conjuge
                .Include(c => c.Cliente)
                .Where(conjuge => conjuge.Cliente.ID.Equals(_usuarioTeste.Cliente.ID))
                .FirstOrDefaultAsync();

            Assert.True(_mensagens.PossuiErros);
            Assert.Null(resultado);
        }

        [Fact]
        public async Task Deletar_Conjuge_Deve_Deletar()
        {
            await criarConjuge();

            await _servico.Remover();

            var entidadeAdicionada = await _contexto.Conjuge
                .Include(c => c.Cliente)
                .Where(conjuge => conjuge.Cliente.ID.Equals(_usuarioTeste.Cliente.ID))
                .FirstOrDefaultAsync();

            Assert.False(_mensagens.PossuiErros);
            Assert.Null(entidadeAdicionada);
        }

        private async Task<ConjugeExibicaoModel> criarConjuge()
        {
            var conjugeModel = new ConjugeModel("000.000.000-00", "teste", DateTime.Now, _usuarioTeste.Cliente.ID, 1, 1);

            var resultado = await _servico.Adicionar(conjugeModel);
            
            return resultado;
        }

        private List<GeneroDominio> criarEntidades()
        {
            var generos = new List<GeneroDominio>()
            {
                new GeneroDominio("Masculino", "M"),
                new GeneroDominio("Masculino", "M")
            };

            
            var regimes = new List<TipoRegimeCasamentoDominio>()
            {
                new TipoRegimeCasamentoDominio() { Descricao = "Teste1" },
                new TipoRegimeCasamentoDominio() { Descricao = "Teste2" }
            };
            _contexto.Generos.AddRange(generos);
            _contexto.TipoRegimeCasamento.AddRange(regimes);

            SaveChanges();

            return generos;
        }
    }


}
