using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class ConsultaProfile : Profile
    {
        public ConsultaProfile()
        {
            CreateMap<ConsultaRequestDto, Consulta>();

            CreateMap<Consulta, ConsultaResponseDto>();
        }
    }
}