using System;
using System.Collections.Generic;
using Dominio.Enum;

namespace Dominio
{
    public class ProdutoDominio : EntidadeBase
    {
        public new Produto ID { get; private set; }
        public string Nome { get; private set; }
        public string Sigla { get; private set; }
        public bool RequerConvenio { get; private set; }

        public IEnumerable<IntencaoOperacaoSituacaoPassoDominio> PassosIntencaoOperacao { get; private set; } 

        public ProdutoDominio () {}

        public ProdutoDominio(Produto id, string nome, string sigla, bool requerConvenio)
        {
            ID = id;
            Nome = nome;
            Sigla = sigla;
            RequerConvenio = requerConvenio;
        }

        public void SetPropriedadesAtualizadas(string nome, string sigla, bool requerConvenio)
        {
            Nome = nome;
            Sigla = sigla;
            RequerConvenio = requerConvenio;
        }
    }
}
