using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DetalleVentaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("por-venta/{idVenta}")]
        public async Task<ActionResult<IEnumerable<DetalleVenta>>> GetDetallesPorVenta(int idVenta)
        {
            var detalles = await _context.DetallesVenta
                .Where(d => d.IdVenta == idVenta)
                .ToListAsync();

            return detalles;
        }

        [HttpPost]
        public async Task<IActionResult> PostDetalleVenta(DetalleVenta detalle)
        {

            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.IdProducto == detalle.IdProducto);

            if (producto == null)
                return BadRequest("Producto no existe.");


            if (producto.Stock < detalle.Cantidad)
                return BadRequest($"Stock insuficiente. Disponible: {producto.Stock}.");

            producto.Stock -= detalle.Cantidad;

            _context.DetallesVenta.Add(detalle);


            await _context.SaveChangesAsync();

            return Ok(detalle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleVenta(int id, DetalleVenta detalle)
        {
            if (id != detalle.IdDetalle)
                return BadRequest();

            _context.Entry(detalle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ActualizarTotalVenta(detalle.IdVenta);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.DetallesVenta.Any(e => e.IdDetalle == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleVenta(int id)
        {
            var detalle = await _context.DetallesVenta.FindAsync(id);
            if (detalle == null)
                return NotFound();

            int idVenta = detalle.IdVenta;

            _context.DetallesVenta.Remove(detalle);
            await _context.SaveChangesAsync();

            await ActualizarTotalVenta(idVenta);

            return NoContent();
        }

        private async Task ActualizarTotalVenta(int idVenta)
        {
            var venta = await _context.Ventas.FindAsync(idVenta);
            if (venta == null) return;

            var total = await _context.DetallesVenta
                .Where(d => d.IdVenta == idVenta)
                .SumAsync(d => d.Subtotal);

            venta.Total = total;
            await _context.SaveChangesAsync();
        }
    }
}
