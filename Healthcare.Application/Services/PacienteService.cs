using AutoMapper;
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

        public async Task<IEnumerable<Paciente>> GetAllAsync()
        {
            return await _pacienteRepository.GetAllAsync();
        }


        public async Task<Paciente?> GetByIdAsync(int id)
        {
            return await _pacienteRepository.GetByIdAsync(id);
        }


        public async Task<Paciente> CreateAsync(Paciente paciente)
        {
            await _pacienteRepository.AddAsync(paciente);
            await _pacienteRepository.SaveChangesAsync();
            return paciente;
        }


        public async Task<bool> UpdateAsync(int id, Paciente paciente)
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