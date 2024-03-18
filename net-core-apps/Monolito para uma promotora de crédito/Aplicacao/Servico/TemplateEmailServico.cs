using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio.Resource;
using Dominio.Enum.TemplateEmail;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class TemplateEmailServico : ITemplateEmailServico
    {
        private readonly PlataformaClienteContexto _contexto;
        private readonly IBemMensagens _mensagens;
        private readonly TemplateBuilderServico _templateBuilder;

        public TemplateEmailServico(PlataformaClienteContexto contexto, IBemMensagens mensagens, TemplateBuilderServico templateBuilder)
        {
            _contexto = contexto;
            _mensagens = mensagens;
            _templateBuilder = templateBuilder;
        }

        public async Task<string> GerarTemplate(TemplateEmailFinalidade finalidade, Dictionary<string, object> chaves = null)
        {
            var templates = await _contexto.TemplatesEmail.Where(template => template.IdFinalidade == finalidade).ToListAsync();

            var content = templates.FirstOrDefault(t => t.IdTipo == TemplateEmailTipo.Content);

            if (content == null)
            {
                _mensagens.AdicionarErro(string.Format(Mensagens.TemplateEmail_Ausente, finalidade.ToString()), EnumMensagemTipo.banco);
                return null;
            }

            var header = templates.FirstOrDefault(t => t.IdTipo == TemplateEmailTipo.Header);
            if (header == null)
            {
                header = await _contexto.TemplatesEmail.FirstOrDefaultAsync(t => t.IdTipo == TemplateEmailTipo.Header && t.IdFinalidade == TemplateEmailFinalidade.Default);
            }

            var footer = templates.FirstOrDefault(t => t.IdTipo == TemplateEmailTipo.Header);
            if (footer == null)
            {
                footer = await _contexto.TemplatesEmail.FirstOrDefaultAsync(t => t.IdTipo == TemplateEmailTipo.Footer && t.IdFinalidade == TemplateEmailFinalidade.Default);
            }

            var chavesLayout = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["header"] = header?.Conteudo,
                ["content"] = content.Conteudo,
                ["footer"] = footer?.Conteudo,
            };

            var template = _templateBuilder.SubstituirChaves(gerarTemplateBase(), chavesLayout);

            template = _templateBuilder.SubstituirChaves(template, chaves);

            return template;
        }

        private string gerarTemplateBase() => $@"
            <!DOCTYPE html>
            <html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:o=""urn:schemas-microsoft-com:office:office"">
                <head>
                    <meta http-equiv=""Content-Type"" content=""text/html charset=UTF-8"" />
                    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                    <meta name=""format-detection"" content=""telephone=no"">
                    <meta name=""x-apple-disable-message-reformatting"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <style type=""text/css"">
                    #outlook a {{
                        padding: 0;
                    }}

                    .ExternalClass,
                    .ReadMsgBody {{
                        width: 100%;
                    }}

                    @media screen {{
                        @font-face {{
                        font-family: 'Roboto';
                        font-style: normal;
                        font-weight: normal;
                        src: local('Roboto'), url(""https://fonts.googleapis.com/css2?family=Roboto:wght@400;500;700;900&display=swap"");
                        }}
                    }}
                    </style>
                </head>
                <body>
                    <table width=""100%"" align=""center"" border=""0"" cellspacing=""0"" cellpadding=""0"" style=""margin:0px auto; max-width:600px;"">
                        <tr>
                            <td class=""container"" align=""center"" style=""background-color: #F5F5F5;"">

                                [[header]]

                                [[content]]

                                [[footer]]
                            
                            </td>
                        </tr>
                    </table>
                </body>
            </html>
        ";
    }
}
