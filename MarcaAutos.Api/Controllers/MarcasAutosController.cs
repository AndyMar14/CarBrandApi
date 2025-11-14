using MarcaAutos.Api.Data;
using MarcaAutos.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarcaAutos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasAutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MarcasAutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MarcasAutos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaAuto>>> Get()
        {
            var marcas = await _context.MarcasAutos.AsNoTracking().ToListAsync();
            return Ok(marcas);
        }

        // GET: api/MarcasAutos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MarcaAuto>> Get(int id)
        {
            var marca = await _context.MarcasAutos.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (marca == null)
            {
                return NotFound($"No se encontró la marca con ID {id}");
            }

            return Ok(marca);
        }

        // POST: api/MarcasAutos
        [HttpPost]
        public async Task<ActionResult<MarcaAuto>> Post([FromBody] MarcaAuto marca)
        {
            if (string.IsNullOrWhiteSpace(marca.Nombre))
            {
                return BadRequest("El nombre de la marca es requerido");
            }

            _context.MarcasAutos.Add(marca);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = marca.Id }, marca);
        }

        // PUT: api/MarcasAutos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MarcaAuto marca)
        {
            if (id != marca.Id)
            {
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo de la solicitud");
            }

            if (string.IsNullOrWhiteSpace(marca.Nombre))
            {
                return BadRequest("El nombre de la marca es requerido");
            }

            var marcaExistente = await _context.MarcasAutos.FindAsync(id);
            if (marcaExistente == null)
            {
                return NotFound($"No se encontró la marca con ID {id}");
            }

            marcaExistente.Nombre = marca.Nombre;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MarcaAutoExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/MarcasAutos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var marca = await _context.MarcasAutos.FindAsync(id);
            if (marca == null)
            {
                return NotFound($"No se encontró la marca con ID {id}");
            }

            _context.MarcasAutos.Remove(marca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> MarcaAutoExists(int id)
        {
            return await _context.MarcasAutos.AnyAsync(e => e.Id == id);
        }
    }
}
