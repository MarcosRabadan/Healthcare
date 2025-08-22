using Healthcare.Domain.Enums;

namespace Healthcare.Application.DTOs
{
    public class AlergiaDto
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public TipoAlergia Tipo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaDiagnostico { get; set; }
        public string Severidad { get; set; } = string.Empty;
    }
}