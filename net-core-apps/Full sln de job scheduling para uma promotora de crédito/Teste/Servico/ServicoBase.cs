using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using B.Mensagens.Implementacoes;
using B.Mensagens.Interfaces;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;

namespace Teste.Servico
{
    public class ServicoBase
    {
        protected readonly IBemMensagens _mensageria;
        protected readonly PlataformaClienteContexto _contexto;

        public ServicoBase()
        {
            var builder = new DbContextOptionsBuilder<PlataformaClienteContexto>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            _contexto = new PlataformaClienteContexto(builder.Options);
            _mensageria = new BemMensagens();
        }

        protected async Task<IEnumerable<T>> getEntidades<T>() where T : class
            => await _contexto.Set<T>().ToListAsync();

    }
}
