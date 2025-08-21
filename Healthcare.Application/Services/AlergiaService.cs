using AutoMapper;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using System.Threading.Tasks;

namespace Healthcare.Application.Services
{
    public class AlergiaService
    {
        private readonly IAlergiaRepository _alergiaRepository;
        private readonly IMapper _mapper;

        public AlergiaService(IAlergiaRepository alergiaRepository, IMapper mapper)
        {
            _alergiaRepository = alergiaRepository;
            _mapper = mapper;
        }

        public async Task<Alergia?> GetByIdAsync(int id)
        {
            return await _alergiaRepository.GetByIdAsync(id);
        }

        public async Task<Alergia> CreateAsync(Alergia alergia)
        {
            await _alergiaRepository.AddAsync(alergia);
            await _alergiaRepository.SaveChangesAsync();
            return alergia;
        }

        public async Task<bool> UpdateAsync(int id, Alergia alergia)
        {
            var existing = await _alergiaRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(alergia, existing);

            _alergiaRepository.Update(existing);
            await _alergiaRepository.SaveChangesAsync();
            return true;
        }
    }
}