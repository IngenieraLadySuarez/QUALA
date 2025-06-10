using Dapper;
using Quala.Sucursales.Api.Data;
using Quala.Sucursales.Api.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Quala.Sucursales.Api.Repositories
{
    public class SucursalRepository : ISucursalRepository
    {
        private readonly DapperContext _context;

        public SucursalRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sucursal>> GetAllAsync()
        {
            var query = "js_suc_ObtenerSucursales";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Sucursal>(query, commandType: CommandType.StoredProcedure);
        }

        public async Task<Sucursal?> GetByIdAsync(int id)
        {
            var query = "js_suc_ObtenerSucursalPorId";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Sucursal>(query, new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateAsync(Sucursal sucursal)
        {
            var query = "js_suc_InsertarSucursal";
            var parameters = new DynamicParameters();
            parameters.Add("@Codigo", sucursal.Codigo);
            parameters.Add("@Descripcion", sucursal.Descripcion);
            parameters.Add("@Direccion", sucursal.Direccion);
            parameters.Add("@Identificacion", sucursal.Identificacion);
            parameters.Add("@FechaCreacion", sucursal.FechaCreacion);
            parameters.Add("@MonedaId", sucursal.MonedaId);

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateAsync(Sucursal sucursal)
        {
            var query = "js_suc_ActualizarSucursal";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", sucursal.Id);
            parameters.Add("@Codigo", sucursal.Codigo);
            parameters.Add("@Descripcion", sucursal.Descripcion);
            parameters.Add("@Direccion", sucursal.Direccion);
            parameters.Add("@Identificacion", sucursal.Identificacion);
            parameters.Add("@FechaCreacion", sucursal.FechaCreacion);
            parameters.Add("@MonedaId", sucursal.MonedaId);

            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "js_suc_EliminarSucursal";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, new { Codigo = id }, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> ExistsByCodigoAsync(int codigo)
        {
            var query = "SELECT COUNT(1) FROM js_suc_sucursal WHERE Codigo = @Codigo";
            using var connection = _context.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(query, new { Codigo = codigo });
            return count > 0;
        }

        public async Task<bool> ExistsByCodigoExcludingIdAsync(int codigo, int id)
        {
            var query = "SELECT COUNT(1) FROM js_suc_sucursal WHERE Codigo = @Codigo AND Id != @Id";
            using var connection = _context.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(query, new { Codigo = codigo, Id = id });
            return count > 0;
        }

        public async Task<bool> MonedaExistsAsync(int monedaId)
        {
            var query = "SELECT COUNT(1) FROM js_mon_moneda WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            var count = await connection.ExecuteScalarAsync<int>(query, new { Id = monedaId });
            return count > 0;
        }

        public async Task<Sucursal?> GetByCodAsync(int codigo)
        {
            var query = "js_suc_ObtenerSucursalPorCod";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Sucursal>(query, new { Codigo = codigo }, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteCodAsync(int codigo)
        {
            var query = "js_suc_EliminarSucursal";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, new { Codigo = codigo }, commandType: CommandType.StoredProcedure);
        }
    }
}
