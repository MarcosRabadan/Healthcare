using AutoMapper;
using Healthcare.Domain.Entities;
using Healthcare.Application.DTOs;

namespace Healthcare.Application.Mapping
{
    public class ProfesionalProfile : Profile
    {
        public ProfesionalProfile()
        {
            CreateMap<Profesional, ProfesionalDto>().ReverseMap();
        }
    }
}