using System;
using B.Configuracao;
using Dominio.Resource;
using MassTransit;
using Microsoft.Extensions.Logging;
using SharedKernel.ValueObjects.v2;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Infraestrutura.Fila.Whatsapp;
using Fila.Model.WhatsApp;

namespace Infraestrutura.Fila.Sms
{
    [ExcludeFromCodeCoverage]
    public class ProducerWhatsapp : IProducerWhatsapp
    {
        private const string CONFIGURACAO_WHATSAPP_FORNECEDOR = "configuracao-whatsapp-idfornecedor";

        private readonly ILogger<ProducerWhatsapp> _logger;
        private readonly ISendEndpointProvider _provedorFila;
        private readonly Configuracao _configuracao;
        private readonly ConfiguracaoOrigem _origemConfiguracao;

        public ProducerWhatsapp(ILogger<ProducerWhatsapp> logger, ISendEndpointProvider provedorFila, Configuracao configuracao, ConfiguracaoOrigem origemConfiguracao)
        {
            _logger = logger;
            _provedorFila = provedorFila;
            _configuracao = configuracao;
            _origemConfiguracao = origemConfiguracao;

            string uriFilaWhatsApp = $"queue:ROBO-FILA-{_origemConfiguracao.Ambiente.ToUpper()}/WHATSAPP";

            EndpointConvention.Map<WhatsAppRequisicaoMensagem>(new Uri(uriFilaWhatsApp));
        }

        public async Task Publicar(string codigoReferenciaMensagem, Guid template, Fone telefone, Dictionary<string, string> mensagem)
        {
            try
            {
                var idFornecedorWhatsapp = Int32.Parse(_configuracao.BuscarParametro(CONFIGURACAO_WHATSAPP_FORNECEDOR));

                if (idFornecedorWhatsapp == 0)
                    throw new ArgumentNullException(string.Format(Mensagens.Configuracao_NaoEncontrada, CONFIGURACAO_WHATSAPP_FORNECEDOR));

                var requisicaoWhatsapp = new WhatsAppRequisicaoMensagem
                {
                    CodigoReferenciaMensagem = codigoReferenciaMensagem,
                    IdTemplate               = template,
                    DDD                      = telefone.DDD,
                    Telefone                 = telefone.Telefone,
                    Mensagem                 = mensagem,
                    IdWhatsAppFornecedor     = idFornecedorWhatsapp
                };

                await _provedorFila.Send<WhatsAppRequisicaoMensagem>(requisicaoWhatsapp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        private Fone montarTelefoneCompleto(Fone fone)
            => new Fone(fone.DDD, fone.Telefone);
    }
}
