using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class DetalleVenta
    {
        [Key]
        public int IdDetalle { get; set; }

        public int IdVenta { get; set; }

        public int IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }
    }
}
