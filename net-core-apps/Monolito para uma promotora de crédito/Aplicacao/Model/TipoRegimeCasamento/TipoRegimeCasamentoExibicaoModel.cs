using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.Model.TipoRegimeCasamento
{
    public class TipoRegimeCasamentoExibicaoModel
    {
        public int ID { get; set; }
        public string Descricao { get; set; }

        public TipoRegimeCasamentoExibicaoModel(int id, string descricao)
        {
            ID = id;
            Descricao = descricao;
        }
    }
}
