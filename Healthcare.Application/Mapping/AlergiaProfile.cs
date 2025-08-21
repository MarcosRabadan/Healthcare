using AutoMapper;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class AlergiaProfile : Profile
    {
        public AlergiaProfile()
        {
            CreateMap<Alergia, Alergia>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Paciente, opt => opt.Ignore()); 
        }
    }
}