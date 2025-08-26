using AutoMapper;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
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

        public async Task<IEnumerable<ProfesionalResponseDto>> GetAllAsync()
        {
            var profesionales = await _profesionalRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProfesionalResponseDto>>(profesionales);
        }

        public async Task<ProfesionalResponseDto?> GetByIdAsync(int id)
        {
            var profesional = await _profesionalRepository.GetByIdAsync(id);
            return profesional == null ? null : _mapper.Map<ProfesionalResponseDto?>(profesional);
        }

        public async Task<ProfesionalResponseDto> CreateAsync(ProfesionalRequestDto profesionalDto)
        {
            var profesional = _mapper.Map<Profesional>(profesionalDto);
            await _profesionalRepository.AddAsync(profesional);
            await _profesionalRepository.SaveChangesAsync();
            return _mapper.Map<ProfesionalResponseDto>(profesional);
        }

        public async Task<bool> UpdateAsync(int id, ProfesionalRequestDto profesional)
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