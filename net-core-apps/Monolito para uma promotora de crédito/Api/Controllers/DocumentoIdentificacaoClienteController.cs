using Aplicacao.Model.DocumentoIdentificacaoCliente;
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
    [Route("clientes/autenticado/documentos")]
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class DocumentoIdentificacaoClienteController : BaseController
    {
        private readonly DocumentoIdentificacaoClienteServico _documentoIdentificacaoClienteServico;
        public DocumentoIdentificacaoClienteController(DocumentoIdentificacaoClienteServico documentoIdentificacaoClienteServico, IBemMensagens mensagens) : base(mensagens)
            => _documentoIdentificacaoClienteServico = documentoIdentificacaoClienteServico;

        [HttpGet]
        public async Task<RetornoApi<IEnumerable<DocumentoIdentificacaoClienteExibicaoModel>>> Get()
        {
            var documentos = await _documentoIdentificacaoClienteServico.BuscarDocumentoPorCliente();

            return FormatarRetorno(documentos);
        }

        [HttpPost]
        public async Task<RetornoApi<DocumentoIdentificacaoClienteExibicaoModel>> Post([FromBody] DocumentoIdentificacaoClienteModel documentoIdentificacao)
        {
            var documento = await _documentoIdentificacaoClienteServico.GravarDocumento(documentoIdentificacao);

            return FormatarRetorno(documento);
        }

        [HttpPut("{idDocumento}")]
        public async Task<RetornoApi<DocumentoIdentificacaoClienteExibicaoModel>> Put(int idDocumento, [FromBody] DocumentoIdentificacaoClienteModel documentoIdentificacao)
        {
            var documento = await _documentoIdentificacaoClienteServico.AtualizarDocumento(idDocumento, documentoIdentificacao);

            return FormatarRetorno(documento);
        }

        [HttpDelete("{idDocumento}")]
        public async Task<RetornoApi<bool>> Delete(int idDocumento)
        {
            var documentoDeletado = await _documentoIdentificacaoClienteServico.RemoverDocumento(idDocumento);

            return FormatarRetorno(documentoDeletado);
        }
    }
}
