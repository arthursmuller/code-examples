using Fila.Model.WhatsApp;
using B.Mensagens.Interfaces;
using B.WhatsApp;
using Dominio.Entidades;
using Infraestrutura;
using Aplicacao.Servico;
using System;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel.ValueObjects.v2;

namespace Aplicacao.Servico
{
    public class WhatsAppServico : ServicoBase
    {
        private readonly IProvedorWhatsApp _provedorWhatsApp;

        public WhatsAppServico(IBemMensagens mensagens
                             , PlataformaClienteContexto contexto
                             , IProvedorWhatsApp provedorWhatsApp) : base(mensagens, contexto)
            => _provedorWhatsApp = provedorWhatsApp;

        public async Task EnviarMensagem(WhatsAppRequisicaoMensagem requisicao)
        {
            var DDDBase = requisicao.DDD;
            var DDD = DDDBase.Substring(DDDBase.Length - 2, 2);
            var mensagemWhatsApp = 
                    new WhatsappMensagemDominio(requisicao.CodigoReferenciaMensagem
                                              , requisicao.IdTemplate
                                              , $"{DDD}{requisicao.Telefone}"
                                              , requisicao.Mensagem
                                              , requisicao.IdWhatsAppFornecedor);
            await _contexto.AddAsync(mensagemWhatsApp);
            await _contexto.SaveChangesAsync();

            Fone f = new Fone(requisicao.DDD, requisicao.Telefone); 
            if (!f.IsValid(_mensagens))
            {
                await registrarMensagemErro(mensagemWhatsApp, string.Join(";", _mensagens.BuscarErros()?.Select(s => s.Mensagem)));
                return;
            }

            try
            {
                await _provedorWhatsApp.Enviar( $"+55{DDD}{requisicao.Telefone}"
                                              , requisicao.IdTemplate
                                              , requisicao.Mensagem );
            }
            catch (Exception ex)
            {
                await registrarMensagemErro(mensagemWhatsApp, ex.Message);
            }
        }

        private async Task registrarMensagemErro(WhatsappMensagemDominio mensagemWhatsApp, string mensagem)
        {
            mensagemWhatsApp.RegistraMensagemRetornoErro(mensagem);
            await _contexto.SaveChangesAsync();
        }
    }
}
