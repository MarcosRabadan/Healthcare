using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.Utils;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Enums;
using Healthcare.Domain.Repositories;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.Application.Services.Login
{
    public class RegisterService : IRegisterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _unitOfWork.Usuarios.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return false;

            var passwordHash = PasswordHasher.ComputeSha256Hash(request.Password);

            var usuario = new Usuario
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                Email = request.Email,
                Rol = (RolUsuario)request.Rol,
                IsDeleted = false
            };

            await _unitOfWork.Usuarios.AddAsync(usuario);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}