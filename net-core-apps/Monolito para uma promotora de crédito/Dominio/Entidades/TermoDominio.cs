using Dominio.Enum;
using System;
using System.Collections.Generic;

namespace Dominio
{
    public class TermoDominio : EntidadeBase
    {
        public TipoTermo IdTipoTermo { get; private set; }

        public string Nome { get; private set; }

        public string Descricao { get; private set; }

        public DateTime DataInicioVigencia { get; private set; }

        public TipoTermoDominio TipoTermo { get; private set; }

        public IEnumerable<UsuarioTermoDominio> UsuariosTermos { get; private set; }

        public TermoDominio(TipoTermo idTipoTermo, string nome, string descricao, DateTime dataInicioVigencia)
        {
            IdTipoTermo = idTipoTermo;
            Nome = nome;
            Descricao = descricao;
            DataInicioVigencia = dataInicioVigencia;
        }
    }
}
