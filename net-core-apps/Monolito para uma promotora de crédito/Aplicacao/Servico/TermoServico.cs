using B.Mensagens.Interfaces;
using Dominio;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class TermoServico : ServicoBase, ITermoServico
    {
        public TermoServico(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto) : base(mensagens, usuarioLogin, contexto) { }

        public async Task<IEnumerable<TermoDominio>> ObterTermosPendentesAceiteUsuario()
            => await ObterTermosPendentesAceiteUsuario(_usuarioLogin.IdUsuario);

        public async Task<IEnumerable<TermoDominio>> ObterTermosPendentesAceiteUsuario(int idUsuario)
        {
            return await _contexto.Termos
                                    .Include(t => t.UsuariosTermos)
                                    .Where(t => t.DataInicioVigencia.Date <= DateTime.Now.Date && !t.UsuariosTermos.Any(u => u.IdUsuario.Equals(idUsuario)))
                                    .AsNoTracking()
                                    .ToListAsync();
        }
    }
}
