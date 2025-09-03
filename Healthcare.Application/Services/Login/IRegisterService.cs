using Healthcare.Application.DTOs.Requests;
using System.Threading.Tasks;

namespace Healthcare.Application.Services.Login
{
    public interface IRegisterService
    {
        Task<bool> RegisterAsync(RegisterRequestDto request);
    }
}