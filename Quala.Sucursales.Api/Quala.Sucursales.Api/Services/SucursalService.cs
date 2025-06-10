using Microsoft.Extensions.Logging;
using Quala.Sucursales.Api.Models;
using Quala.Sucursales.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quala.Sucursales.Api.Services
{
    public class SucursalService
    {
        private readonly ISucursalRepository _repository;
        private readonly ILogger<SucursalService> _logger;

        public SucursalService(ISucursalRepository repository, ILogger<SucursalService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<IEnumerable<Sucursal>> GetAll()
        {
            _logger.LogInformation("Obteniendo todas las sucursales...");
            return _repository.GetAllAsync();
        }

        public Task<Sucursal?> GetById(int id)
        {
            _logger.LogInformation("Buscando sucursal con ID {Id}", id);
            return _repository.GetByIdAsync(id);
        }

        public Task<Sucursal?> GetByCod(int codigo)
        {
            _logger.LogInformation("Buscando sucursal con Codigo {Codigo}", codigo);
            return _repository.GetByCodAsync(codigo);
        }

        public Task<int> Create(Sucursal sucursal)
        {
            _logger.LogInformation("Creando nueva sucursal con código {Codigo}", sucursal.Codigo);
            return _repository.CreateAsync(sucursal);
        }

        public Task<int> Update(Sucursal sucursal)
        {
            _logger.LogInformation("Actualizando sucursal con ID {Id}", sucursal.Id);
            return _repository.UpdateAsync(sucursal);
        }

        public Task<int> Delete(int id)
        {
            _logger.LogWarning("Eliminando sucursal con ID {Id}", id);
            return _repository.DeleteAsync(id);
        }

        public Task<bool> ExistsByCodigo(int codigo)
        {
            _logger.LogInformation("Validar si el código ya existe...");
            return _repository.ExistsByCodigoAsync(codigo);
        }

        public Task<bool> ExistsByCodigoExcludingId(int codigo, int id)
        {
            _logger.LogInformation("Validar si el código existe en otra sucursal distinta");
            return _repository.ExistsByCodigoExcludingIdAsync(codigo, id);
        }

        public Task<bool> MonedaExists(int monedaId)
        {
            _logger.LogInformation("Validar si la moneda existe");
            return _repository.MonedaExistsAsync(monedaId);
        }

        public Task<int> DeleteCod(int codigo)
        {
            _logger.LogWarning("Eliminando sucursal con código {Codigo}", codigo);
            return _repository.DeleteCodAsync(codigo);
        }
    }
}
