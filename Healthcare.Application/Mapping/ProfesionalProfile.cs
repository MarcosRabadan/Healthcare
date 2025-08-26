using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class ProfesionalProfile : Profile
    {
        public ProfesionalProfile()
        {
            CreateMap<ProfesionalRequestDto, Profesional>();

            CreateMap<Profesional, ProfesionalResponseDto>();
        }
    }
}