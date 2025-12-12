using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }

        public int IdCliente { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        public string Estado { get; set; } = "Registrada"; 
    }
}
