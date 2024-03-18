using System;
using System.Net.Mail;
using B.Mensagens.Interfaces;
using System.Threading.Tasks;
using Infraestrutura.Enum;
using System.Net;
using B.Mensagens;
using Infraestrutura.DTO.Email;
using System.Diagnostics.CodeAnalysis;

namespace Infraestrutura.Providers
{
    [ExcludeFromCodeCoverage]
    public class ProviderEmail : IProviderEmail
    {
        private readonly IBemMensagens _mensageria;

        public ProviderEmail(IBemMensagens mensageria)
        {
            _mensageria = mensageria;
        }

        public async Task<StatusEnvio> EnviarEmail(EmailMensagemDto requisicao)
        {
            if (String.IsNullOrEmpty(requisicao.Destinatario))
            {
                _mensageria.AdicionarErro("Não foi possível realizar o envio do email, os valores da requisição são inválidos.", EnumMensagemTipo.formulario);
                
                return StatusEnvio.Erro;
            }
            
            SmtpClient clienteSmtp = new SmtpClient(requisicao.Host, requisicao.Porta);
            clienteSmtp.EnableSsl = requisicao.Ssl;
            clienteSmtp.Credentials = new NetworkCredential(requisicao.Usuario, requisicao.Senha);

            MailAddress de = new MailAddress(requisicao.Usuario, requisicao.NomeExibicao, System.Text.Encoding.UTF8);
            MailAddress para = new MailAddress(requisicao.Destinatario ?? "");

            using (MailMessage mensagem = new MailMessage(de, para))
            {
                if (!String.IsNullOrWhiteSpace(requisicao.Copia)) {
                    MailAddress copias = new MailAddress(requisicao.Copia);
                    mensagem.CC.Add(copias);
                }

                mensagem.Body = requisicao.Corpo;
                mensagem.IsBodyHtml = !String.IsNullOrWhiteSpace(requisicao.Corpo) && requisicao.Corpo.Contains('<');
                mensagem.BodyEncoding =  System.Text.Encoding.UTF8;
                mensagem.Subject = requisicao.Assunto;
                mensagem.SubjectEncoding = System.Text.Encoding.UTF8;
                mensagem.Priority = requisicao.Prioritario ? MailPriority.High : MailPriority.Normal;

                try {
                    await clienteSmtp.SendMailAsync(mensagem);
                    return StatusEnvio.Sucesso;
                }
                catch (Exception e) {
                    _mensageria.AdicionarErro("Não foi possível realizar o envio do email, verifique as configurações da empresa ou a requisição.", EnumMensagemTipo.comunicacaoapi);
                    _mensageria.AdicionarErro(e, EnumMensagemTipo.comunicacaoapi);
                    
                    return StatusEnvio.Erro;
                }
            }
        }
    }
}