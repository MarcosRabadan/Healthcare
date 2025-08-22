namespace Healthcare.Application.DTOs
{
    public class ConsultaDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int PacienteId { get; set; }
        public int ProfesionalId { get; set; }
    }
}