using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly HealthcareDbContext _context;

        public PacienteRepository(HealthcareDbContext context)
        {
            _context = context;
        }

        public IQueryable<Paciente> GetAll()
        {
            return _context.Pacientes.AsQueryable();
        }

        public async Task<Paciente?> GetByIdAsync(int id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task AddAsync(Paciente paciente)
        {
            await _context.Pacientes.AddAsync(paciente);
        }

        public void Update(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
        }


        public void Delete(Paciente paciente)
        {
            _context.Pacientes.Remove(paciente);
        }

        public async Task<bool> ExistNumeroHistoriaClinicaAsync(string code)
        {
            return await _context.Pacientes.AnyAsync(p => p.NumeroHistoriaClinica == code);
        }
        public async Task<bool> ExistEmailAsync(string email)
        {
            return await _context.Pacientes.AnyAsync(p => p.Email == email);
        }
    }
}