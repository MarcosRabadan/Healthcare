using AutoMapper;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class AnamnesisProfile : Profile
    {
        public AnamnesisProfile()
        {
            CreateMap<Anamnesis, Anamnesis>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Consulta, opt => opt.Ignore());
        }
    }
}