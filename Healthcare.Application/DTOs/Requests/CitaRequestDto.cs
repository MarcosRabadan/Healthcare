using Healthcare.Application.DTOs.Enums;
using Healthcare.Application.Validators;
using Healthcare.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Healthcare.Application.DTOs.Requests
{
    public class CitaRequestDto
    {
        [Required]
        public int PacienteId { get; set; }

        [Required]
        public int ProfesionalId { get; set; }

        [Required]
        [NoPastDate(ErrorMessage = "La fecha no puede ser pasada.")]
        public DateTime FechaHora { get; set; }

        [Required]
        [StringLength(100)]
        public string Especialidad { get; set; } = string.Empty;

        [Required]
        public EnumValueDto Estado { get; set; }
    }
}