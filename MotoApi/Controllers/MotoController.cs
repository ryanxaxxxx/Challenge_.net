using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoApi.Data;
using MotoApi.Models;

namespace MotoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MotoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/moto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moto>>> GetAll()
        {
            return Ok(await _context.Motos.ToListAsync());
        }

        // GET: api/moto/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Moto>> GetById(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null) return NotFound();
            return Ok(moto);
        }

        // GET: api/moto/placa/{placa}
        [HttpGet("placa/{placa}")]
        public async Task<ActionResult<Moto>> GetByPlaca(string placa)
        {
            var moto = await _context.Motos.FirstOrDefaultAsync(m => m.Placa == placa);
            if (moto == null) return NotFound();
            return Ok(moto);
        }

        // POST: api/moto
        [HttpPost]
        public async Task<ActionResult<Moto>> CreateMoto(Moto moto)
        {
            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = moto.Id }, moto);
        }

        // PUT: api/moto/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMoto(int id, Moto moto)
        {
            if (id != moto.Id) return BadRequest();

            _context.Entry(moto).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Motos.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/moto/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoto(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto == null) return NotFound();

            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
// aqui na Controller temos o CRUD completo.