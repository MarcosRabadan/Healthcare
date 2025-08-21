using AutoMapper;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class ConsultaProfile : Profile
    {
        public ConsultaProfile()
        {
            CreateMap<Consulta, Consulta>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Paciente, opt => opt.Ignore())
                .ForMember(dest => dest.Profesional, opt => opt.Ignore());
        }
    }
}