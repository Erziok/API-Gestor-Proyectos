using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace APIGestorProyectos.Models
{
    public partial class Tarea
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
        public int? IdEstado { get; set; }
        [Required]
        public int IdProyecto { get; set; }
        [Required]
        public int? IdResponsable { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; }
        public virtual Proyecto IdProyectoNavigation { get; set; }
        public virtual Responsable IdResponsableNavigation { get; set; }
    }
}
