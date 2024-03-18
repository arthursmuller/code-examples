using System;

namespace Dominio
{
    public abstract class EntidadeBase
    {
        public int ID { get; protected set; }

        public string UsuarioAtualizacao { get; set; } = "SISTEMA";

        public DateTime DataAtualizacao { get; set; } = DateTime.Now;

        protected void setDataAtualizacao()
            => DataAtualizacao = DateTime.Now;
    }
}
