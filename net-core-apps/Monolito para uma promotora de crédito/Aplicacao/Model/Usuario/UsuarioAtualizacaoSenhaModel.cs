using System.ComponentModel.DataAnnotations;

namespace Aplicacao
{
    public class UsuarioAtualizacaoSenhaModel
    {
        [Required]
        public string SenhaAtual { get; set; }
        [Required]
        public string NovaSenha { get; set; }
    }
}
