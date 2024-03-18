using Infraestrutura.Providers.BemApi.Dto;
using SharedKernel.ValueObjects.v2;
using System;
using System.Threading.Tasks;

namespace Infraestrutura.Providers.BemApi
{
    public interface IProviderBemApi
    {
        Task<ObtencaoSituacaoPropostaDto> ObterSituacaoProposta(CPF cpf, string token, DateTime dataNascimento, string tokenAutenticacao);
    }
}
