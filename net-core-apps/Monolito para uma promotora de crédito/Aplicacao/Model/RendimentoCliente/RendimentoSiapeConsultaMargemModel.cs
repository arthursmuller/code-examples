using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.RendimentoCliente
{
    [ExcludeFromCodeCoverage]
    public class RendimentoSiapeConsultaMargemModel
    {
        public bool PendenteInformacoesBanco { get; set; }
        public IEnumerable<RendimentoSiapeConsultaMargemItemModel> Items { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class RendimentoSiapeConsultaMargemItemModel
    {
        public bool PendenteInformacoesBanco { get; set; }
        public string Orgao { get; set; }
        public string Matricula { get; set; }
        public string Instituidor { get; set; }
        public decimal ValorMaximoParcela { get; set; }
        public bool EmprestimoAutorizado { get; set; }
        public bool PortabilidadeAutorizada { get; set; }
    }
}
