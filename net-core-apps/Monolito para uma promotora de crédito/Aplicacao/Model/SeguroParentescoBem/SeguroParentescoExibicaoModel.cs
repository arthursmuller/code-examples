﻿namespace Aplicacao.Model.SeguroParentescoBem
{
    public class SeguroParentescoExibicaoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Codigo { get; set; }

        public SeguroParentescoExibicaoModel(int id, string descricao, int codigo)
        {
            Id = id;
            Descricao = descricao;
            Codigo = codigo;
        }
    }
}
