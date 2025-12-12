using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Mascota
    {
        [Key]
        public int IdMascota { get; set; }

        public int IdCliente { get; set; }     

        public string Nombre { get; set; } = null!;
        public string Especie { get; set; } = null!;   
        public string? Raza { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public bool Activo { get; set; }
    }
}
