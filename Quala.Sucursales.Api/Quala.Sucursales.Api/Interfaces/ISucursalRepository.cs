using Quala.Sucursales.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quala.Sucursales.Api.Repositories
{
    public interface ISucursalRepository
    {
        Task<IEnumerable<Sucursal>> GetAllAsync();
        Task<Sucursal?> GetByIdAsync(int id);
        Task<Sucursal?> GetByCodAsync(int codigo);
        Task<int> CreateAsync(Sucursal sucursal);
        Task<int> UpdateAsync(Sucursal sucursal);
        Task<int> DeleteAsync(int id);

        Task<int> DeleteCodAsync(int codigo);
        Task<bool> ExistsByCodigoAsync(int codigo);
        Task<bool> ExistsByCodigoExcludingIdAsync(int codigo, int id);
        Task<bool> MonedaExistsAsync(int monedaId);
    }
}
