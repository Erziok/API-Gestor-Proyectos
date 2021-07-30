using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIGestorProyectos.DTO
{
    public class ProyectosDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public DateTime? FechaAsignacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaTermino { get; set; }
        [Required]
        public int? IdEmpresa { get; set; }
        [Required]
        public int? IdEstado { get; set; }
        [Required]
        public int? IdResponsable { get; set; }
        public string NombreEmpresa { get; set; }
        public string NombreEstado { get; set; }
        public string NombreResponsable { get; set; }
    }
}
