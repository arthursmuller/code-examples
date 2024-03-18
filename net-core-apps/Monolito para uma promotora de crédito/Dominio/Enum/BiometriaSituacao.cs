using System.ComponentModel;

namespace Dominio.Enum
{
    public enum BiometriaSituacao
    {

        [Description("Não Realizado")]
        NaoRealizado = 0,

        [Description("Pendente")]
        Pendente = 1,

        [Description("Falha")]
        Falha = 2,

        [Description("Concluído")]
        Concluido = 3

    }
}
