using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacao.Model.ParametroOperacao;

namespace Aplicacao.Servico
{
    public interface IParametroOperacaoServico
    {
        Task<IEnumerable<ParametroOperacaoModel>> BuscarParametrosOperacao();
        Task<ParametroOperacaoNovoModel> CriarParametroOperacao(ParametroOperacaoCriacaoModel parametroOperacao);
        Task<bool> AtualizarParametroOperacao(ParametroOperacaoAtualizacaoModel parametroOperacaoAtualizado, int id);
        Task<bool> DeletarParametroOperacao(int id);
    }
}