using AutoMapper;
using Healthcare.Domain.Entities;
using Healthcare.Application.DTOs;

namespace Healthcare.Application.Mapping
{
    public class PacienteProfile : Profile
    {
        public PacienteProfile()
        {
            CreateMap<Paciente, PacienteDto>().ReverseMap();
        }
    }
}