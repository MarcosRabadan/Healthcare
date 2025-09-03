using Healthcare.Application.DTOs.Enums;

namespace Healthcare.Application.DTOs.Responses
{
    public class UsuarioResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public RolUsuarioDto Rol { get; set; }
    }
}