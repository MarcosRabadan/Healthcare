using AutoMapper;
using Healthcare.Application.DTOs;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Application.Services
{
    public class PacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;
        public PacienteService(IPacienteRepository pacienteRepository, IMapper mapper)
        {
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PacienteDto>> GetAllAsync()
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PacienteDto>>(pacientes);
        }


        public async Task<PacienteDto?> GetByIdAsync(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);
            return paciente == null ? null : _mapper.Map<PacienteDto?>(paciente);
        }


        public async Task<PacienteDto> CreateAsync(PacienteDto pacienteDto)
        {
            var paciente = _mapper.Map<Paciente>(pacienteDto);
            await _pacienteRepository.AddAsync(paciente);
            await _pacienteRepository.SaveChangesAsync();
            return _mapper.Map<PacienteDto>(paciente);
        }


        public async Task<bool> UpdateAsync(int id, PacienteDto paciente)
        {
            var existing = await _pacienteRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(paciente, existing);

            _pacienteRepository.Update(existing);
            await _pacienteRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(id);

            if (paciente == null)
                return false;

            paciente.IsDeleted = true;
            _pacienteRepository.Update(paciente);

            await _pacienteRepository.SaveChangesAsync();
            return true;
        }
    }
}