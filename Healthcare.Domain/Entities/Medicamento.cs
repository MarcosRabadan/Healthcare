using System.Collections.Generic;

namespace Healthcare.Domain.Entities
{
    public class Medicamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Composicion { get; set; } = string.Empty;
        public string FormaFarmaceutica { get; set; } = string.Empty;

        // Relaciones
        public ICollection<Prescripcion> Prescripciones { get; set; } = new List<Prescripcion>();
    }
}