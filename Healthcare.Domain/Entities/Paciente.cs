using System.Collections.Generic;

namespace Healthcare.Domain.Entities
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NumeroHistoriaClinica { get; set; } = string.Empty;

        // Relaciones
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
        public ICollection<Prescripcion> Prescripciones { get; set; } = new List<Prescripcion>();
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
        public ICollection<Alergia> Alergias { get; set; } = new List<Alergia>();
    }
}