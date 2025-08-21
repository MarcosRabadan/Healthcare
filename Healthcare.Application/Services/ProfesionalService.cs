using AutoMapper;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Application.Services
{
    public class ProfesionalService
    {
        private readonly IProfesionalRepository _profesionalRepository;
        private readonly IMapper _mapper;

        public ProfesionalService(IProfesionalRepository profesionalRepository, IMapper mapper)
        {
            _profesionalRepository = profesionalRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Profesional>> GetAllAsync()
        {
            return await _profesionalRepository.GetAllAsync();
        }

        public async Task<Profesional?> GetByIdAsync(int id)
        {
            return await _profesionalRepository.GetByIdAsync(id);
        }

        public async Task<Profesional> CreateAsync(Profesional profesional)
        {
            await _profesionalRepository.AddAsync(profesional);
            await _profesionalRepository.SaveChangesAsync();
            return profesional;
        }

        public async Task<bool> UpdateAsync(int id, Profesional profesional)
        {
            var existing = await _profesionalRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(profesional, existing);
            _profesionalRepository.Update(existing);
            await _profesionalRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profesional = await _profesionalRepository.GetByIdAsync(id);
            if (profesional == null)
                return false;

            profesional.IsDeleted = true;
            _profesionalRepository.Update(profesional);
            await _profesionalRepository.SaveChangesAsync();
            return true;
        }
    }
}