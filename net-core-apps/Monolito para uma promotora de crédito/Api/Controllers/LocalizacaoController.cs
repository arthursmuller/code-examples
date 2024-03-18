using Aplicacao.CEP;
using Aplicacao.Model.Municipio;
using Aplicacao.Model.TipoLogradouro;
using Aplicacao.Model.UnidadeFederativa;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("localizacoes")]
    [ExcludeFromCodeCoverage]
    public class LocalizacaoController : BaseController
    {
        private readonly ILocalizacaoServico _localizacaoServico;

        public LocalizacaoController(ILocalizacaoServico localizacaoServico, IBemMensagens mensagens) : base(mensagens)
            => _localizacaoServico = localizacaoServico;

        [HttpGet("unidades-federativas")]
        public async Task<RetornoApi<IEnumerable<UnidadeFederativaModel>>> ListarUnidadesFederativas()
        {
            var unidadesFederativas = await _localizacaoServico.ListarUnidadesFederativas();

            return FormatarRetorno(unidadesFederativas);
        }

        [HttpGet("unidades-federativas/{idUF}/municipios")]
        public async Task<RetornoApi<IEnumerable<MunicipioModel>>> ListarMunicipios(int idUF, [FromQuery] string municipio)
        {
            var municipios = await _localizacaoServico.ListarMunicipios(idUF, municipio);

            return FormatarRetorno(municipios);
        }

        [HttpGet("tipos-logradouro")]
        public async Task<RetornoApi<IEnumerable<TipoLogradouroModel>>> ListarTiposLogradouro([FromQuery] string descricao)
        {
            var tipos = await _localizacaoServico.ListarTiposLogradouro(descricao);

            return FormatarRetorno(tipos);
        }

        [HttpGet("cep/{codigo}")]
        public async Task<RetornoApi<CepModel>> ObterLocalizacaoPorCEP(string codigo)
        {
            var localizacao = await _localizacaoServico.ObterLocalizacaoPorCEP(codigo);

            return FormatarRetorno(localizacao);
        }

        [HttpGet("ceps")]
        public async Task<RetornoApi<IEnumerable<CepModel>>> BuscarCeps([FromQuery] CepEnvioModel parametros)
        {
            var localizacoes = await _localizacaoServico.BuscarCeps(parametros);

            return FormatarRetorno(localizacoes);
        }
    }
}
