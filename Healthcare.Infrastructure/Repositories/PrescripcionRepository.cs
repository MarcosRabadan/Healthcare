using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Infrastructure.Repositories
{
    public class PrescripcionRepository : IPrescripcionRepository
    {
        private readonly HealthcareDbContext _context;

        public PrescripcionRepository(HealthcareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prescripcion>> GetAllAsync()
        {
            return await _context.Prescripciones
                .Include(p => p.Paciente)
                .Include(p => p.Medicamento)
                .ToListAsync();
        }

        public async Task<Prescripcion?> GetByIdAsync(int id)
        {
            return await _context.Prescripciones
                .Include(p => p.Paciente)
                .Include(p => p.Medicamento)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Prescripcion prescripcion)
        {
            await _context.Prescripciones.AddAsync(prescripcion);
        }

        public void Update(Prescripcion prescripcion)
        {
            _context.Prescripciones.Update(prescripcion);
        }

        public void Delete(Prescripcion prescripcion)
        {
            prescripcion.IsDeleted = true;
            _context.Prescripciones.Update(prescripcion);
        }       
    }
}