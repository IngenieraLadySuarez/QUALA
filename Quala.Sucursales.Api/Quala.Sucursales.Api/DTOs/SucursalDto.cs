using System;
using System.ComponentModel.DataAnnotations;

namespace Quala.Sucursales.Api.DTOs
{
    // DTO para mostrar sucursal (puede ser igual al modelo o ajustar según necesidad)
    public class SucursalDto
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Identificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int MonedaId { get; set; }
    }

    // DTO para creación de sucursal
    public class CreateSucursalDto
    {
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

    // DTO para actualización de sucursal
    public class UpdateSucursalDto
    {
        [Required]
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
