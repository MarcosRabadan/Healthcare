namespace Healthcare.Application.DTOs
{
    public class PrescripcionDto
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int MedicamentoId { get; set; }
        public string Dosis { get; set; } = string.Empty;
        public string Frecuencia { get; set; } = string.Empty;
        public string Duracion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
    }
}