using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }

        public int IdCategoria { get; set; }   

        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public bool Activo { get; set; }
    }
}
