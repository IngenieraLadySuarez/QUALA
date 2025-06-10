using Microsoft.Extensions.Logging;
using Quala.Sucursales.Api.DTOs;
using Quala.Sucursales.Api.Models;
using Quala.Sucursales.Api.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quala.Sucursales.Api.Services
{
    public class MonedaService
    {
        private readonly IMonedaRepository _repository;
        private readonly ILogger<MonedaService> _logger;

        public MonedaService(IMonedaRepository repository, ILogger<MonedaService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<MonedaDto>> GetAll()
        {
            _logger.LogInformation("Obteniendo todas las monedas");
            var monedas = await _repository.GetAllAsync();
            _logger.LogInformation("Se obtuvieron {Count} monedas", monedas.Count());

            return monedas.Select(m => new MonedaDto
            {
                Id = m.Id,
                Nombre = m.Nombre
            });
        }

        public async Task<MonedaDto?> GetById(int id)
        {
            _logger.LogInformation("Obteniendo moneda con Id {Id}", id);
            var moneda = await _repository.GetByIdAsync(id);

            if (moneda == null)
            {
                _logger.LogWarning("No se encontró moneda con Id {Id}", id);
                return null;
            }

            _logger.LogInformation("Moneda encontrada: {@Moneda}", moneda);
            return new MonedaDto
            {
                Id = moneda.Id,
                Nombre = moneda.Nombre
            };
        }

        public async Task<int> Create(CreateMonedaDto dto)
        {
            _logger.LogInformation("Creando nueva moneda: {@Moneda}", dto);

            // Validación previa puede ir aquí (ejemplo: existencia)
            if (await _repository.ExistsByNombreAsync(dto.Nombre))
            {
                _logger.LogWarning("Ya existe moneda con nombre {Nombre}", dto.Nombre);
                return 0; // O lanzar excepción según convención
            }

            var moneda = new Moneda { Nombre = dto.Nombre };
            var result = await _repository.CreateAsync(moneda);

            _logger.LogInformation("Moneda creada, filas afectadas: {Rows}", result);
            return result;
        }

        public async Task<int> Update(UpdateMonedaDto dto)
        {
            _logger.LogInformation("Actualizando moneda Id {Id} con datos: {@Moneda}", dto.Id, dto);

            if (await _repository.ExistsByNombreExcludingIdAsync(dto.Nombre, dto.Id))
            {
                _logger.LogWarning("Ya existe otra moneda con nombre {Nombre}", dto.Nombre);
                return 0; // O lanzar excepción según convención
            }

            var moneda = new Moneda
            {
                Id = dto.Id,
                Nombre = dto.Nombre
            };

            var result = await _repository.UpdateAsync(moneda);
            _logger.LogInformation("Moneda actualizada, filas afectadas: {Rows}", result);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            _logger.LogInformation("Eliminando moneda con Id {Id}", id);
            var result = await _repository.DeleteAsync(id);
            _logger.LogInformation("Moneda eliminada, filas afectadas: {Rows}", result);
            return result;
        }

        public async Task<bool> ExistsByNombre(string nombre)
        {
            _logger.LogInformation("Verificando existencia de moneda con nombre {Nombre}", nombre);
            var exists = await _repository.ExistsByNombreAsync(nombre);
            _logger.LogInformation("Existe moneda con nombre {Nombre}: {Exists}", nombre, exists);
            return exists;
        }

        public async Task<bool> ExistsByNombreExcludingId(string nombre, int id)
        {
            _logger.LogInformation("Verificando existencia de moneda con nombre {Nombre} excluyendo Id {Id}", nombre, id);
            var exists = await _repository.ExistsByNombreExcludingIdAsync(nombre, id);
            _logger.LogInformation("Existe moneda con nombre {Nombre} excluyendo Id {Id}: {Exists}", nombre, id, exists);
            return exists;
        }
    }
}
