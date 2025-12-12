using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MascotasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mascota>>> GetMascotas()
        {
            return await _context.Mascotas
                .Where(m => m.Activo)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mascota>> GetMascota(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);

            if (mascota == null || !mascota.Activo)
                return NotFound();

            return mascota;
        }

        [HttpPost]
        public async Task<ActionResult<Mascota>> PostMascota(Mascota mascota)
        {

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.IdCliente == mascota.IdCliente && c.Activo);

            if (cliente == null)
            {
                return BadRequest("El cliente no existe o está inactivo.");
            }

            mascota.Activo = true;

            _context.Mascotas.Add(mascota);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMascota), new { id = mascota.IdMascota }, mascota);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutMascota(int id, Mascota mascota)
        {
            if (id != mascota.IdMascota)
                return BadRequest();

 
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.IdCliente == mascota.IdCliente && c.Activo);

            if (cliente == null)
            {
                return BadRequest("El cliente no existe o está inactivo.");
            }

            _context.Entry(mascota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Mascotas.Any(e => e.IdMascota == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMascota(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
                return NotFound();

            mascota.Activo = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
