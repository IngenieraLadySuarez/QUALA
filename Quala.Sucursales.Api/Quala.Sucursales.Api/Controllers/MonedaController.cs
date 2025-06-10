using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quala.Sucursales.Api.DTOs;
using Quala.Sucursales.Api.Services;

namespace Quala.Sucursales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MonedaController : ControllerBase
    {
        private readonly MonedaService _service;

        public MonedaController(MonedaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var monedas = await _service.GetAll();
            return Ok(monedas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var moneda = await _service.GetById(id);
            return moneda == null ? NotFound() : Ok(moneda);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMonedaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _service.ExistsByNombre(dto.Nombre))
                return BadRequest($"Ya existe una moneda con el nombre '{dto.Nombre}'.");

            await _service.Create(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMonedaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest("El ID de la URL y del cuerpo no coinciden.");

            if (await _service.ExistsByNombreExcludingId(dto.Nombre, id))
                return BadRequest($"Ya existe una moneda con el nombre '{dto.Nombre}'.");

            await _service.Update(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
