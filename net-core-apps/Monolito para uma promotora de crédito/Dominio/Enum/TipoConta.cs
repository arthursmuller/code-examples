using System.ComponentModel;

namespace Dominio.Enum
{
    public enum TipoConta
    {
        [Description("N")]
        Normal = 1,
        [Description("P")]
        Poupanca = 2,
        [Description("C")]
        CartaoMagnetico = 3
    }
}
