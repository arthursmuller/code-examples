using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum.TemplateTorpedoVoz;
using Infraestrutura;
using Infraestrutura.Fila.Email;
using Infraestrutura.Fila.TorpedoVoz;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class TorpedoVozServico : ServicoBase, ITorpedoVozServico
    {
        private readonly IProducerTorpedoVoz _producerTorpedoVoz;
        public TorpedoVozServico( IBemMensagens mensagens
                                , IUsuarioLogin usuarioLogin
                                , PlataformaClienteContexto contexto
                                , IProducerTorpedoVoz producerTorpedoVoz ) : base(mensagens, usuarioLogin, contexto)
        {
            _producerTorpedoVoz = producerTorpedoVoz;
        }

        public async Task<bool>  RegistrarTorpedoVoz(TemplateTorpedoVoz template, int? codigoOrigem, Fone fone, string mensagem)
        {

            var registro = new RegistroTorpedoVozDominio(template
                                                        ,$"{fone.DDD}{fone.Telefone}"
                                                        ,mensagem
                                                        ,_usuarioLogin.IdUsuario
                                                        ,codigoOrigem );

            await _contexto.RegistrosTorpedoVoz.AddAsync(registro);
            await _contexto.SaveChangesAsync();

            await _producerTorpedoVoz.Publicar(registro.CodigoReferenciaTorpedoVoz
                                              ,fone
                                              ,mensagem);

            return true;
        }
    }
}
