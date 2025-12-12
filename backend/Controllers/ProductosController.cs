using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos
                .Where(p => p.Activo)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null || !producto.Activo)
                return NotFound();

            return producto;
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {

            var categoria = await _context.CategoriasProducto
                .FirstOrDefaultAsync(c => c.IdCategoria == producto.IdCategoria && c.Activo);

            if (categoria == null)
            {
                return BadRequest("La categoría no existe o está inactiva.");
            }

            producto.Activo = true;

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto),
                new { id = producto.IdProducto }, producto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.IdProducto)
                return BadRequest();

            var categoria = await _context.CategoriasProducto
                .FirstOrDefaultAsync(c => c.IdCategoria == producto.IdCategoria && c.Activo);

            if (categoria == null)
            {
                return BadRequest("La categoría no existe o está inactiva.");
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Productos.Any(e => e.IdProducto == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound();

            producto.Activo = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
