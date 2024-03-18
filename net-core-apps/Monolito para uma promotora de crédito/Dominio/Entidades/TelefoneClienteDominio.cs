using SharedKernel.ValueObjects.v2;
using System;

namespace Dominio
{
    public class TelefoneClienteDominio : EntidadeBase
    {
        public string DDD { get; private set; }
        public string Fone { get; private set; }
        public bool Confirmado { get; private set; }
        public bool Deletado { get; private set; }
        public int IdCliente { get; private set; }
        public ClienteDominio Cliente { get; private set; }

        public TelefoneClienteDominio() { }

        public TelefoneClienteDominio(int idCliente, Fone fone)
        {
            IdCliente = idCliente;
            DDD = fone.DDD;
            Fone = fone.Telefone;
            Deletado = false;
        }

        public void SetAtualizarTelefone(Fone fone)
        {
            DDD = fone.DDD;
            Fone = fone.Telefone;
            Deletado = false;

            atualizarData();
        }


        public void AlternarConfirmado(bool confirmado)
        {
            Confirmado = confirmado;
            atualizarData();
        }

        public void AlternarDeletado(bool deletado)
        {
            Deletado = deletado;
            atualizarData();
        }

        private void atualizarData()
        {
            DataAtualizacao = DateTime.Now;
        }
    }
}
