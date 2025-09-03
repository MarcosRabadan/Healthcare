using Healthcare.Application.DTOs.Requests;
using Healthcare.Domain.Entities;
using System.Threading.Tasks;

namespace Healthcare.Application.Services.Login
{
    public interface ILoginService
    {
        Task<Usuario?> ValidateUserAsync(LoginRequestDto request);
    }
}