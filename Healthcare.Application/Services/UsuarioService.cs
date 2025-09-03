using AutoMapper;
using Healthcare.Application.Constants;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Application.Utils;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Enums;
using Healthcare.Domain.Repositories;

namespace Healthcare.Application.Services
{
    public class UsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsuarioService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioResponseDto>> GetAllAsync()
        {
            var users = await _unitOfWork.Usuarios.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioResponseDto>>(users);
        }

        public async Task<UsuarioResponseDto?> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Usuarios.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UsuarioResponseDto>(user);
        }

        public async Task<(UsuarioResponseDto? Created, ErrorResponseDto? Error)> CreateAsync(UsuarioRequestDto request)
        {
            var existingUser = await _unitOfWork.Usuarios.GetByEmailAsync(request.Email);
            if (existingUser != null)
                return (null, ErrorMessages.UsuarioYaExiste);

            var usuario = _mapper.Map<Usuario>(request);
            usuario.PasswordHash = PasswordHasher.ComputeSha256Hash(request.Password);
            usuario.Rol = (RolUsuario)request.Rol;
            usuario.IsDeleted = false;

            await _unitOfWork.Usuarios.AddAsync(usuario);
            await _unitOfWork.SaveChangesAsync();

            var createdDto = _mapper.Map<UsuarioResponseDto>(usuario);
            return (createdDto, null);
        }

        public async Task<(bool Success, ErrorResponseDto? Error)> UpdateAsync(int id, UsuarioRequestDto request)
        {
            var user = await _unitOfWork.Usuarios.GetByIdAsync(id);
            if (user == null)
                return (false, null);

            user.Username = request.Username;
            user.Email = request.Email;
            user.Rol = (RolUsuario)request.Rol;
            if (!string.IsNullOrWhiteSpace(request.Password))
                user.PasswordHash = PasswordHasher.ComputeSha256Hash(request.Password);

            await _unitOfWork.Usuarios.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return (true, null);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _unitOfWork.Usuarios.GetByIdAsync(id);
            if (user == null)
                return false;

            await _unitOfWork.Usuarios.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}