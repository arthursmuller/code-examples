using Aplicacao.Model.Cliente;
using Newtonsoft.Json;

namespace Aplicacao
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        [JsonIgnore]
        public bool Administrador { get; set; }

        public LojaModel Loja { get; set; }

        public ClienteExibicaoModel Cliente { get; set; }
    }
}
