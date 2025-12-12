using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Dtos;
using backend.Data; 

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("ProductosPorCategoria")]
        public async Task<ActionResult<IEnumerable<ReportePorCategoriaDto>>> ProductosPorCategoria()
        {
            var query =
                from dv in _context.DetallesVenta
                join p in _context.Productos on dv.IdProducto equals p.IdProducto
                join c in _context.CategoriasProducto on p.IdCategoria equals c.IdCategoria
                join v in _context.Ventas on dv.IdVenta equals v.IdVenta
                where v.Estado != "Anulada"
                group new { dv, c } by c.Nombre into g
                select new ReportePorCategoriaDto
                {
                    Categoria = g.Key,
                    TotalUnidades = g.Sum(x => x.dv.Cantidad),
                    TotalMonto = g.Sum(x => x.dv.Cantidad * x.dv.PrecioUnitario)
                };

            return await query.OrderByDescending(x => x.TotalMonto).ToListAsync();
        }
    }
}
