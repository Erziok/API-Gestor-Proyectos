using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using APIGestorProyectos.Models;
using Microsoft.AspNetCore.Components;

namespace APIGestorProyectos.DTO
{
    public class TareasDTO
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
        public string NombreEstado { get; set; }
        public string NombreProyecto { get; set; }
        public string NombreResponsable { get; set; }

    }
}
