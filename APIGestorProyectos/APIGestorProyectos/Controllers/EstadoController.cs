using APIGestorProyectos.Models;
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
    public class EstadoController : ControllerBase
    {
        private readonly IGestorProyectosRepository<Estado> _gestorProyectosRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly gestorproyectosContext _context;

        public EstadoController(IGestorProyectosRepository<Estado> gestorProyectosRepository, IUnitOfWork unitOfWork, gestorproyectosContext context)
        {
            this._gestorProyectosRepository = gestorProyectosRepository;
            this._unitOfWork = unitOfWork;
            this._context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Estado>> Get()
        {
            return await _gestorProyectosRepository.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estado>> GetEstadoId(int id)
        {
            var estadoExistente = await _gestorProyectosRepository.FindAsync(id);

            if (estadoExistente == null)
            {
                return NotFound();
            }

            return await _unitOfWork.Context.Set<Estado>().FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Estado estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var created = await _gestorProyectosRepository.CreateAsync(estado);

            if (created)
                _unitOfWork.Commit();

            return Created("Created", new { Response = StatusCode(201) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstado(int id, Estado estado)
        {
            if (id != estado.Id)
            {
                return BadRequest();
            }

            _context.Entry(estado).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        //Petición DELETE: api/empresa/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estado = await _context.Estados.FindAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }


    }
}
