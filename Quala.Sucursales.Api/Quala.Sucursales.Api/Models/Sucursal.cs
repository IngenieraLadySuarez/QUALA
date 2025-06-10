using System.ComponentModel.DataAnnotations;

namespace Quala.Sucursales.Api.Models
{
    public class Sucursal
    {
        public int Id { get; set; }

        [Required]
        public int Codigo { get; set; }

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }

        [Required]
        [MaxLength(250)]
        public string Direccion { get; set; }

        [Required]
        [MaxLength(50)]
        public string Identificacion { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public int MonedaId { get; set; }
    }
}
