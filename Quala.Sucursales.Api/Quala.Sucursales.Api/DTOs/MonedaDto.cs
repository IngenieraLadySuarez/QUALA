using System.ComponentModel.DataAnnotations;

namespace Quala.Sucursales.Api.DTOs
{
    public class MonedaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    public class CreateMonedaDto
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = null!;
    }

    public class UpdateMonedaDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = null!;
    }
}
