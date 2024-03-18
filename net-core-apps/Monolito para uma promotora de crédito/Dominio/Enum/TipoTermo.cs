using System.ComponentModel;

namespace Dominio.Enum
{
    public enum TipoTermo
    {
        [Description("Criação de Usuário")]
        CriacaoUsuario = 1,
        [Description("Aceite de Importação de Dados")]
        ImportacaoDados = 2
    }
}
