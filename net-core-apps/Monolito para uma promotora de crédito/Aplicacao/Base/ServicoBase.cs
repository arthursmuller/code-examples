using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class ServicoBase
    {
        protected readonly IBemMensagens _mensagens;
        protected readonly IUsuarioLogin _usuarioLogin;
        protected readonly PlataformaClienteContexto _contexto;

        public ServicoBase(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto)
        {
            _mensagens = mensagens;
            _usuarioLogin = usuarioLogin;
            _contexto = contexto;
        }

        public async Task<UsuarioDominio> ObterDadosUsuarioAutenticado()
        {
            var usuario = await _contexto.Usuarios
                                    .Include(u => u.Cliente)
                                    .FirstOrDefaultAsync(u => u.ID.Equals(_usuarioLogin.IdUsuario));

            if (usuario == null)
                _mensagens.AdicionarErro(Mensagens.Usuario_NaoEncontrado, B.Mensagens.EnumMensagemTipo.banco);
            else if (usuario.Cliente == null)
                _mensagens.AdicionarErro(Mensagens.Cliente_NaoEncontrado, B.Mensagens.EnumMensagemTipo.banco);

            return usuario;
        }

        protected async Task<IEnumerable<T>> getDados<T>() where T : class
            => await _contexto.Set<T>().ToListAsync();

        protected async Task addRangeAndSaveAsync<T>(IEnumerable<T> entities) where T : class
        {
            await _contexto.Set<T>().AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        protected async Task<int> SaveChangesAsync() =>
            await _contexto.SaveChangesAsync();
    }
}
