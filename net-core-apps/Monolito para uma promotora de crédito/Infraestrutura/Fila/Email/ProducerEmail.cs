using B.Configuracao;
using Fila.Model.Email;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Infraestrutura.Fila.Email
{
    [ExcludeFromCodeCoverage]
    public class ProducerEmail : IProducerEmail
    {
        private readonly ILogger<ProducerEmail> _logger;
        private readonly ISendEndpointProvider _provedorFila;
        private readonly Configuracao _configuracao;

        private readonly ConfiguracaoOrigem _origemConfiguracao;

        public ProducerEmail(ILogger<ProducerEmail> logger
                           , ISendEndpointProvider sendEndpointProvider
                           , Configuracao configuracao
                           , ConfiguracaoOrigem origemConfiguracao)
        {
            _logger = logger;
            _provedorFila = sendEndpointProvider;
            _configuracao = configuracao;
            _origemConfiguracao = origemConfiguracao;

            string uriFilaEmail = $"queue:ROBO-FILA-{_origemConfiguracao.Ambiente.ToUpper()}/Email";

            EndpointConvention.Map<EmailRequisicaoMensagem>(new Uri(uriFilaEmail));
        }

        public async Task Publicar(string codigoReferencia, string[] destinatarios, string assunto, string corpo)
        {
            try
            {
                var idFornecedorEmail = Int32.Parse(_configuracao.BuscarParametro("fornecedor-email-id"));

                if (idFornecedorEmail == 0)
                {
                    throw new ArgumentNullException("Configuração 'Fornecedor-email-id' inválido ou não encontrado");
                }

                var mensagem = new EmailRequisicaoMensagem
                {
                    CodigoReferenciaMensagem = codigoReferencia,
                    Destinatarios = destinatarios,
                    Mensagem = corpo,
                    Assunto = assunto,
                    IdEmailFornecedor = idFornecedorEmail,
                };

                await _provedorFila.Send<EmailRequisicaoMensagem>(mensagem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
