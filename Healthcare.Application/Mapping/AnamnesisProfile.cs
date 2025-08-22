using AutoMapper;
using Healthcare.Domain.Entities;
using Healthcare.Application.DTOs;

namespace Healthcare.Application.Mapping
{
    public class AnamnesisProfile : Profile
    {
        public AnamnesisProfile()
        {
            CreateMap<Anamnesis, AnamnesisDto>().ReverseMap();
        }
    }
}