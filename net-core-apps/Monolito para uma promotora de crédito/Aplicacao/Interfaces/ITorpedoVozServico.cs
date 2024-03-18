using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Enum.TemplateTorpedoVoz;
using SharedKernel.ValueObjects.v2;

namespace Aplicacao.Servico
{
    public interface ITorpedoVozServico
    {
        Task<bool> RegistrarTorpedoVoz( TemplateTorpedoVoz template
                                , int? codigoOrigem
                                , Fone fone
                                , string mensagem);
    }
}