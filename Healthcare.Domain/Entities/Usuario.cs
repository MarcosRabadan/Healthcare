using Healthcare.Domain.Enums;

namespace Healthcare.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
        public RolUsuario Rol { get; set; } 
        public bool IsDeleted { get; set; } = false;
    }
}  