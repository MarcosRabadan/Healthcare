using Healthcare.Domain.Enums;

namespace Healthcare.Domain.Entities
{
    public class Cita
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int ProfesionalId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Especialidad { get; set; } = string.Empty;
        public EstadoCita Estado { get; set; }

        // Relaciones
        public Paciente? Paciente { get; set; }
        public Profesional? Profesional { get; set; }

        // Borrado lógico
        public bool IsDeleted { get; set; } = false;
    }
}   