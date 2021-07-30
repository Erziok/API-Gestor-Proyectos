using APIGestorProyectos.DTO;
using APIGestorProyectos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace DataAccess.Generic
{
    public interface IGestorProyectosRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<IEnumerable<Tarea>> GetAsyncTarea();
        Task<IEnumerable<Proyecto>> GetAsyncProyecto();
        Task<IEnumerable<TareasDTO>> GetAsyncDTO();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> whereCondition = null,
                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                           string includeProperties = "");
        Task<bool> CreateAsync(T entity);
        Task<IEnumerable<T>> FindAsync(int id);
        Task<IEnumerable<Tarea>> FindAsyncTarea(int id);
        Task<IEnumerable<Proyecto>> FindAsyncProyecto(int id);
    }

    public class GestorProyectosRepository<T> : IGestorProyectosRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        public GestorProyectosRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _unitOfWork.Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<Tarea>> GetAsyncTarea()
        {
            return await _unitOfWork.Context.Set<Tarea>()
                     .Include(x => x.IdEstadoNavigation)
                     .Include(x => x.IdResponsableNavigation)
                     .Include(x => x.IdProyectoNavigation).ToListAsync();
        }

        public async Task<IEnumerable<Proyecto>> GetAsyncProyecto()
        {
            return await _unitOfWork.Context.Set<Proyecto>()
                     .Include(x => x.IdEstadoNavigation)
                     .Include(x => x.IdResponsableNavigation)
                     .Include(x => x.IdEmpresaNavigation).ToListAsync();
        }

        public async Task<IEnumerable<TareasDTO>> GetAsyncDTO()
        {
            var tareas = await _unitOfWork.Context.Set<Tarea>()
                     .Include(x => x.IdEstadoNavigation)
                     .Include(x => x.IdResponsableNavigation)
                     .Include(x => x.IdProyectoNavigation).ToListAsync();

            List<TareasDTO> tareasDTO = new List<TareasDTO>();
            return tareasDTO;
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> whereCondition = null,
                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                  string includeProperties = "")
        {
            IQueryable<T> query = _unitOfWork.Context.Set<T>();

            if (whereCondition != null)
            {
                query = query.Where(whereCondition);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<bool> CreateAsync(T entity)
        {
            bool created = false;

            try
            {
                var save = await _unitOfWork.Context.Set<T>().AddAsync(entity);

                if (save != null)
                    created = true;
            }
            catch (Exception)
            {
                throw;
            }
            return created;
        }

        public async Task<IEnumerable<T>> FindAsync(int id)
        {
            return await _unitOfWork.Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<Tarea>> FindAsyncTarea(int id)
        {
            return await _unitOfWork.Context.Set<Tarea>()
                     .Include(x => x.IdEstadoNavigation)
                     .Include(x => x.IdResponsableNavigation)
                     .Include(x => x.IdProyectoNavigation).ToListAsync();
        }

        public async Task<IEnumerable<Proyecto>> FindAsyncProyecto(int id)
        {
            return await _unitOfWork.Context.Set<Proyecto>()
                     .Include(x => x.IdEstadoNavigation)
                     .Include(x => x.IdResponsableNavigation)
                     .Include(x => x.IdEmpresaNavigation).ToListAsync();
        }

    }
}