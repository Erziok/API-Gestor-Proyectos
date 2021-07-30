using APIGestorProyectos.Models;
using DataAccess.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using APIGestorProyectos.DTO;

namespace APIGestorProyectos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectoController : ControllerBase
    {
        private readonly IGestorProyectosRepository<Proyecto> _gestorProyectosRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly gestorproyectosContext _context;
        private readonly IMapper _mapper;

        public ProyectoController(IGestorProyectosRepository<Proyecto> gestorProyectosRepository, IUnitOfWork unitOfWork, gestorproyectosContext context, IMapper mapper)
        {
            this._gestorProyectosRepository = gestorProyectosRepository;
            this._unitOfWork = unitOfWork;
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProyectosDTO>> Get()
        {
            var proyectos = await _gestorProyectosRepository.GetAsyncProyecto();

            var model = _mapper.Map<IEnumerable<ProyectosDTO>>(proyectos);

            return model;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> GetProyectoId(int id)
        {
            var proyectoExistente = await _gestorProyectosRepository.FindAsync(id);

            if (proyectoExistente == null)
            {
                return NotFound();
            }

            return await _unitOfWork.Context.Set<Proyecto>().FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Proyecto proyecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var created = await _gestorProyectosRepository.CreateAsync(proyecto);

            if (created)
                _unitOfWork.Commit();

            return Created("Created", new { Response = StatusCode(201) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProyecto(int id, Proyecto proyecto)
        {
            if (id != proyecto.Id)
            {
                return BadRequest();
            }

            _context.Entry(proyecto).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        //Petición DELETE: api/empresa/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProyecto(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }


    }
}
