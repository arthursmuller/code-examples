using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio.Entidades;
using Fila.Model.Email;
using Infraestrutura;
using Infraestrutura.DTO.Email;
using Infraestrutura.Enum;
using Infraestrutura.Providers;
using System;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class EmailServico : ServicoBase
    {
        private readonly IProviderEmail _providerEmail;

        public EmailServico(
            IBemMensagens mensagens,
            PlataformaClienteContexto contexto,
            IProviderEmail providerEmail) : base(mensagens, contexto) 
        {
            _providerEmail = providerEmail;
        }

        public async Task ProcessarRequisicao(EmailRequisicaoMensagem requisicao)
        {
            var emailFornecedor = await _contexto.EmailFornecedores.FindAsync(requisicao.IdEmailFornecedor);
            
            if (emailFornecedor == null)
            {
                _mensagens.AdicionarErro("ID de email de fornecedor não encontrado.", EnumMensagemTipo.formulario);
                return;
            }

            var emailMensagem = new EmailMensagemDominio(
                requisicao.CodigoReferenciaMensagem,
                requisicao.Destinario,
                requisicao.Copia,
                requisicao.Assunto,
                requisicao.Mensagem,
                requisicao.Prioritario,
                DateTime.Now,
                requisicao.IdEmailFornecedor
            );

            EmailMensagemDto mensagem = criarMensagem(emailMensagem, emailFornecedor);

            StatusEnvio resposta = await _providerEmail.EnviarEmail(mensagem);

            _contexto.EmailMensagens.Add(emailMensagem);

            if (resposta == StatusEnvio.Sucesso)
            {
                emailMensagem.RegistrarEnvio();
            }

            await _contexto.SaveChangesAsync();
        }

        private EmailMensagemDto criarMensagem(EmailMensagemDominio mensagem, EmailFornecedorDominio email)
        {
            return new EmailMensagemDto()
            {
                Destinatario = mensagem.Destinatario,
                Copia = mensagem.Copia,
                Assunto = mensagem.Assunto,
                Prioritario = mensagem.Prioritario,
                Corpo = mensagem.Mensagem,
                Porta = email.Porta,
                Host = email.Host,
                Senha = email.Senha,
                Usuario = email.Usuario,
                NomeExibicao = email.NomeExibicao,
                Ssl = email.Ssl,
            };
        }

    }
}
