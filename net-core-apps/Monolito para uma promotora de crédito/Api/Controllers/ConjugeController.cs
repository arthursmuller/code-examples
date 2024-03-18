using Aplicacao.Model.Conjuge;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("/clientes/autenticado/conjuges")]
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class ConjugeController : BaseController
    {
        private readonly ConjugeServico _servico;

        public ConjugeController(ConjugeServico servico, IBemMensagens mensagens) : base(mensagens)
            => _servico = servico;

        [HttpPost]
        public async Task<RetornoApi<ConjugeExibicaoModel>> Adicionar([FromBody] ConjugeModel model)
            => FormatarRetorno(await _servico.Adicionar(model));

        [HttpPut]
        public async Task<RetornoApi<ConjugeExibicaoModel>> Atualizar([FromBody] ConjugeModel model)
            => FormatarRetorno(await _servico.Atualizar(model));

        [HttpDelete]
        public async Task<RetornoApi<bool>> Remover()
           => FormatarRetorno(await _servico.Remover());
    }
}
