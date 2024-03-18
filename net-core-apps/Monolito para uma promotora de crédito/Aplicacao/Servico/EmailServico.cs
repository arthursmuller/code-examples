using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum.TemplateEmail;
using Infraestrutura;
using Infraestrutura.Fila.Email;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class EmailServico : ServicoBase, IEmailServico
    {
        private readonly IProducerEmail _producerEmail;
        private readonly ITemplateEmailServico _templateEmailServico;

        public EmailServico(IBemMensagens mensagens
                          , IUsuarioLogin usuarioLogin
                          , PlataformaClienteContexto contexto
                          , IProducerEmail producerEmail
                          , ITemplateEmailServico templateEmailServico) : base(mensagens, usuarioLogin, contexto)
        {
            _producerEmail = producerEmail;
            _templateEmailServico = templateEmailServico;
        }

        public async Task<bool> RegistrarEmail(TemplateEmailFinalidade finalidade, string titulo, string[] destinatarios, Dictionary<string, object> chaves = null, int? idUsuario = null)
        {
            var corpo = await _templateEmailServico.GerarTemplate(finalidade, chaves);

            if (String.IsNullOrWhiteSpace(corpo))
            {
                return false;
            }

            var registro = new RegistroEmailDominio(finalidade, destinatarios, idUsuario);
            await _contexto.RegistrosEmail.AddAsync(registro);
            await _contexto.SaveChangesAsync();

            await _producerEmail.Publicar(registro.CodigoReferenciaEmail, destinatarios, titulo, corpo);

            return true;
        }
    }
}
