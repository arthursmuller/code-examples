using Aplicacao.Model.SeguroProduto;
using Dominio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface ISeguroProdutoServico
    {
        Task<IEnumerable<SeguroProdutoModel>> Listar();
    }
}
