using AutoMapper;
using Healthcare.Domain.Entities;
using Healthcare.Application.DTOs;

namespace Healthcare.Application.Mapping
{
    public class CitaProfile : Profile
    {
        public CitaProfile()
        {
            CreateMap<Cita, CitaDto>().ReverseMap();
        }
    }
}