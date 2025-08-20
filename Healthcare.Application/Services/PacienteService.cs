using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Application.Services
{
    public class PacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
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
    }
}