using Fila.Model.TorpedoVoz;
using Serilog;
using Aplicacao.Servico;
using System.Threading.Tasks;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Fila
{
    public class ConsumidorTorpedoVoz
    {
        private readonly TorpedoVozServico _torpedoVozServico;
        private readonly ILogger _logger;

        [ExcludeFromCodeCoverage]
        public ConsumidorTorpedoVoz(TorpedoVozServico torpedoVozServico, ILogger logger)
        {
            _torpedoVozServico = torpedoVozServico;
            _logger = logger;
        }

        public async Task Processar(TorpedoVozRequisicaoMensagem mensagem){
            try{
                await _torpedoVozServico.ProcessarRequisicao(mensagem);
            }
            catch(Exception ex){
                _logger.Error($"Erro ao processar requisição de Torpedo de Voz {mensagem.CodigoReferenciaMensagem}", ex.Message, ex.StackTrace);
            }
        }

        public async Task ProcessarErro(TorpedoVozRequisicaoMensagem mensagem)
            => _logger.Error("Erro ao processar requisição de envio whatsapp", mensagem);
    }
}
