using Microsoft.AspNetCore.Mvc;
using Quala.Sucursales.Api.Auth;
using Quala.Sucursales.Api.DTOs;
using Quala.Sucursales.Api.Models;

namespace Quala.Sucursales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto login)
        {
            // Simulación de usuario válido
            if (login.Username == "admin" && login.Password == "1234")
            {
                var user = new User
                {
                    Username = login.Username,
                    Role = "Admin"
                };

                var token = _jwtService.GenerateToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized("Credenciales inválidas");
        }
    }
}
