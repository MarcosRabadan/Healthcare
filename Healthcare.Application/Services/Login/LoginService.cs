using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.Utils;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Application.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Usuario?> ValidateUserAsync(LoginRequestDto request)
        {
            var usuario = await _unitOfWork.Usuarios.GetByEmailAsync(request.Email);
            if (usuario == null || usuario.IsDeleted)
                return null;

            var hash = PasswordHasher.ComputeSha256Hash(request.Password);
            if (usuario.PasswordHash != hash)
                return null;

            return usuario;
        }
    }
}