using Aplicacao.Model.Anexo;
using Aplicacao.Model.Biometria;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public interface IBiometriaServico
    {
        Task<BiometriaConsultaModel> ObterSituacaoBiometria();
        Task<bool> ExecutarBiometria();
        Task<bool> ProcessarRetornoWebhookUnico(BiometriaWebhookRetornoUnicoModel retorno);

    }
}
