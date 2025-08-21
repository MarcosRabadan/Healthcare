namespace Healthcare.Application.DTOs
{
    public class MedicamentoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Composicion { get; set; } = string.Empty;
        public string FormaFarmaceutica { get; set; } = string.Empty;
    }
}