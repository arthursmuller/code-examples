using Infraestrutura.Providers.Cliente.Dto;
using Infraestrutura.Providers.Cliente.Dto.ListaBeneficio;
using Infraestrutura.Providers.Cliente.Dto.NovaAutorizacao;
using SharedKernel.ValueObjects.v2;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestrutura.Providers.Cliente
{
    public interface IProviderCliente
    {
        Task<ClienteDto> ObterDadosCliente(string cpf, string tokenAutenticacao);

        Task<string> ObterAutorizacaoConsultaBeneficioExistente(CPF cpf, string tokenAutenticacao);

        Task<string> ObterNovaAutorizacaoParaConsultaBeneficioInss(NovaAutorizacaoParametrosDto parametros, string tokenAutenticacao);

        Task<IEnumerable<ListagemBeneficiosInssDto>> ListarBeneficiosInss(string chaveConsultaBeneficio, string tokenAutenticacao);
    }
}
