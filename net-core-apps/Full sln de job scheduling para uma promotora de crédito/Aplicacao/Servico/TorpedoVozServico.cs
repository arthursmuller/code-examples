using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio.Entidades;
using Fila.Model.TorpedoVoz;
using Infraestrutura;
using Infraestrutura.DTO.ZenviaTorpedoVoz;
using Infraestrutura.Enum;
using Infraestrutura.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class TorpedoVozServico : ServicoBase
    {
        private readonly IProviderZenviaTotalVoice _providerZenviaTotalVoice;

        public TorpedoVozServico(
            IBemMensagens mensagens,
            PlataformaClienteContexto contexto,
            IProviderZenviaTotalVoice providerZenviaTotalVoice) : base(mensagens, contexto) 
        {
            _providerZenviaTotalVoice = providerZenviaTotalVoice;
        }

        public async Task ProcessarRequisicao(TorpedoVozRequisicaoMensagem requisicao)
        {

            var fornecedor = _contexto.TorpedoVozFornecedores.Where( x => x.ID.Equals(requisicao.IdTorpedoVozFornecedor) ).FirstOrDefault();


            var DDDBase = requisicao.DDD;
            var DDD = DDDBase.Substring(DDDBase.Length - 2, 2);

            var mensagemVoz = new TorpedoVozMensagemDominio(
                requisicao.CodigoReferenciaMensagem,
                $"{DDD}{requisicao.Telefone}",
                requisicao.Mensagem,
                requisicao.IdTorpedoVozFornecedor,
                DateTime.Now
            );
            _contexto.TorpedoVozMensagens.Add(mensagemVoz);
            ZenviaTorpedoVozDto mensagemZenvia = criarMensagem(mensagemVoz);

            var status = await _providerZenviaTotalVoice.EnviarMensagemVoz(mensagemZenvia, fornecedor.ChaveEnvio);

            mensagemVoz.InformarSituacaoEnvio(status);
            
            await _contexto.SaveChangesAsync();

        }

        private ZenviaTorpedoVozDto criarMensagem(TorpedoVozMensagemDominio mensagem)
        {
            return new ZenviaTorpedoVozDto()
            {
                NumeroDestino = $"55{mensagem.NumeroTelefone}",
                Mensagem = mensagem.Mensagem
            };
        }
    }
}
