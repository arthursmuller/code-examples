using System;

namespace Dominio.Entidades
{
    public class EntidadeBase
    {
        public int ID { get; protected set; }

        public string UsuarioAtualizacao { get; set; } = "SISTEMA";

        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
