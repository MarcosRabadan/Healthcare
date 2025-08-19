namespace Healthcare.Domain.Entities
{
    public class Consulta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int PacienteId { get; set; }
        public int ProfesionalId { get; set; }

        // Relaciones
        public Paciente? Paciente { get; set; }
        public Profesional? Profesional { get; set; }
        public Anamnesis? Anamnesis { get; set; }
    }
}