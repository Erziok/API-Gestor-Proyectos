using System;
using System.Collections.Generic;

#nullable disable

namespace APIGestorProyectos.Models
{
    public partial class Responsable
    {
        public Responsable()
        {
            Proyectos = new HashSet<Proyecto>();
            Tareas = new HashSet<Tarea>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Proyecto> Proyectos { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
