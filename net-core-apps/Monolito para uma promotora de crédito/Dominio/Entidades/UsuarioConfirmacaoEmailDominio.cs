using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class UsuarioConfirmacaoEmailDominio : EntidadeBase
    {
        public string Token { get; private set; }

        public DateTime DataRequisicao { get; private set; }

        public int IdUsuario { get; private set; }

        public UsuarioDominio Usuario { get; private set; }

        public bool Expirado { get => !Valido || !(DataRequisicao.AddMinutes(35) > DateTime.Now); }

        public bool Valido { get; private set; }

        public UsuarioConfirmacaoEmailDominio() { }

        public UsuarioConfirmacaoEmailDominio(int idUsuario, string token)
        {
            Token = token;
            IdUsuario = idUsuario;
            Valido = true;
            DataRequisicao = DateTime.Now;
        }

        public void Invalidar()
        {
            Valido = false;
            DataAtualizacao = DateTime.Now;
        }
    }
}