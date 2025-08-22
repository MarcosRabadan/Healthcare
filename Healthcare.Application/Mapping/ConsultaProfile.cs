using AutoMapper;
using Healthcare.Application.DTOs;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class ConsultaProfile : Profile
    {
        public ConsultaProfile()
        {
            CreateMap<Consulta, ConsultaDto>().ReverseMap();
        }
    }
}