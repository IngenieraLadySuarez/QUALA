using Quala.Sucursales.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quala.Sucursales.Api.Repositories
{
    public interface IMonedaRepository
    {
        Task<IEnumerable<Moneda>> GetAllAsync();
        Task<Moneda?> GetByIdAsync(int id);
        Task<int> CreateAsync(Moneda moneda);
        Task<int> UpdateAsync(Moneda moneda);
        Task<int> DeleteAsync(int id);
        Task<bool> ExistsByNombreAsync(string nombre);
        Task<bool> ExistsByNombreExcludingIdAsync(string nombre, int id);
    }
}
