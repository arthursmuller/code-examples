using System.Threading.Tasks;
using Infraestrutura.DTO.Zenvia;
using Infraestrutura.Enum;

namespace Infraestrutura.Providers
{
    public interface IProviderZenvia
    {
        Task<(StatusEnvio, ZenviaStatus?, ZenviaStatusDetalhes?)> EnviarMensagem( ZenviaSmsMensagemDto requisicao
                                                                                , string credenciais);
        
        Task<(StatusEnvio, ZenviaStatus?, ZenviaStatusDetalhes?, string)> ConsultarRequisicao( string id
                                                                                             , string credenciais);
        
    }
}