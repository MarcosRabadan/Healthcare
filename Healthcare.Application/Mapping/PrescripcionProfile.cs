using AutoMapper;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class PrescripcionProfile : Profile
    {
        public PrescripcionProfile()
        {
            CreateMap<Prescripcion, Prescripcion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Paciente, opt => opt.Ignore())
                .ForMember(dest => dest.Medicamento, opt => opt.Ignore());
        }
    }
}