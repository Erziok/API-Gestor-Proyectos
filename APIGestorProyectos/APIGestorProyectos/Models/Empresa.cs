using System;
using System.Collections.Generic;

#nullable disable

namespace APIGestorProyectos.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            Proyectos = new HashSet<Proyecto>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Proyecto> Proyectos { get; set; }
    }
}
