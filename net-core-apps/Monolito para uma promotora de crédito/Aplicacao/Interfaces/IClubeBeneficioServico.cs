using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IClubeBeneficioServico
    {
        Task<string> CriarAutenticarUsuario();
    }
}
