using Aplicacao;
using Aplicacao.Filters;
using Aplicacao.Model.Anexo;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class AnexoController : BaseController
    {
        private readonly IAnexoServico _anexoServico;

        public AnexoController(IAnexoServico anexoServico, IBemMensagens mensagens) : base(mensagens)
            => _anexoServico = anexoServico;
        
        [HttpPost("anexos")]
        public async Task<RetornoApi<AnexoModel>> Post([FromBody] AnexoCriacaoModel anexoCriacao)
        {
            var anexo = await _anexoServico.GravarArquivo(anexoCriacao);

            return FormatarRetorno(anexo);
        }

        [HttpGet("usuarios/autenticado/anexos")]
        public async Task<RetornoApi<IEnumerable<AnexoModel>>> ObterParaUsuarioAutenticado()
        {
            var anexos = await _anexoServico.BuscarAnexosPorUsuarioAutenticado();

            return FormatarRetorno(anexos);
        }

        [HttpGet("usuarios/{cpf}/anexos")]
        [Authorize(Roles = "admin")]
        [ServiceFilter(typeof(FiltroLogRequisicao))]
        public async Task<RetornoApi<IEnumerable<AnexoModel>>> ObterPorCpf(string cpf)
        {
            var anexos = await _anexoServico.BuscarAnexosPorCpfUsuario(cpf);

            return FormatarRetorno(anexos);
        }

        [HttpGet("anexos/{id}")]
        public async Task<RetornoApi<AnexoModel>> Get(int id)
        {
            var anexo = await _anexoServico.BuscarAnexo(id);

            return FormatarRetorno(anexo);
        }

        [HttpDelete("anexos/{id}")]
        public async Task<RetornoApi<bool>> Delete(int id)
        {
            var anexoDeletado = await _anexoServico.DeletarAnexo(id);

            return FormatarRetorno(anexoDeletado);
        }

        [Authorize]
        [HttpGet("anexos/tipos-documento")]
        public async Task<RetornoApi<IEnumerable<TipoDocumentoModel>>> ListarTiposDocumento()
        {
            var opcoes = await _anexoServico.ListarTiposDocumentos();

            return FormatarRetorno(opcoes);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("anexos/solicitacoes")]
        public async Task<RetornoApi<bool>> SolicitarAnexoParaCliente([FromBody] AnexoSolicitacaoModel solicitacao)
        {
            var resultado = await _anexoServico.SolicitarAnexoParaCliente(solicitacao);

            return FormatarRetorno(resultado);
        }
    }
}
