using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum.TemplateWhatsapp;
using Infraestrutura;
using Infraestrutura.Fila.Whatsapp;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class WhatsappServico : ServicoBase, IWhatsappServico
    {
        private readonly IProducerWhatsapp _producerWhatsapp;
        private readonly ITemplateEmailServico _templateEmailServico;

        public WhatsappServico(IBemMensagens mensagens
                          , IUsuarioLogin usuarioLogin
                          , PlataformaClienteContexto contexto
                          , IProducerWhatsapp producerWhatsapp) : base(mensagens, usuarioLogin, contexto)
        {
            _producerWhatsapp = producerWhatsapp;
        }

        public async Task<bool>  RegistrarWhatsapp(TemplateWhatsapp template, int? codigoOrigem, Guid guid, Fone fone, Dictionary<string, string> valoresMensagem)
        {
            var registro = new RegistroWhatsappDominio(template
                                                      ,$"{fone.DDD}{fone.Telefone}"
                                                      ,valoresMensagem
                                                      ,_usuarioLogin.IdUsuario
                                                      ,codigoOrigem);

            await _contexto.RegistrosWhatsapp.AddAsync(registro);
            await _contexto.SaveChangesAsync();

            await _producerWhatsapp.Publicar(registro.CodigoReferenciaWhatsapp, guid, fone, valoresMensagem);

            return true;
        }
    }
}
