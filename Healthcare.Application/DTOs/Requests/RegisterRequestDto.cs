using Healthcare.Application.DTOs.Enums;
using System.ComponentModel.DataAnnotations;

namespace Healthcare.Application.DTOs.Requests
{
    public class RegisterRequestDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public RolUsuarioDto Rol { get; set; }
    }
}