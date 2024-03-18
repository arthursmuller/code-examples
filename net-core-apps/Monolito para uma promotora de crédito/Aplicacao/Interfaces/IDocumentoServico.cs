using Aplicacao.Model.Documento;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IDocumentoServico
    {
        Task<DocumentoModel> GerarPdfTermoSeguro(
                UsuarioDominio usuario,
                SeguroPropostaDominio seguroProposta,
                SeguroPropostaIcatuDominio seguroPropostaIcatu,
                SeguroProdutoIcatuDominio seguroProdutoIcatu,
                SeguroClienteIcatuDominio seguroClienteIcatu,
                SeguroProfissaoIcatuDominio seguroProfissaoIcatu,
                SeguroCoberturaDominio seguroCoberturaCodigo2,
                SeguroCoberturaDominio seguroCoberturaCodigo24,
                SeguroCoberturaDominio seguroCoberturaCodigo26,
                SeguroClienteTelefoneDominio telefoneCliente,
                List<SeguroBeneficiarioDominio> seguroBeneficiarios,
                SeguroCobrancaPropostaCartaoIcatuDominio seguroCobrancaPropostaCartaoIcatu);
    }
}
