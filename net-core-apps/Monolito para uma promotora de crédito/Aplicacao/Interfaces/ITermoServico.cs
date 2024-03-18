using Dominio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface ITermoServico
    {
        Task<IEnumerable<TermoDominio>> ObterTermosPendentesAceiteUsuario(int idUsuario);
    }
}
