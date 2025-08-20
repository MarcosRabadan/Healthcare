using System.Collections.Generic;

namespace Healthcare.Domain.Entities
{
    public class Profesional
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Relaciones
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();

        // Borrado lógico
        public bool IsDeleted { get; set; } = false;
    }
}