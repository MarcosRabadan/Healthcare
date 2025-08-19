namespace Healthcare.Domain.Entities
{
    public class Cita
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int ProfesionalId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Especialidad { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        // Relaciones
        public Paciente? Paciente { get; set; }
        public Profesional? Profesional { get; set; }
    }
}   