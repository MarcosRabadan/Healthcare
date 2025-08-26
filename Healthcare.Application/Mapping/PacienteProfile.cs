using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class PacienteProfile : Profile
    {
        public PacienteProfile()
        {
            CreateMap<PacienteRequestDto, Paciente>();

            CreateMap<Paciente, PacienteResponseDto>();
        }
    }
}