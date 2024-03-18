using Aplicacao.Model.AcessoDadosPessoais;
using AutoMapper;
using B.Configuracao;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum.TemplateEmail;
using Infraestrutura;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class AcessoDadosPessoaisServico
    {
        private readonly IBemMensagens _mensagens;
        private readonly PlataformaClienteContexto _contexto;
        private readonly IEmailServico _emailServico;
        private readonly Configuracao _configuracao;

        public AcessoDadosPessoaisServico(IBemMensagens mensagens, PlataformaClienteContexto contexto, IEmailServico emailServico, Configuracao configuracao)
        {
            _mensagens = mensagens;
            _contexto = contexto;
            _emailServico = emailServico;
            _configuracao = configuracao;
        }

        public async Task<SolicitacaoAcessoDadosPessoaisModel> CriarSolicitacaoDeAcesso(SolicitacaoAcessoDadosPessoaisEnvioModel model)
        {
            var solicitacao = new SolicitacaoAcessoDadosPessoaisClienteDominio(
                model.Nome,
                model.Sobrenome,
                model.DataNascimento,
                model.NomeMae,
                model.Motivo);

            var telefone = obterTelefoneCompletoValidado(model);

            if (_mensagens.PossuiErros)
                return null;

            solicitacao.InformarAtualizarDadosContato(model.Email, telefone.DDD, telefone.Telefone);

            await _contexto.SolicitacoesAcessoDadosPessoaisClientes.AddAsync(solicitacao);
            await _contexto.SaveChangesAsync();

            await enviarNotificacao(solicitacao);

            return Mapper.Map<SolicitacaoAcessoDadosPessoaisModel>(solicitacao);
        }

        private Fone obterTelefoneCompletoValidado(SolicitacaoAcessoDadosPessoaisEnvioModel model)
        {
            var telefone = new Fone(model.TelefoneCompleto.DDD, model.TelefoneCompleto.Telefone);
            telefone.IsValid(_mensagens);

            return telefone;
        }

        private async Task enviarNotificacao(SolicitacaoAcessoDadosPessoaisClienteDominio solicitacao)
        {
            var emailSolicitacaoAcesso = _configuracao.BuscarParametro("email_solicitacao_dados_pessoais");

            var chavesLayout = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["solicitacao"] = new {
                    TelefoneCompleto = Regex.Replace(solicitacao.TelefoneCompleto, @"(\w{2})(\w{4})(\w{5}|\w{4})", @"($1) $2 $3"),
                    DataNascimento = solicitacao.DataNascimento.ToString("dd/MM/yyyy"),
                    Email = solicitacao.Email,
                    Motivo = solicitacao.Motivo,
                    NomeMae = solicitacao.NomeMae,
                    Sobrenome = solicitacao.Sobrenome,
                    Nome = solicitacao.Nome
                },
            };

            var resultadoEnvioNotificacao = await _emailServico.RegistrarEmail(
                TemplateEmailFinalidade.SolicitacaoAcessoDadosPessoais,
                $"Solicitação de Acesso a Dados Pessoais ##{solicitacao.ID} - {solicitacao.Nome}",
                new string[] { emailSolicitacaoAcesso },
                chavesLayout
            );

            await registrarResultadoEnvioNotificacao(solicitacao, resultadoEnvioNotificacao);
        }

        private async Task registrarResultadoEnvioNotificacao(SolicitacaoAcessoDadosPessoaisClienteDominio solicitacao, bool notificacaoEnviada)
        {
            solicitacao.AlternarSituacaoEnvioNotificacao(notificacaoEnviada);
            await _contexto.SaveChangesAsync();
        }
    }
}
