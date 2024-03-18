using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers.IcatuApi.Dto
{
    [ExcludeFromCodeCoverage]
    public class IcatuApiEndereco
    {
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public string Pais { get; set; }
    }

    public class IcatuApiCliente
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Documento { get; set; }
        public string Tipo { get; set; }
        public IcatuApiEndereco Endereco { get; set; }
    }

    public class IcatuApiItem
    {
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public int Valor { get; set; }
    }

    public class IcatuApiCartaoCredito
    {
        public int QuantidadeDeParcelas { get; set; }
        public string DescricaoFatura { get; set; }
        public bool CapturaTransacao { get; set; }
    }

    public class IcatuApiCheckout
    {
        public bool IgnoraPaginaSucesso { get; set; }
        public string PaginaSucesso { get; set; }
        public int TempoExpiracao { get; set; }
        public List<string> MetodosPagamentosAceitos { get; set; }
        public IcatuApiCartaoCredito CartaoCredito { get; set; }
    }

    public class Pagamento
    {
        public string TipoPagamento { get; set; }
        public int Valor { get; set; }
        public IcatuApiCartaoCredito CartaoCredito { get; set; }
        public IcatuApiCheckout Checkout { get; set; }
    }

    public class IcatuApiMetadado
    {
        public string Empresa { get; set; }
        public string CodigoProposta { get; set; }
        public string CodigoCobranca { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
    }

    public class CriarPedidoPagamentoDto
    {
        public IcatuApiCliente Cliente { get; set; }
        public List<IcatuApiItem> Itens { get; set; }
        public List<Pagamento> Pagamentos { get; set; }
        public IcatuApiMetadado Metadado { get; set; }
    }
}
