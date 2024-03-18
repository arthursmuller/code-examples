using B.Mensagens.Interfaces;
using Dominio.Abstracoes;
using Dominio.Dto;
using Dominio.Entidades;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class SeguroBemServico
    {
        protected readonly IBemMensagens _mensagens;
        private readonly IIcatuApiServico _icatuApiServico;
        private readonly ProdutoClienteContexto _contexto;

        public SeguroBemServico(
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
            var parentescosFromApi = await _icatuApiServico.GetParentescos();
            var seguroParentescosBem = await getDados<SeguroParentescoBemDominio>();
            var seguroParentescosIcatu = await getDados<SeguroParentescoIcatuDominio>();
            var novosDadosParaPopular = getDadosDivergentes(parentescosFromApi, seguroParentescosBem);
            var dadosPopuladosSeguroBem = await popularParentescoBemDados(novosDadosParaPopular);
            var novosDadosParaPopularParentescoIcatu = getDadosDivergentesTabelas(dadosPopuladosSeguroBem, seguroParentescosIcatu);
            await popularParentescoIcatuDados(novosDadosParaPopularParentescoIcatu);
        }

        private IEnumerable<IcatuParentescoDto> getDadosDivergentes(IEnumerable<IcatuParentescoDto> icatuParentescoDtos, IEnumerable<SeguroParentescoBemDominio> parentescosBem)
           => icatuParentescoDtos.Where(ipd => !parentescosBem.Any(pb => pb.Codigo == ipd.CodigoParentesco));

        private IEnumerable<SeguroParentescoBemDominio> getDadosDivergentesTabelas(IEnumerable<SeguroParentescoBemDominio> parentescosBem, IEnumerable<SeguroParentescoIcatuDominio> parentescosIcatu)
           => parentescosBem.Where(pb => !parentescosIcatu.Any(pi => pi.Codigo == pb.Codigo));

        private async Task<IEnumerable<SeguroParentescoBemDominio>> popularParentescoBemDados(IEnumerable<IcatuParentescoDto> parentescoIcatuDtos)
        {
            var parentescosBem = new List<SeguroParentescoBemDominio>();

            foreach (var parentesco in parentescoIcatuDtos)
                parentescosBem.Add(new SeguroParentescoBemDominio
                {
                    Codigo = parentesco.CodigoParentesco,
                    Descricao = parentesco.NomeParentesco
                });

            await addRangeAndSaveAsync(parentescosBem);

            return parentescosBem;
        }

        private async Task popularParentescoIcatuDados(IEnumerable<SeguroParentescoBemDominio> parentescosBem)
        {
            List<SeguroParentescoIcatuDominio> segurosParentescoIcatu = new List<SeguroParentescoIcatuDominio>();
            foreach (var parentesco in parentescosBem)
                segurosParentescoIcatu.Add(new SeguroParentescoIcatuDominio(parentesco.Codigo, parentesco.Descricao, parentesco.ID));

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
