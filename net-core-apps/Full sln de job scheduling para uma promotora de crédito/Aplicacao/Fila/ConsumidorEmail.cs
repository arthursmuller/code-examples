using System.Threading.Tasks;
using Fila.Model.Email;
using Serilog;
using Aplicacao.Servico;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Fila
{
    [ExcludeFromCodeCoverage]
    public class ConsumidorEmail
    {
        private readonly EmailServico _emailServico;
        private readonly ILogger _logger;

        public ConsumidorEmail(EmailServico emailServico, ILogger logger)
        {
            _emailServico = emailServico;
            _logger = logger;
        }

        public async Task Processar(EmailRequisicaoMensagem mensagem)
        {
            try{
                await _emailServico.ProcessarRequisicao(mensagem);
            }
            catch(Exception ex){
                _logger.Error($"Erro ao processar requisição de Email {mensagem.CodigoReferenciaMensagem}", ex.Message, ex.StackTrace);
            }
        }

        public async Task ProcessarErro(EmailRequisicaoMensagem mensagem)
        {
            _logger.Error("Erro ao processar requisição de email", mensagem);
        }
    }
}