using System.ComponentModel.DataAnnotations;

namespace Healthcare.Application.DTOs.Responses
{
    public class MedicamentoResponseDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Composicion { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string FormaFarmaceutica { get; set; } = string.Empty;
    }
}