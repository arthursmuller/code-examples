using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Enum.TemplateEmail;

namespace Aplicacao.Servico
{
    public interface ITemplateEmailServico
    {
       Task<string> GerarTemplate(TemplateEmailFinalidade finalidade, Dictionary<string, object> chaves = null);
    }
}