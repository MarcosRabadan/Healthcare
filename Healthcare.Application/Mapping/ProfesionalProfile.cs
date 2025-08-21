using AutoMapper;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class ProfesionalProfile : Profile
    {
        public ProfesionalProfile()
        {
            CreateMap<Profesional, Profesional>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Citas, opt => opt.Ignore())
                .ForMember(dest => dest.Consultas, opt => opt.Ignore());
        }
    }
}