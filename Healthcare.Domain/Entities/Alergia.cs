namespace Healthcare.Domain.Entities
{
    public class Alergia
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaDiagnostico { get; set; }
        public string Severidad { get; set; } = string.Empty;

        // Relaciones
        public Paciente? Paciente { get; set; }
    }
}