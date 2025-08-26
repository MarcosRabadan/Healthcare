using Healthcare.Application.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Healthcare.Application.DTOs.Requests
{
    public class PacienteRequestDto
    {

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [NoFutureDate(ErrorMessage = "La fecha no puede ser futura.")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(1)]
        [RegularExpression("^(M|F)$")]
        public string Sexo { get; set; } = string.Empty;

        [StringLength(100)]
        public string Direccion { get; set; } = string.Empty;

        [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
        [StringLength(90)]
        public string Telefono { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
    }
}