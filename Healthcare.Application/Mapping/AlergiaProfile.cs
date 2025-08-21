using AutoMapper;
using Healthcare.Domain.Entities;
using Healthcare.Application.DTOs;

namespace Healthcare.Application.Mapping
{
    public class AlergiaProfile : Profile
    {
        public AlergiaProfile()
        {
            CreateMap<Alergia, AlergiaDto>().ReverseMap();
        }
    }
}