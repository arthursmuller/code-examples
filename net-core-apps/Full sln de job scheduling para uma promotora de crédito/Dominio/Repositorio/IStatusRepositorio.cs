using System.Threading.Tasks;

namespace Dominio.Repositorio
{
    public interface IStatusRepositorio
    {
        Task<string> BuscarStatusBanco();
    }
}