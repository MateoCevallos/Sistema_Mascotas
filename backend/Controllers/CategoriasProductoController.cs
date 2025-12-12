using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasProductoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasProductoController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaProducto>>> GetCategorias()
        {
            return await _context.CategoriasProducto
                .Where(c => c.Activo)
                .ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaProducto>> GetCategoria(int id)
        {
            var categoria = await _context.CategoriasProducto.FindAsync(id);

            if (categoria == null || !categoria.Activo)
                return NotFound();

            return categoria;
        }


        [HttpPost]
        public async Task<ActionResult<CategoriaProducto>> PostCategoria(CategoriaProducto categoria)
        {
            categoria.Activo = true;

            _context.CategoriasProducto.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoria),
                new { id = categoria.IdCategoria }, categoria);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, CategoriaProducto categoria)
        {
            if (id != categoria.IdCategoria)
                return BadRequest();

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CategoriasProducto.Any(e => e.IdCategoria == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.CategoriasProducto.FindAsync(id);
            if (categoria == null)
                return NotFound();

            categoria.Activo = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
