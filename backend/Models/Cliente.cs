using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Cliente
    {
        [Key]                        
        public int IdCliente { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string? Documento { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
