using Aplicacao.Model.Cliente;
using Aplicacao.Model.Conjuge;
using Aplicacao.Model.Genero;
using Aplicacao.Model.TipoRegimeCasamento;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects.v2;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class ConjugeServico : ServicoBase
    {
        public ConjugeServico(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto)
        : base(mensagens, usuarioLogin, contexto) { }

        public async Task<ConjugeExibicaoModel> Adicionar(ConjugeModel model)
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            var cpf = new CPF(model.CPF);

            if (!string.IsNullOrWhiteSpace(cpf.ToString()) && !cpf.IsValid(_mensagens))
                return null;

            var novoConjuge = new ConjugeDominio(
                cpf,
                model.Nome,
                model.DataNascimento,
                usuario.Cliente.ID,
                model.IdGenero,
                model.IdTipoRegimeCasamento
            );

            await addAndSaveAsync(novoConjuge);
            await carregarRelacoes(novoConjuge);

            usuario.Cliente.SetConjuge(novoConjuge.ID);

            await SaveChangesAsync();

            return converterParaModel(novoConjuge);
        }

        public async Task<ConjugeExibicaoModel> Atualizar(ConjugeModel model)
        {
            var conjuge = await getConjugeUsuarioAutenticado();

            var cpf = new CPF(model.CPF);
            if (!string.IsNullOrWhiteSpace(cpf.ToString()) && !cpf.IsValid(_mensagens))
                return null;

            conjuge.SetPropriedadesAtualizadas(cpf, model.Nome, model.DataNascimento);
            conjuge.SetTipoRegimeCasamento(model.IdTipoRegimeCasamento);
            conjuge.SetGenero(model.IdGenero);

            await SaveChangesAsync();

            return converterParaModel(conjuge);
        }

        public async Task<bool> Remover()
        {
            var conjuge = await getConjugeUsuarioAutenticado();
            _contexto.Remove(conjuge);
            await SaveChangesAsync();
            return true;
        }

        private async Task<ConjugeDominio> getConjugeUsuarioAutenticado()
        {
            var usuario = await ObterDadosUsuarioAutenticado();
            var conjuge = await _contexto.Conjuge
                .Include(c => c.Cliente)
                .Include(c => c.Genero)
                .Include(c => c.TipoRegimeCasamento)
                .FirstOrDefaultAsync(c => c.Cliente.ID == usuario.Cliente.ID);

            if (conjuge is null)
            {
                _mensagens.AdicionarErro(Mensagens.Conjugue_NaoEncontrado, B.Mensagens.EnumMensagemTipo.negocio);
                return null;
            }

            return conjuge;
        }

        private async Task addAndSaveAsync<T>(T entity)
        {
            await _contexto.AddAsync(entity);
            await SaveChangesAsync();
        }

        private async Task carregarRelacoes(ConjugeDominio conjuge)
        {
            await _contexto.Entry(conjuge).Reference(s => s.TipoRegimeCasamento).LoadAsync();
            await _contexto.Entry(conjuge).Reference(s => s.Genero).LoadAsync();
        }


        private ConjugeExibicaoModel converterParaModel(ConjugeDominio conjuge) =>
            new ConjugeExibicaoModel(
                conjuge.ID,
                conjuge.CPF,
                conjuge.Nome,
                conjuge.DataNascimento,
                new GeneroModel(conjuge.Genero.ID, conjuge.Genero.Sigla, conjuge.Genero.Descricao),
                new ClienteExibicaoModel(conjuge.Cliente),
                new TipoRegimeCasamentoExibicaoModel(conjuge.TipoRegimeCasamento.ID, conjuge.TipoRegimeCasamento.Descricao));
        
    }
}

