using Aplicacao.Model.Banco;
using Aplicacao.Model.SeguroBeneficiario;
using Aplicacao.Model.SeguroEndereco;
using Aplicacao.Model.SeguroTelefoneCliente;
using System.Collections.Generic;

namespace Aplicacao.Model.SeguroProposta
{
    public class CriarSeguroPropostaModel
    {
        public int IdSeguroProduto { get; set; }
        public int IdMeioPagamento { get; set; }
        public decimal RendaMensal { get; set; }
        public char PPE { get; set; }
        public bool RelacionamentoEletronico { get; set; }
        public bool Aposentado { get; set; }
        public int? IdEnderecoCliente { get; set; }
        public ContaBancariaModel ContaBancariaModel { get; set; }
        public SeguroEnderecoModel EnderecoCobranca { get; set; }
        public List<SeguroBeneficiarioModel> Beneficiarios { get; set; }
        public List<SeguroClienteTelefoneModel> Telefones { get; set; }       
    }

}
