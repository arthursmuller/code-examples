using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Infraestrutura.Providers.IcatuApi.Dto
{
    [ExcludeFromCodeCoverage]

    public class EnderecoCobranca
    {
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Pais { get; set; }
    }
    public class CartaoCredito
    {
        public string Identificador { get; set; }
        public string NomeCliente { get; set; }
        public string DocumentoTitular { get; set; }
        public string PrimeirosSeisDigitos { get; set; }
        public string UltimosQuatroDigitos { get; set; }

        public EnderecoCobranca EnderecoCobranca { get; set; }
    }

    public class RespostaGateway
    {
        public string Codigo { get; set; }
        public List<object> Erros { get; set; }
    }

    public class PedidoSeguroMetadado
    {
        public string Empresa { get; set; }
        public string CodigoProposta { get; set; }
        public string CodigoCobranca { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public string IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
    }

    public class Transacao
    {
        public string ChaveOperacao { get; set; }
        public string Identificador { get; set; }
        public string TipoTransacao { get; set; }
        public int Valor { get; set; }
        public string Status { get; set; }
        public bool Sucesso { get; set; }
        public int Parcelas { get; set; }
        public string DescricaoFatura { get; set; }
        public string NsuAdquirente { get; set; }
        public CartaoCredito CartaoCredito { get; set; }
        public string DataCriacao { get; set; }
        public string DataAlteracao { get; set; }
        public RespostaGateway RespostaGateway { get; set; }
        public PedidoSeguroMetadado Metadado { get; set; }
    }

    public class Cobranca
    {
        public string Identificador { get; set; }
        public string Codigo { get; set; }
        public int Valor { get; set; }
        public string Status { get; set; }
        public string MetodoPagamento { get; set; }
        public string DataCriacao { get; set; }
        public string DataAlteracao { get; set; }
        public Transacao Transacao { get; set; }
    }

    public class PedidoSeguroCheckout
    {
        public string Identificador { get; set; }
        public int Valor { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
        public string DataCriacao { get; set; }
        public string DataAlteracao { get; set; }
        public string DataCancelamento { get; set; }
        public string DataExpiracao { get; set; }
        public PedidoSeguroMetadado Metadado { get; set; }
    }

    public class ConsultarPedidoPagamentoDto
    {
        public string Identificador { get; set; }
        public string Codigo { get; set; }
        public int Valor { get; set; }
        public bool Fechado { get; set; }
        public string Status { get; set; }
        public string DataCriacao { get; set; }
        public string DataAlteracao { get; set; }
        public List<Cobranca> Cobrancas { get; set; }
        public List<PedidoSeguroCheckout> Checkouts { get; set; }
        public PedidoSeguroMetadado Metadado { get; set; }
    }
}
