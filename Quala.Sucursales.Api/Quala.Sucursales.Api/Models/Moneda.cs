using System.ComponentModel.DataAnnotations;

namespace Quala.Sucursales.Api.Models
{
    public class Moneda
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;
    }
}
