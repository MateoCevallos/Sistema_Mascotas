namespace backend.Models
{
    public class ReportePorCategoriaDto
    {
        public string Categoria { get; set; } = string.Empty;
        public int TotalUnidades { get; set; }
        public decimal TotalMonto { get; set; }
    }
}
