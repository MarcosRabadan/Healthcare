namespace Healthcare.Domain.Entities
{
    public class Prescripcion
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int MedicamentoId { get; set; }
        public string Dosis { get; set; } = string.Empty;
        public string Frecuencia { get; set; } = string.Empty;
        public string Duracion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }

        // Relaciones
        public Paciente? Paciente { get; set; }
        public Medicamento? Medicamento { get; set; }

        // Borrado lógico
        public bool IsDeleted { get; set; } = false;
    }
}