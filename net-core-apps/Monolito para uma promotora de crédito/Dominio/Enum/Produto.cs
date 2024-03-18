using System.ComponentModel;

namespace Dominio.Enum
{
    public enum Produto
    {
        [Description("SEG")]
        Seguro = 0,

        [Description("CC")]
        CreditoConsignado = 1,

        [Description("CCC")]
        CartaoCreditoConsignado = 2,

        [Description("FGTS")]
        AntecipaçãoFGTS = 4,
    }
}