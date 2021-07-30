using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGestorProyectos.DTO;
using APIGestorProyectos.Models;

namespace APIGestorProyectos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Automapper para TareasDTO
            CreateMap<Tarea, TareasDTO>()
                .ForMember(dest => dest.NombreEstado,
                            opt => opt.MapFrom(src => src.IdEstadoNavigation.Nombre))
                .ForMember(dest => dest.NombreResponsable,
                            opt => opt.MapFrom(src => src.IdResponsableNavigation.Nombre))
                .ForMember(dest => dest.NombreProyecto,
                            opt => opt.MapFrom(src => src.IdProyectoNavigation.Nombre));

            // Automapper para ProyectosDTO
            CreateMap<Proyecto, ProyectosDTO>()
                .ForMember(dest => dest.NombreEstado,
                            opt => opt.MapFrom(src => src.IdEstadoNavigation.Nombre))
                .ForMember(dest => dest.NombreResponsable,
                            opt => opt.MapFrom(src => src.IdResponsableNavigation.Nombre))
                .ForMember(dest => dest.NombreEmpresa,
                            opt => opt.MapFrom(src => src.IdEmpresaNavigation.Nombre));

        }

    }
}
