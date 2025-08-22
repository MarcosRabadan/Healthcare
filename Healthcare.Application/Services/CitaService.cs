using AutoMapper;
using Healthcare.Application.DTOs;
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

        public async Task<IEnumerable<CitaDto>> GetAllAsync()
        {
            var citas = await _citaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CitaDto>>(citas);
        }

        public async Task<CitaDto?> GetByIdAsync(int id)
        {
            var cita = await _citaRepository.GetByIdAsync(id);
            return cita == null ? null : _mapper.Map<CitaDto>(cita);
        }

        public async Task<CitaDto> CreateAsync(CitaDto citaDto)
        {
            var cita = _mapper.Map<Cita>(citaDto);
            await _citaRepository.AddAsync(cita);
            await _citaRepository.SaveChangesAsync();
            return citaDto;
        }

        public async Task<bool> UpdateAsync(int id, CitaDto cita)
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