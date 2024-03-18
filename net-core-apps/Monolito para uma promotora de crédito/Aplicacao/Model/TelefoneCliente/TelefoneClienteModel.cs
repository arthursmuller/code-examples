using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Model.TelefoneCliente
{
    public class TelefoneClienteModel
    {
        public int? Id { get; set; }

        [Required]
        public string DDD { get; set; }

        [Required]
        public string Fone { get; set; }

        public bool Principal { get; set; }

        public TelefoneClienteModel() { }
    }
}
