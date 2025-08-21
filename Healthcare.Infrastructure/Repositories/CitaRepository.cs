using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Infrastructure.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly HealthcareDbContext _context;

        public CitaRepository(HealthcareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cita>> GetAllAsync()
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Profesional)
                .ToListAsync();
        }

        public async Task<Cita?> GetByIdAsync(int id)
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Profesional)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Cita cita)
        {
            await _context.Citas.AddAsync(cita);
        }

        public void Update(Cita cita)
        {
            _context.Citas.Update(cita);
        }

        public void Delete(Cita cita)
        {
            cita.IsDeleted = true;
            _context.Citas.Update(cita);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}