namespace backend.Dtos
{
    public class ReportePorCategoriaDto
    {
        public string Categoria { get; set; } = "";
        public int TotalUnidades { get; set; }
        public decimal TotalMonto { get; set; }
    }
}