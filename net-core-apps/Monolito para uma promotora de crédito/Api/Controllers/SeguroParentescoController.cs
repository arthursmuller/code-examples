using Aplicacao.Model.SeguroParentescoBem;
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
    [ExcludeFromCodeCoverage]
    public class SeguroParentescoController : BaseController
    {
        private readonly SeguroParentescoServico _seguroParentescoServico;

        public SeguroParentescoController(SeguroParentescoServico seguroParentescoServico, IBemMensagens mensagens) : base(mensagens)
            => _seguroParentescoServico = seguroParentescoServico;

        [HttpGet("/clientes/autenticado/seguros/parentescos")]
        public async Task<RetornoApi<IEnumerable<SeguroParentescoExibicaoModel>>> Get()
            => FormatarRetorno(await _seguroParentescoServico.Listar());

        [Authorize(Roles = "admin")]
        [HttpPost("/administrador/autenticado/seguros/parentescos")]
        public async Task<RetornoApi<IEnumerable<SeguroParentescoExibicaoModel>>> Adicionar(IEnumerable<CriarSeguroParentescoModel> seguroParentescos)
            => FormatarRetorno(await _seguroParentescoServico.Adicionar(seguroParentescos));

        [Authorize(Roles = "admin")]
        [HttpPost("/administrador/autenticado/seguros/parentescos/icatu")]
        public async Task<RetornoApi<bool>> Adicionar(IEnumerable<CriarSeguroParentescoIcatuModel> seguroParentescos)
           => FormatarRetorno(await _seguroParentescoServico.Adicionar(seguroParentescos));
    }
}
