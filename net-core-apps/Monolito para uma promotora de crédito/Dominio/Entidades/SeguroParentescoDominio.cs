using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class SeguroParentescoDominio : EntidadeBase
    {
        public string Descricao { get; set; }
        public int Codigo { get; set; }

        public SeguroParentescoDominio() { }

        public SeguroParentescoDominio(string descricao, int codigo)
        {
            Descricao = descricao;
            Codigo = codigo;
        }
    }
}
