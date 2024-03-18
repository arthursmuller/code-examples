using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Enum.TemplateEmail;

namespace Aplicacao.Servico
{
    public interface IEmailServico
    {
        Task<bool> RegistrarEmail(TemplateEmailFinalidade finalidade, string titulo, string[] destinatario, Dictionary<string, object> chaves = null, int? idUsuario = null);
    }
}