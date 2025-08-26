using AutoMapper;
using Healthcare.Application.Constants;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Enums;
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

        public async Task<IEnumerable<CitaResponseDto>> GetAllAsync()
        {
            var citas = await _citaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CitaResponseDto>>(citas);
        }

        public async Task<CitaResponseDto?> GetByIdAsync(int id)
        {
            var cita = await _citaRepository.GetByIdAsync(id);
            return cita == null ? null : _mapper.Map<CitaResponseDto>(cita);
        }

        public async Task<(CitaResponseDto? Created, ErrorResponseDto? Error)> CreateAsync(CitaRequestDto citaDto)
        {
            if (!Enum.IsDefined(typeof(EstadoCita), citaDto.Estado.Value))
            {
                return (null, ErrorMessages.EstadoCitaInvalido);
            }

            var cita = _mapper.Map<Cita>(citaDto);
            await _citaRepository.AddAsync(cita);
            await _citaRepository.SaveChangesAsync();
            var createdDto = _mapper.Map<CitaResponseDto>(cita);
            return (createdDto, null);
        }

        public async Task<bool> UpdateAsync(int id, CitaRequestDto cita)
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