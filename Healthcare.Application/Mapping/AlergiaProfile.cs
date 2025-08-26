using AutoMapper;
using Healthcare.Application.DTOs;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Enums;

namespace Healthcare.Application.Mapping
{
    public class AlergiaProfile : Profile
    {
        public AlergiaProfile()
        {
            CreateMap<AlergiaRequestDto, Alergia>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (TipoAlergia)src.Tipo.Value));

            CreateMap<Alergia, AlergiaResponseDto>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => new EnumValueDto
                {
                    Value = (int)src.Tipo,
                    Name = src.Tipo.ToString()
                }));

            CreateMap<TipoAlergia, EnumValueDto>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => (int)src))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()));

            CreateMap<EnumValueDto, TipoAlergia>()
                .ConvertUsing(dto => (TipoAlergia)dto.Value);
        }
    }
}