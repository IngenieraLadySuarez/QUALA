using Dapper;
using Quala.Sucursales.Api.Data;
using Quala.Sucursales.Api.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Quala.Sucursales.Api.Repositories
{
    public class MonedaRepository : IMonedaRepository
    {
        private readonly DapperContext _context;
        private readonly ILogger<MonedaRepository> _logger;

        public MonedaRepository(DapperContext context, ILogger<MonedaRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Moneda>> GetAllAsync()
        {
            _logger.LogInformation("Obteniendo todas las monedas...");
            var query = "js_mon_ObtenerMonedas";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Moneda>(query, commandType: CommandType.StoredProcedure);
        }

        public async Task<Moneda?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Buscando moneda con ID {Id}", id);
            var query = "js_mon_ObtenerMonedaPorId";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Moneda>(query, new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateAsync(Moneda moneda)
        {
            _logger.LogInformation("Creando nueva moneda con nombre {Nombre}", moneda.Nombre);
            var query = "js_mon_InsertarMoneda";
            var parameters = new DynamicParameters();
            parameters.Add("@Nombre", moneda.Nombre);

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateAsync(Moneda moneda)
        {
            _logger.LogInformation("Actualizando moneda con ID {Id}", moneda.Id);
            var query = "js_mon_ActualizarMoneda";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", moneda.Id);
            parameters.Add("@Nombre", moneda.Nombre);

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteAsync(int id)
        {
            _logger.LogWarning("Eliminando moneda con ID {Id}", id);
            var query = "js_mon_EliminarMoneda";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> ExistsByNombreAsync(string nombre)
        {
            _logger.LogInformation("Validando si el nombre de moneda ya existe...");
            var query = "SELECT COUNT(1) FROM js_mon_moneda WHERE Nombre = @Nombre";
            using var connection = _context.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(query, new { Nombre = nombre });
            return count > 0;
        }

        public async Task<bool> ExistsByNombreExcludingIdAsync(string nombre, int id)
        {
            _logger.LogInformation("Validando si el nombre de moneda existe en otro registro distinto...");
            var query = "SELECT COUNT(1) FROM js_mon_moneda WHERE Nombre = @Nombre AND Id != @Id";
            using var connection = _context.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(query, new { Nombre = nombre, Id = id });
            return count > 0;
        }
    }
}
