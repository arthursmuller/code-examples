using B.Mensagens.Interfaces;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class ServicoBase
    {
        protected readonly IBemMensagens _mensagens;
        protected readonly PlataformaClienteContexto _contexto;

        public ServicoBase(IBemMensagens mensagens, PlataformaClienteContexto contexto)
        {
            _mensagens = mensagens;
            _contexto = contexto;
        }

        protected async Task<IEnumerable<T>> getDados<T>() where T : class
            => await _contexto.Set<T>().ToListAsync();

        protected async Task addRangeAndSaveAsync<T>(IEnumerable<T> entities) where T : class
        {
            await _contexto.Set<T>().AddRangeAsync(entities);
            await _contexto.SaveChangesAsync();
        }
    }
}
