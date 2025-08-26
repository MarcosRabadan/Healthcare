using System.ComponentModel.DataAnnotations;

namespace Healthcare.Application.DTOs.Responses
{
    public class AnamnesisResponseDto
    {
        public int Id { get; set; }

        [Required]
        public int ConsultaId { get; set; }

        [Required]
        [StringLength(500)]
        public string AntecedentesPersonales { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string AntecedentesFamiliares { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Habitos { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string MotivoConsulta { get; set; } = string.Empty;
    }
}