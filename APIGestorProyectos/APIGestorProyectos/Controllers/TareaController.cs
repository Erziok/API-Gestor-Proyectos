using APIGestorProyectos.DTO;
using APIGestorProyectos.Models;
using AutoMapper;
using DataAccess.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGestorProyectos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareaController : ControllerBase
    {
        private readonly IGestorProyectosRepository<Tarea> _gestorProyectosRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly gestorproyectosContext _context;
        private readonly IMapper _mapper;

        public TareaController(IGestorProyectosRepository<Tarea> gestorProyectosRepository, IUnitOfWork unitOfWork, gestorproyectosContext context, IMapper mapper)
        {
            this._gestorProyectosRepository = gestorProyectosRepository;
            this._unitOfWork = unitOfWork;
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TareasDTO>> Get()
        {
            var tareas = await _gestorProyectosRepository.GetAsyncTarea();

            var model = _mapper.Map<IEnumerable<TareasDTO>>(tareas);

            return model;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTareaId(int id)
        {
            var tareaExistente = await _gestorProyectosRepository.FindAsync(id);

            if (tareaExistente == null)
            {
                return NotFound();
            }

            return await _unitOfWork.Context.Set<Tarea>().FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var created = await _gestorProyectosRepository.CreateAsync(tarea);

            if (created)
                _unitOfWork.Commit();

            return Created("Created", new { Response = StatusCode(201) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarea(int id, Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return BadRequest();
            }

            _context.Entry(tarea).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        //Petición DELETE: api/empresa/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }


    }
}
