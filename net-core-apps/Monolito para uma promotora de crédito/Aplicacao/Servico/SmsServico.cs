using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum.TemplateEmail;
using Dominio.Enum.TemplateSms;
using Infraestrutura;
using Infraestrutura.Fila.Sms;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class SmsServico : ServicoBase, ISmsServico
    {
        private readonly IProducerSms _producerSms;
        public SmsServico(  IBemMensagens mensagens
                          , IUsuarioLogin usuarioLogin
                          , PlataformaClienteContexto contexto
                          , IProducerSms producerSms) : base(mensagens, usuarioLogin, contexto)
        {
            _producerSms = producerSms;
        }

        public async Task<bool> RegistrarSms(TemplateSms templateSms, int? codigoOrigem, Fone fone, string mensagem)
        {
            var registro = new RegistroSmsDominio( templateSms
                                                 , $"{fone.DDD}{fone.Telefone}"
                                                 , mensagem 
                                                 , _usuarioLogin.IdUsuario
                                                 , codigoOrigem );

            await _contexto.RegistrosSms.AddAsync(registro);
            await _contexto.SaveChangesAsync();

            await _producerSms.Publicar( registro.CodigoReferenciaSms
                                       , fone
                                       , mensagem );
            
            return true;
        }
    }
}
