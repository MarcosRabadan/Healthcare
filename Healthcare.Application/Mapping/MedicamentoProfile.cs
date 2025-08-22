using AutoMapper;
using Healthcare.Domain.Entities;
using Healthcare.Application.DTOs;

namespace Healthcare.Application.Mapping
{
    public class MedicamentoProfile : Profile
    {
        public MedicamentoProfile()
        {
            CreateMap<Medicamento, MedicamentoDto>().ReverseMap();
        }
    }
}