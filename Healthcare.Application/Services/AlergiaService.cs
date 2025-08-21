using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using System.Threading.Tasks;

namespace Healthcare.Application.Services
{
    public class AlergiaService
    {
        private readonly IAlergiaRepository _alergiaRepository;

        public AlergiaService(IAlergiaRepository alergiaRepository)
        {
            _alergiaRepository = alergiaRepository;
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
    }
}