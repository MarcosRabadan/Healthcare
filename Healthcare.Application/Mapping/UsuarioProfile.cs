using AutoMapper;
using Healthcare.Application.DTOs.Enums;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Enums;

namespace Healthcare.Application.Mapping
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<RegisterRequestDto, Usuario>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => (RolUsuario)src.Rol))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));
            CreateMap<UsuarioRequestDto, Usuario>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<Usuario, UsuarioResponseDto>();
        }
    }
}