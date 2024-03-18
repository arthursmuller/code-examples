using B.Mensagens.Interfaces;
using Dominio;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class TipoDocumentoServico : ServicoBase
    {
        public TipoDocumentoServico(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto)
        : base(mensagens, usuarioLogin, contexto) { }

        public async Task<IEnumerable<TipoDocumentoModel>> ListarIdentificaoPessoal()
            => (await _contexto.TiposDocumento.Where(tp => tp.TipoDocumentoIdentificacaoPessoal).ToListAsync())
                .Select(i => new TipoDocumentoModel((int) i.ID, i.Nome, i.Codigo));
    }
}

