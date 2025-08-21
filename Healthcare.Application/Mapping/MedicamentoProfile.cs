using AutoMapper;
using Healthcare.Domain.Entities;

namespace Healthcare.Application.Mapping
{
    public class MedicamentoProfile : Profile
    {
        public MedicamentoProfile()
        {
            CreateMap<Medicamento, Medicamento>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}