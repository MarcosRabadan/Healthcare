using Healthcare.Application.DTOs.Enums;

namespace Healthcare.Application.DTOs.Requests
{
    public class UsuarioRequestDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public RolUsuarioDto Rol { get; set; } 
    }
}