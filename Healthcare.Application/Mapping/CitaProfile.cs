using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class CitaProfile : Profile
    {
        public CitaProfile()
        {
            CreateMap<CitaRequestDto, Cita>();

            CreateMap<Cita, CitaResponseDto>();
        }
    }
}