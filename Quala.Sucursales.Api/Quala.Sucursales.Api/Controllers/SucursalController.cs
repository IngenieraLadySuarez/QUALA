using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quala.Sucursales.Api.DTOs;
using Quala.Sucursales.Api.Models;
using Quala.Sucursales.Api.Services;
using System.Text.RegularExpressions;

namespace Quala.Sucursales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SucursalController : ControllerBase
    {
        private readonly SucursalService _service;

        public SucursalController(SucursalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var sucursal = await _service.GetById(id);
            return sucursal == null ? NotFound() : Ok(sucursal);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSucursalDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (createDto.FechaCreacion.Date < DateTime.UtcNow.Date)
                return BadRequest("La fecha de creación no puede ser anterior a hoy.");

            if (await _service.ExistsByCodigo(createDto.Codigo))
                return BadRequest($"El código {createDto.Codigo} ya está registrado.");

            if (!await _service.MonedaExists(createDto.MonedaId))
                return BadRequest($"La moneda con Id {createDto.MonedaId} no existe.");

            if (string.IsNullOrWhiteSpace(createDto.Descripcion))
                return BadRequest("La descripción no puede estar vacía o solo contener espacios.");

            if (string.IsNullOrWhiteSpace(createDto.Direccion))
                return BadRequest("La dirección no puede estar vacía o solo contener espacios.");

            if (!Regex.IsMatch(createDto.Identificacion, @"^\d+$"))
                return BadRequest("La identificación debe contener sólo números.");

            // Map CreateSucursalDto to Sucursal model
            var sucursal = new Sucursal
            {
                Codigo = createDto.Codigo,
                Descripcion = createDto.Descripcion,
                Direccion = createDto.Direccion,
                Identificacion = createDto.Identificacion,
                FechaCreacion = createDto.FechaCreacion,
                MonedaId = createDto.MonedaId
            };

            await _service.Create(sucursal);
            return Ok();
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> Update(int codigo, [FromBody] UpdateSucursalDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (updateDto.Id != updateDto.Id)
                return BadRequest("El Id de la sucursal no coincide con el Id de la ruta.");

            if (updateDto.FechaCreacion.Date < DateTime.UtcNow.Date)
                return BadRequest("La fecha de creación no puede ser anterior a hoy.");

            var existing = await _service.GetByCod(updateDto.Codigo);
            if (existing == null)
                return NotFound($"No existe la sucursal con Id {updateDto.Codigo}.");

            //if (await _service.ExistsByCodigoExcludingId(updateDto.Codigo, updateDto.Id))
            //    return BadRequest($"El código {updateDto.Codigo} ya está registrado en otra sucursal.");

            if (!await _service.MonedaExists(updateDto.MonedaId))
                return BadRequest($"La moneda con Id {updateDto.MonedaId} no existe.");

            if (string.IsNullOrWhiteSpace(updateDto.Descripcion))
                return BadRequest("La descripción no puede estar vacía o solo contener espacios.");

            if (string.IsNullOrWhiteSpace(updateDto.Direccion))
                return BadRequest("La dirección no puede estar vacía o solo contener espacios.");

            if (!Regex.IsMatch(updateDto.Identificacion, @"^\d+$"))
                return BadRequest("La identificación debe contener sólo números.");

            // Map UpdateSucursalDto to Sucursal model
            var sucursal = new Sucursal
            {
                Id = updateDto.Id,
                Codigo = updateDto.Codigo,
                Descripcion = updateDto.Descripcion,
                Direccion = updateDto.Direccion,
                Identificacion = updateDto.Identificacion,
                FechaCreacion = updateDto.FechaCreacion,
                MonedaId = updateDto.MonedaId
            };

            await _service.Update(sucursal);
            return Ok();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Delete(int codigo)
        {
            var existing = await _service.GetByCod(codigo);
            if (existing == null)
                return NotFound($"No existe la sucursal con el código {codigo}.");

            await _service.DeleteCod(codigo);
            return Ok();
        }
    }
}
