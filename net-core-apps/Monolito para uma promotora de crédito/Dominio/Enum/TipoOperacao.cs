using System.ComponentModel;

namespace Dominio.Enum
{
    public enum TipoOperacao
    {
        [Description("N")]
        Novo = 1,

        [Description("R")]
        Refinanciamento = 2,

        [Description("P")]
        Portabilidade = 3,
    }
}