using Healthcare.Application.DTOs.Enums;
using Healthcare.Application.Validators;
using Healthcare.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Healthcare.Application.DTOs.Requests
{
    public class AlergiaRequestDto
    {
        [Required]
        public int PacienteId { get; set; }

        [Required]
        public EnumValueDto Tipo { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        [NoFutureDate(ErrorMessage = "La fecha no puede ser futura.")]
        public DateTime FechaDiagnostico { get; set; }

        [Required]
        [StringLength(100)]
        public string Severidad { get; set; } = string.Empty;
    }
}