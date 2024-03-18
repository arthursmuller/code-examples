using B.Mensagens.Interfaces;
using Dominio.Abstracoes;
using Dominio.Dto;
using Dominio.Entidades;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class SeguroProfissaoServico
    {
        protected readonly IBemMensagens _mensagens;
        private readonly IIcatuApiServico _icatuApiServico;
        private readonly ProdutoClienteContexto _contexto;

        public SeguroProfissaoServico(
            IBemMensagens mensagens,
            ProdutoClienteContexto contexto,
            IIcatuApiServico icatuApiServico)
        {
            _mensagens = mensagens;
            _contexto = contexto;
            _icatuApiServico = icatuApiServico;
        }
        public async Task AtualizarDados()
        {
            System.Diagnostics.Debugger.Launch();

            var profissoesFromApi = await _icatuApiServico.GetProfissoes();
            var seguroProfissoesBem = await getDados<SeguroProfissaoBemDominio>();
            var seguroProfissoesIcatu = await getDados<SeguroProfissaoIcatuDominio>();
            var novosDadosParaPopularFromApi = getDadosDivergentes(profissoesFromApi, seguroProfissoesBem);
            var dadosPopuladosSeguroBem = await popularProfissaoBemDados(novosDadosParaPopularFromApi);
            var novosDadosParaPopularProfissoesIcatu = getDadosDivergentesTabelas(dadosPopuladosSeguroBem, seguroProfissoesIcatu);
            await popularProfissaoIcatuDados(novosDadosParaPopularProfissoesIcatu);
        }

        private IEnumerable<IcatuProfissaoDto> getDadosDivergentes(IEnumerable<IcatuProfissaoDto> icatuProfissaoDtos, IEnumerable<SeguroProfissaoBemDominio> profissaoBem)
            => icatuProfissaoDtos.Where(ipd => !profissaoBem.Any(pb => pb.Codigo == ipd.CodigoProfissao));

        private IEnumerable<SeguroProfissaoBemDominio> getDadosDivergentesTabelas(IEnumerable<SeguroProfissaoBemDominio> profissaoBem, IEnumerable<SeguroProfissaoIcatuDominio> profissaoIcatu)
           => profissaoBem.Where(pb => !profissaoIcatu.Any(pi => pi.Codigo == pb.Codigo));

        private async Task<IEnumerable<SeguroProfissaoBemDominio>> popularProfissaoBemDados(IEnumerable<IcatuProfissaoDto> icatuProfissaoDtos)
        {
            var profissoesBem = new List<SeguroProfissaoBemDominio>();

            foreach (var profissao in icatuProfissaoDtos)
                profissoesBem.Add(new SeguroProfissaoBemDominio
                {
                    Codigo = profissao.CodigoProfissao,
                    Descricao = profissao.NomeProfissao
                });

            await addRangeAndSaveAsync(profissoesBem);

            return profissoesBem;
        }

        private async Task popularProfissaoIcatuDados(IEnumerable<SeguroProfissaoBemDominio> profissaoBem)
        {
            var segurosParentescoIcatu = new List<SeguroProfissaoIcatuDominio>();

            foreach (var profissao in profissaoBem)
                segurosParentescoIcatu.Add(new SeguroProfissaoIcatuDominio(profissao.Codigo, profissao.Descricao, profissao.ID));

            await addRangeAndSaveAsync(segurosParentescoIcatu);
        }

        private async Task<IEnumerable<T>> getDados<T>() where T : class
            => await _contexto.Set<T>().ToListAsync();

        private async Task addRangeAndSaveAsync<T>(IEnumerable<T> entities) where T : class
        {
            await _contexto.Set<T>().AddRangeAsync(entities);
            await _contexto.SaveChangesAsync();
        }
    }
}
