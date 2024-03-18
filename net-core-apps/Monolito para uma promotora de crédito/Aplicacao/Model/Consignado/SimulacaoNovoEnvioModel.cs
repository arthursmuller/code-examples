using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Model.Consignado
{
    public class SimulacaoNovoEnvioModel
    {
        [Required(ErrorMessage = "Convênio é obrigatório.")]
        public int IdConvenio { get; set; }

        public decimal? ValorOperacao { get; set; }

        public decimal? ValorPrestacao { get; set; }

        public bool RetornarSomenteOperacoesViaveis { get; set; }
    }
}
