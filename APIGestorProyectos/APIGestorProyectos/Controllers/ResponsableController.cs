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
    public class ResponsableController : ControllerBase
    {
        private readonly IGestorProyectosRepository<Responsable> _gestorProyectosRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly gestorproyectosContext _context;

        public ResponsableController(IGestorProyectosRepository<Responsable> gestorProyectosRepository, IUnitOfWork unitOfWork, gestorproyectosContext context)
        {
            this._gestorProyectosRepository = gestorProyectosRepository;
            this._unitOfWork = unitOfWork;
            this._context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Responsable>> Get()
        {
            return await _gestorProyectosRepository.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Responsable>> GetResponsableId(int id)
        {
            var responsableExistente = await _gestorProyectosRepository.FindAsync(id);

            if (responsableExistente == null)
            {
                return NotFound();
            }

            return await _unitOfWork.Context.Set<Responsable>().FindAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Responsable responsable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var created = await _gestorProyectosRepository.CreateAsync(responsable);

            if (created)
                _unitOfWork.Commit();

            return Created("Created", new { Response = StatusCode(201) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResponsable(int id, Responsable responsable)
        {
            if (id != responsable.Id)
            {
                return BadRequest();
            }

            _context.Entry(responsable).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        //Petición DELETE: api/empresa/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResponsable(int id)
        {
            var responsable = await _context.Responsables.FindAsync(id);

            if (responsable == null)
            {
                return NotFound();
            }

            _context.Responsables.Remove(responsable);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }


    }
}
