using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class CategoriaProducto
    {
        [Key]                                    
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
