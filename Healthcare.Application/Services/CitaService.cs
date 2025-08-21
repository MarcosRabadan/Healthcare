using AutoMapper;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Application.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _citaRepository;
        private readonly IMapper _mapper;

        public CitaService(ICitaRepository citaRepository, IMapper mapper)
        {
            _citaRepository = citaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Cita>> GetAllAsync()
        {
            return await _citaRepository.GetAllAsync();
        }

        public async Task<Cita?> GetByIdAsync(int id)
        {
            return await _citaRepository.GetByIdAsync(id);
        }

        public async Task<Cita> CreateAsync(Cita cita)
        {
            await _citaRepository.AddAsync(cita);
            await _citaRepository.SaveChangesAsync();
            return cita;
        }

        public async Task<bool> UpdateAsync(int id, Cita cita)
        {
            var existing = await _citaRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(cita, existing);
            _citaRepository.Update(existing);
            await _citaRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cita = await _citaRepository.GetByIdAsync(id);
            if (cita == null)
                return false;

            cita.IsDeleted = true;
            _citaRepository.Update(cita);
            await _citaRepository.SaveChangesAsync();
            return true;
        }
    }
}