using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
        {
            return await _context.Ventas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetVenta(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);

            if (venta == null)
                return NotFound();

            return venta;
        }

        [HttpPost]
        public async Task<ActionResult<Venta>> PostVenta(Venta venta)
        {
 
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.IdCliente == venta.IdCliente && c.Activo);

            if (cliente == null)
                return BadRequest("El cliente no existe o está inactivo.");

            venta.Fecha = DateTime.Now;
            venta.Total = 0;
            if (string.IsNullOrWhiteSpace(venta.Estado))
                venta.Estado = "Registrada";

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVenta), new { id = venta.IdVenta }, venta);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenta(int id, Venta venta)
        {
            if (id != venta.IdVenta)
                return BadRequest();

            _context.Entry(venta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Ventas.Any(e => e.IdVenta == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
                return NotFound();

            venta.Estado = "Anulada";
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
