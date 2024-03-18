using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio.Entidades;
using Fila.Model.Sms;
using Infraestrutura;
using Infraestrutura.DTO.Zenvia;
using Infraestrutura.Enum;
using Infraestrutura.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class SmsServico : ServicoBase
    {
        private readonly IProviderZenvia _providerZenvia;

        public SmsServico(
            IBemMensagens mensagens,
            PlataformaClienteContexto contexto,
            IProviderZenvia providerZenvia) : base(mensagens, contexto) 
        {
            _providerZenvia = providerZenvia;
        }

        public async Task ProcessarRequisicao(SmsRequisicaoMensagem requisicao)
        {
            var smsFornecedor = await _contexto.SmsFornecedores.FindAsync(requisicao.IdSmsFornecedor);

            if (smsFornecedor == null)
            {
                _mensagens.AdicionarErro("ID de SMS de fornecedor não encontrado.", EnumMensagemTipo.formulario);
                return;
            }

            var DDDBase = requisicao.DDD;
            var DDD = DDDBase.Substring(DDDBase.Length - 2, 2);

            var smsMensagem = new SmsMensagemDominio(
                requisicao.CodigoReferenciaMensagem,
                $"{DDD}{requisicao.Telefone}",
                requisicao.Mensagem,
                DateTime.Now,
                requisicao.IdSmsFornecedor
            );
            
            _contexto.SmsMensagens.Add(smsMensagem);

            ZenviaSmsMensagemDto mensagemZenvia = criarMensagem(smsMensagem, smsFornecedor);

            var (status, zenviaStatus, zenviaStatusDetalhes) = await _providerZenvia.EnviarMensagem(mensagemZenvia, smsFornecedor.CredenciaisBase64);

            if (status == StatusEnvio.Sucesso && zenviaStatus == ZenviaStatus.Ok)
            {
                smsMensagem.RegistrarEnvio((int)zenviaStatus, (int)zenviaStatusDetalhes);
            } else {
                smsMensagem.AtualizarSituacaoEnvio((int)zenviaStatus, (int)zenviaStatusDetalhes);
            }

            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarRequisicoesEmAberto()
        {
            var mensagemsEmAberto = await _contexto.SmsMensagens
                .Where(sms => !sms.Processado && sms.DataEnvio.HasValue)
                .Include(sms => sms.SmsFornecedor)
                .ToListAsync();

            var tasks = mensagemsEmAberto.Select(sms => ConsultarStatus(sms)).ToArray();

            Task.WaitAll(tasks);
        }

        public async Task ConsultarStatus(SmsMensagemDominio mensagem)
        {
            var (status, zenviaStatus, zenviaStatusDetalhes, operadora) = await _providerZenvia.ConsultarRequisicao(mensagem.CodigoReferenciaMensagem, mensagem.SmsFornecedor.CredenciaisBase64);

            if (status == StatusEnvio.Sucesso)
            {
                if ((int)zenviaStatus != mensagem.IdSituacaoEnvio || (int)zenviaStatusDetalhes != mensagem.IdSituacaoEnvioDetalhes)
                {
                    if (string.IsNullOrWhiteSpace(mensagem.Operadora))
                    {
                        mensagem.AtualizarOperadora(operadora);
                    }

                    if (zenviaStatus == ZenviaStatus.Delivered)
                    {
                        mensagem.RegistrarRecebimento((int)zenviaStatus, (int)zenviaStatusDetalhes);
                    }
                    else
                    {
                        var processado = zenviaStatus != ZenviaStatus.Scheduled && zenviaStatus != ZenviaStatus.Ok && zenviaStatus != ZenviaStatus.Sent;

                        mensagem.AtualizarSituacaoEnvio((int)zenviaStatus, (int)zenviaStatusDetalhes, processado);
                    }

                    await _contexto.SaveChangesAsync();
                }
            }
        }

        private ZenviaSmsMensagemDto criarMensagem(SmsMensagemDominio mensagem, SmsFornecedorDominio conta)
        {
            return new ZenviaSmsMensagemDto()
            {
                Id = mensagem.CodigoReferenciaMensagem,
                To = $"55{mensagem.NumeroTelefone}",
                Msg = mensagem.Mensagem,
                From = conta.NomeExibicao,
                AggregateId = conta.CodigoAgrupador,
            };
        }
    }
}
