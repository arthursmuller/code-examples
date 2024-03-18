using Aplicacao.Model.Proposta;
using System;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IPropostaServico
    {
        Task<SituacaoPropostaModel> ObterSituacaoProposta(string cpf, string token, DateTime dataNascimento);
    }
}
