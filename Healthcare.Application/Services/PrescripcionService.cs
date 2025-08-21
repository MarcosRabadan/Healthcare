using AutoMapper;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Application.Services
{
    public class PrescripcionService
    {
        private readonly IPrescripcionRepository _prescripcionRepository;
        private readonly IMapper _mapper;

        public PrescripcionService(IPrescripcionRepository prescripcionRepository, IMapper mapper)
        {
            _prescripcionRepository = prescripcionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Prescripcion>> GetAllAsync()
        {
            return await _prescripcionRepository.GetAllAsync();
        }

        public async Task<Prescripcion?> GetByIdAsync(int id)
        {
            return await _prescripcionRepository.GetByIdAsync(id);
        }

        public async Task<Prescripcion> CreateAsync(Prescripcion prescripcion)
        {
            await _prescripcionRepository.AddAsync(prescripcion);
            await _prescripcionRepository.SaveChangesAsync();
            return prescripcion;
        }

        public async Task<bool> UpdateAsync(int id, Prescripcion prescripcion)
        {
            var existing = await _prescripcionRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(prescripcion, existing);
            _prescripcionRepository.Update(existing);
            await _prescripcionRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var prescripcion = await _prescripcionRepository.GetByIdAsync(id);
            if (prescripcion == null)
                return false;

            prescripcion.IsDeleted = true;
            _prescripcionRepository.Update(prescripcion);
            await _prescripcionRepository.SaveChangesAsync();
            return true;
        }
    }
}