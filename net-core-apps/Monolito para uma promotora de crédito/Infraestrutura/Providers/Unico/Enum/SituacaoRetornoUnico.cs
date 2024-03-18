using System.ComponentModel;
namespace Infraestrutura.Providers.Unico
{
    public enum SituacaoRetornoUnico
    {

        [Description("Aguardando documentos ou aguardando execução ou em processamento")]
        AguardandoExecucao = 1,

        [Description("Em divergência")]
        EmDivergencia = 2,

        [Description("Concluido")]
        Concluido = 3,

        [Description("Cancelado")]
        Cancelado = 4,

        [Description("Erro")]
        Erro = 5,

        [Description("Enviando mensagem")]
        EnviandoMensagem = 6,

        [Description("Aguardando captura da mensagem")]
        AugardandoCapturaMensagem = 7,

        [Description("Reenviando mensagem")]
        ReenviandoMensagem = 8

    }
}