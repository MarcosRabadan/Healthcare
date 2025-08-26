using Healthcare.Application.Validators;
using System.ComponentModel.DataAnnotations;

namespace Healthcare.Application.DTOs.Responses
{
    public class ConsultaResponseDto
    {
        public int Id { get; set; }
        [Required]
        [NoPastDate(ErrorMessage = "La fecha no puede ser pasada.")]
        public DateTime Fecha { get; set; }

        [Required]
        public int PacienteId { get; set; }

        [Required]
        public int ProfesionalId { get; set; }
    }
}