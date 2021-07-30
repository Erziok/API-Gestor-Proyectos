using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Generic;
using APIGestorProyectos.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestorProyectos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly IGestorProyectosRepository<Empresa> _gestorProyectosRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly gestorproyectosContext _context;

        public EmpresaController(IGestorProyectosRepository<Empresa> gestorProyectosRepository, IUnitOfWork unitOfWork, gestorproyectosContext context)
        {
            this._gestorProyectosRepository = gestorProyectosRepository;
            this._unitOfWork = unitOfWork;
            this._context = context;
        }

        // Petición GET: api/empresa
        [HttpGet]
        public async Task<IEnumerable<Empresa>> GetEmpresa()
        {
            return await _gestorProyectosRepository.GetAsync();
        }

        // Petición GET por ID: api/empresa/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresaId(int id)
        {
            var empresaExistente = await _gestorProyectosRepository.FindAsync(id);

            if (empresaExistente == null)
            {
                return NotFound();
            }

            return await _unitOfWork.Context.Set<Empresa>().FindAsync(id);
        }

        //Petición POST: api/empresa
        [HttpPost]
        public async Task<IActionResult> CreateEmpresa([FromBody] Empresa empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var created = await _gestorProyectosRepository.CreateAsync(empresa);

            if (created)
                _unitOfWork.Commit();

            return Created("Created", new { Response = StatusCode(201) });
        }

        //Petición PUT: api/empresa/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpresa(int id, Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return BadRequest();
            }

            _context.Entry(empresa).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        //Petición DELETE: api/empresa/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

    }
}
