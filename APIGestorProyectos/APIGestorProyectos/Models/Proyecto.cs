using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace APIGestorProyectos.Models
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            Tareas = new HashSet<Tarea>();
        }
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

        public virtual Empresa IdEmpresaNavigation { get; set; }
        public virtual Estado IdEstadoNavigation { get; set; }
        public virtual Responsable IdResponsableNavigation { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
