using Aplicacao.Model.Notificacoes;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum.Notificacoes;
using Dominio.Resource;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class NotificacaoServico : ServicoBase, INotificacaoServico
    {
        private readonly TemplateBuilderServico _templateBuilder;

        public NotificacaoServico(
            IBemMensagens mensagens,
            IUsuarioLogin usuarioLogin,
            PlataformaClienteContexto contexto,
            TemplateBuilderServico templateBuilder
        ) : base(mensagens, usuarioLogin, contexto)
            => _templateBuilder = templateBuilder;

        public async Task<IEnumerable<NotificacaoModel>> BuscarNotificacoesAutenticado()
        {
            var notificacoes = await _contexto.Notificacoes
                                        .Where(n => n.IdUsuario.Equals(_usuarioLogin.IdUsuario) && !n.Completo)
                                        .OrderByDescending(n => n.DataAtualizacao)
                                        .AsNoTracking()
                                        .ToListAsync();

            return notificacoes.Select(notificacao => converterParaModel(notificacao));
        }

        public async Task<NotificacaoModel> MarcarVisualizacao(int idNotificacao)
        {
            var notificacao = await _contexto.Notificacoes
                .FirstOrDefaultAsync(x => x.ID == idNotificacao);

            if (notificacao == null)
            {
                _mensagens.AdicionarErro(string.Format(Mensagens.Notificacao_NaoEncontrada, idNotificacao), EnumMensagemTipo.banco);
                return null;
            }

            notificacao.RegistarVisualizacao();

            await _contexto.SaveChangesAsync();

            return converterParaModel(notificacao);
        }

        public async Task<NotificacaoModel> CompletarNotificacao(int? idNotificacao, NotificacaoFinalidade? finalidade = null)
        {
            var notificacao = await _contexto.Notificacoes
                .OrderByDescending(n => n.DataCriacao)
                .FirstOrDefaultAsync(n => 
                    (!idNotificacao.HasValue || n.ID == idNotificacao) 
                    && (!finalidade.HasValue || n.IdNotificacaoFinalidade.Equals(finalidade))
                );

            if (notificacao == null)
            {
                var referencia = idNotificacao.HasValue ? idNotificacao.Value.ToString() : finalidade.ToString();
                _mensagens.AdicionarAlerta(string.Format(Mensagens.Notificacao_NaoEncontrada, referencia), EnumMensagemTipo.banco);
                return null;
            }

            notificacao.RegistrarCompletude();

            await _contexto.SaveChangesAsync();

            return converterParaModel(notificacao);
        }

        public async Task<NotificacaoDominio> GerarNotificacao(int idUsuario, NotificacaoFinalidade finalidade, Dictionary<string, object> chaves = null)
        {
            var template = await _contexto.TemplatesNotificacao
                                .AsNoTracking()
                                .FirstOrDefaultAsync(template => template.IdNotificacaoFinalidade == finalidade);

            if (template == null)
            {
                _mensagens.AdicionarAlerta(string.Format(Mensagens.TemplateNotificacao_Ausente, finalidade.ToString()), EnumMensagemTipo.banco);
                return null;
            }

            var notificacao = new NotificacaoDominio(
                idUsuario,
                template.ID,
                _templateBuilder.SubstituirChaves(template.Titulo, chaves),
                _templateBuilder.SubstituirChaves(template.Descricao, chaves),
                _templateBuilder.SubstituirChaves(template.UrlReferencia, chaves),
                template.IdNotificacaoSeveridade,
                template.IdNotificacaoFinalidade
            );

            await _contexto.AddAsync(notificacao);

            await _contexto.SaveChangesAsync();

            return notificacao;
        }

        private NotificacaoModel converterParaModel(NotificacaoDominio notificacao) => new NotificacaoModel()
        {
            Id = notificacao.ID,
            Titulo = notificacao.Titulo,
            Descricao = notificacao.Descricao,
            UrlReferencia = notificacao.UrlReferencia,
            DataNotificacao = notificacao.DataAtualizacao,
            DataVisualizacao = notificacao.DataVisualizacao,
            Severidade = notificacao.IdNotificacaoSeveridade,
        };
    }
}
