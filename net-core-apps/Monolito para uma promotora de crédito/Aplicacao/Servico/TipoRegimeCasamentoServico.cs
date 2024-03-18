using Aplicacao.Model.TipoRegimeCasamento;
using B.Mensagens.Interfaces;
using Dominio;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class TipoRegimeCasamentoServico : ServicoBase
    {
        public TipoRegimeCasamentoServico(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto)
        : base(mensagens, usuarioLogin, contexto) { }

        public async Task<IEnumerable<TipoRegimeCasamentoModel>> Listar()
            => (await _contexto.TipoRegimeCasamento.ToListAsync())
                .Select(i => new TipoRegimeCasamentoModel(i.ID, i.Descricao));
    }
}
