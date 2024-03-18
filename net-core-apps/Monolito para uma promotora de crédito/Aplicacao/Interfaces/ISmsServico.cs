using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Enum.TemplateSms;
using SharedKernel.ValueObjects.v2;

namespace Aplicacao.Servico
{
    public interface ISmsServico
    {
        Task<bool> RegistrarSms( TemplateSms templateSms
                         , int? codigoOrigem
                         , Fone fone
                         , string mensagem);
    }
}