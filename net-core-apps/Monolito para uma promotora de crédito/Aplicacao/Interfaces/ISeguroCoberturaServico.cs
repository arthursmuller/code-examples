using Dominio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface ISeguroCoberturaServico
    {
        Task<IEnumerable<SeguroCoberturaDominio>> ListarSeguros();
    }
}

