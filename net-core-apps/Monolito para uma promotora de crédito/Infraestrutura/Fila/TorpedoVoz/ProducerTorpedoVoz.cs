using B.Configuracao;
using Dominio.Resource;
using MassTransit;
using Microsoft.Extensions.Logging;
using SharedKernel.ValueObjects.v2;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Fila.Model.TorpedoVoz;

namespace Infraestrutura.Fila.TorpedoVoz
{
    [ExcludeFromCodeCoverage]
    public class ProducerTorpedoVoz : IProducerTorpedoVoz
    {
        private readonly ILogger<ProducerTorpedoVoz> _logger;
        private readonly ISendEndpointProvider _provedorFila;
        private readonly Configuracao _configuracao;
        private readonly ConfiguracaoOrigem _origemConfiguracao;
        
        public ProducerTorpedoVoz(ILogger<ProducerTorpedoVoz> logger, ISendEndpointProvider provedorFila, Configuracao configuracao, ConfiguracaoOrigem origemConfiguracao)
        {
            _logger = logger;
            _provedorFila = provedorFila;
            _configuracao = configuracao;
            _origemConfiguracao = origemConfiguracao;

            string uriFilaTorpedoVoz= $"queue:ROBO-FILA-{_origemConfiguracao.Ambiente.ToUpper()}/TorpedoVoz";

            EndpointConvention.Map<TorpedoVozRequisicaoMensagem>(new Uri(uriFilaTorpedoVoz));
        }

        public async Task Publicar(string codigoReferenciaMensagem, Fone telefone, string mensagem)
        {
            try
            {
                var requisicaoMensagemVoz = new TorpedoVozRequisicaoMensagem
                {
                    CodigoReferenciaMensagem = codigoReferenciaMensagem,
                    DDD      = telefone.DDD,
                    Telefone = telefone.Telefone,
                    Mensagem = mensagem,
                    IdTorpedoVozFornecedor = 1
                };

                await _provedorFila.Send<TorpedoVozRequisicaoMensagem>(requisicaoMensagemVoz);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

    }
}
