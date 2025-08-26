using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class MedicamentoProfile : Profile
    {
        public MedicamentoProfile()
        {
            CreateMap<MedicamentoRequestDto, Medicamento>();

            CreateMap<Medicamento, MedicamentoResponseDto>();
        }
    }
}