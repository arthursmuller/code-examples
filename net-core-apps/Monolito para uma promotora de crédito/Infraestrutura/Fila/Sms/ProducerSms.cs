using B.Configuracao;
using Dominio.Resource;
using Fila.Model.Sms;
using MassTransit;
using Microsoft.Extensions.Logging;
using SharedKernel.ValueObjects.v2;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Infraestrutura.Fila.Sms
{
    [ExcludeFromCodeCoverage]
    public class ProducerSms : IProducerSms
    {
        private const string CONFIGURACAO_SMS_FORNECEDOR = "configuracao-sms-idfornecedor";

        private readonly ILogger<ProducerSms> _logger;
        private readonly ISendEndpointProvider _provedorFila;
        private readonly Configuracao _configuracao;
        private readonly ConfiguracaoOrigem _origemConfiguracao;

        public ProducerSms(ILogger<ProducerSms> logger, ISendEndpointProvider provedorFila, Configuracao configuracao, ConfiguracaoOrigem origemConfiguracao)
        {
            _logger = logger;
            _provedorFila = provedorFila;
            _configuracao = configuracao;
            _origemConfiguracao = origemConfiguracao;

            string uriFilaSMS = $"queue:ROBO-FILA-{_origemConfiguracao.Ambiente.ToUpper()}/SMS";

            EndpointConvention.Map<SmsRequisicaoMensagem>(new Uri(uriFilaSMS));
        }

        public async Task Publicar(string codigoReferenciaMensagem, Fone telefone, string mensagem)
        {
            try
            {
                var idFornecedorSms = Int32.Parse(_configuracao.BuscarParametro(CONFIGURACAO_SMS_FORNECEDOR));

                if (idFornecedorSms == 0)
                    throw new ArgumentNullException(string.Format(Mensagens.Configuracao_NaoEncontrada, CONFIGURACAO_SMS_FORNECEDOR));

                var requisicaoSms = new SmsRequisicaoMensagem
                {
                    IdSmsFornecedor = idFornecedorSms,
                    CodigoReferenciaMensagem = codigoReferenciaMensagem,
                    DDD = telefone.DDD,
                    Telefone = telefone.Telefone,
                    Mensagem = mensagem
                };

                await _provedorFila.Send<SmsRequisicaoMensagem>(requisicaoSms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }


    }
}
