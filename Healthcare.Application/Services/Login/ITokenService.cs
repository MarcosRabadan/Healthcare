using Healthcare.Domain.Entities;

namespace Healthcare.Application.Services.Login
{
    public interface ITokenService
    {
        string GenerateToken(Usuario usuario);
    }
}