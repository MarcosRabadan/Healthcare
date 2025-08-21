using Healthcare.Domain.Enums;

namespace Healthcare.Application.DTOs
{
    public class CitaDto
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int ProfesionalId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Especialidad { get; set; } = string.Empty;
        public EstadoCita Estado { get; set; }
    }
}