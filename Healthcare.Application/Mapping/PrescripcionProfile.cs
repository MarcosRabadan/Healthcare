using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class PrescripcionProfile : Profile
    {
        public PrescripcionProfile()
        {
            CreateMap<PrescripcionRequestDto, Prescripcion>();

            CreateMap<Prescripcion, PrescripcionResponseDto>();
        }
    }
}