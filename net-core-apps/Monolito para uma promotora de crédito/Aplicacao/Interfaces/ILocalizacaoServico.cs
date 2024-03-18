using Aplicacao.CEP;
using Aplicacao.Model.Municipio;
using Aplicacao.Model.TipoLogradouro;
using Aplicacao.Model.UnidadeFederativa;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface ILocalizacaoServico
    {
        Task<IEnumerable<UnidadeFederativaModel>> ListarUnidadesFederativas();

        Task<IEnumerable<MunicipioModel>> ListarMunicipios(int idUF, string busca);

        Task<MunicipioModel> ObterMunicipio(int idUF, string municipio);

        Task<IEnumerable<TipoLogradouroModel>> ListarTiposLogradouro(string descricao);

        Task<TipoLogradouroModel> ObterTipoLogradouroPorCodigo(string codigo);

        Task<CepModel> ObterLocalizacaoPorCEP(string descricao);

        Task<IEnumerable<CepModel>> BuscarCeps(CepEnvioModel parametros);
    }
}
