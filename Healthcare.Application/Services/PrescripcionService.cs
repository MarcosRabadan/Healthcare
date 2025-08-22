using AutoMapper;
using Healthcare.Application.DTOs;
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

        public async Task<IEnumerable<PrescripcionDto>> GetAllAsync()
        {
            var prescripciones = await _prescripcionRepository.GetAllAsync();   
            return _mapper.Map<IEnumerable<PrescripcionDto>>(prescripciones);
        }

        public async Task<PrescripcionDto?> GetByIdAsync(int id)
        {
            var prescripcion = await _prescripcionRepository.GetByIdAsync(id);
            return _mapper.Map<PrescripcionDto?>(prescripcion);
        }

        public async Task<PrescripcionDto> CreateAsync(PrescripcionDto prescripcionDto)
        {
            var prescripcion = _mapper.Map<Prescripcion>(prescripcionDto);  
            await _prescripcionRepository.AddAsync(prescripcion);
            await _prescripcionRepository.SaveChangesAsync();
            return _mapper.Map<PrescripcionDto>(prescripcion);  
        }

        public async Task<bool> UpdateAsync(int id, PrescripcionDto prescripcion)
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