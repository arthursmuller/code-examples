using Aplicacao.Model.Aeronautica;
using Aplicacao.Model.Convenio;
using Aplicacao.Model.InssEspecieBeneficio;
using Aplicacao.Model.Marinha;
using Aplicacao.Model.SiapeTipoFuncional;
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
    [Route("convenios")]
    [ExcludeFromCodeCoverage]
    public class ConvenioController : BaseController
    {
        private readonly ConvenioServico _convenioServico;

        public ConvenioController(ConvenioServico convenioServico, IBemMensagens mensagens) : base(mensagens)
            => _convenioServico = convenioServico;

        [HttpGet]
        public async Task<RetornoApi<IEnumerable<ConvenioModel>>> Get()
        {
            var convenios = await _convenioServico.ListarConvenios();

            return FormatarRetorno(convenios);
        }

        [HttpGet("siape/orgaos")]
        public async Task<RetornoApi<IEnumerable<ConvenioOrgaoModel>>> ListarOrgaos([FromQuery] string termo)
        {
            var orgaos = await _convenioServico.ListarOrgaosSiape(termo);

            return FormatarRetorno(orgaos);
        }

        [HttpGet("siape/tipos-funcionais")]
        public async Task<RetornoApi<IEnumerable<SiapeTipoFuncionalModel>>> ListarSiapeTiposFuncionais()
        {
            var orgaos = await _convenioServico.ListarSiapeTiposFuncionais();

            return FormatarRetorno(orgaos);
        }

        [HttpGet("inss/especies-beneficios")]
        public async Task<RetornoApi<IEnumerable<InssEspecieBeneficioModel>>> ListarInssEspeciesBeneficios()
        {
            var orgaos = await _convenioServico.ListarInssEspeciesBeneficios();

            return FormatarRetorno(orgaos);
        }

        [HttpGet("marinha/tipos-funcionais")]
        public async Task<RetornoApi<IEnumerable<MarinhaTipoFuncionalModel>>> ListarMarinhaTiposFuncionais()
        {
            var tiposFuncionais = await _convenioServico.ListarMarinhaTiposFuncionais();

            return FormatarRetorno(tiposFuncionais);
        }

        [HttpGet("marinha/tipos-cargos")]
        public async Task<RetornoApi<IEnumerable<MarinhaCargosModel>>> ListarMarinhaCargos()
        {
            var cargos = await _convenioServico.ListarMarinhaCargos();

            return FormatarRetorno(cargos);
        }

        [HttpGet("aeronautica/tipos-funcionais")]
        public async Task<RetornoApi<IEnumerable<AeronauticaTipoFuncionalModel>>> ListarAeronautiaTiposFuncionais()
        {
            var tiposFuncionais = await _convenioServico.ListarAeronauticaTiposFuncionais();

            return FormatarRetorno(tiposFuncionais);
        }

        [HttpGet("aeronautica/tipos-cargos")]
        public async Task<RetornoApi<IEnumerable<AeronauticaCargosModel>>> ListarAeronauticaCargos()
        {
            var cargos = await _convenioServico.ListarAeronauticaCargos();

            return FormatarRetorno(cargos);
        }
    }
}
