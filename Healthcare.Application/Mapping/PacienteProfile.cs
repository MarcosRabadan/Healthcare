using AutoMapper;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class PacienteProfile : Profile
    {
        public PacienteProfile()
        {
            CreateMap<Paciente, Paciente>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Citas, opt => opt.Ignore())
                .ForMember(dest => dest.Prescripciones, opt => opt.Ignore())
                .ForMember(dest => dest.Consultas, opt => opt.Ignore())
                .ForMember(dest => dest.Alergias, opt => opt.Ignore());
        }
    }
}