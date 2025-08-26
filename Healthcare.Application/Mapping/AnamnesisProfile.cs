using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class AnamnesisProfile : Profile
    {
        public AnamnesisProfile()
        {
            CreateMap<AnamnesisRequestDto, Anamnesis>();

            CreateMap<Anamnesis, AnamnesisResponseDto>();
        }
    }
}