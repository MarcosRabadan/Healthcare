using Healthcare.Application.Validators;
using System.ComponentModel.DataAnnotations;

namespace Healthcare.Application.DTOs.Requests
{
    public class PrescripcionRequestDto
    {
        [Required]
        public int PacienteId { get; set; }

        [Required]
        public int MedicamentoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Dosis { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Frecuencia { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Duracion { get; set; } = string.Empty;

        [Required]
        [NoPastDate(ErrorMessage = "La fecha no puede ser pasada.")]
        public DateTime FechaInicio { get; set; }
    }
}