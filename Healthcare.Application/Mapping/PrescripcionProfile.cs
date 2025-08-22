using AutoMapper;
using Healthcare.Domain.Entities;
using Healthcare.Application.DTOs;

namespace Healthcare.Application.Mapping
{
    public class PrescripcionProfile : Profile
    {
        public PrescripcionProfile()
        {
            CreateMap<Prescripcion, PrescripcionDto>().ReverseMap();
        }
    }
}