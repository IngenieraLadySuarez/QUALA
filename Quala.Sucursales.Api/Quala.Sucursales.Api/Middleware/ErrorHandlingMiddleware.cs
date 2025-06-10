using System.Net;
using System.Text.Json;

namespace Quala.Sucursales.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Continúa al siguiente middleware/controlador
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido una excepción no controlada");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Ocurrió un error inesperado. Por favor, contacta al soporte.",
                    Error = ex.Message // Para desarrollo. En producción podrías ocultarlo.
                };

                var errorJson = JsonSerializer.Serialize(errorResponse);

                await context.Response.WriteAsync(errorJson);
            }
        }
    }
}
