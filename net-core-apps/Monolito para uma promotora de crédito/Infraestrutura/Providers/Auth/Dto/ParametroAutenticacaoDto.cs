using System.ComponentModel.DataAnnotations;

namespace Infraestrutura.Providers.Auth.Dto
{
    public class ParametroAutenticacaoDto
    {
        [Required(ErrorMessage = "Campo usuário deve ser informado!")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Campo senha deve ser informado!")]
        public string Senha { get; set; }
    }
}
