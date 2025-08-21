using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Infrastructure.Repositories
{
    public class ProfesionalRepository : IProfesionalRepository
    {
        private readonly HealthcareDbContext _context;

        public ProfesionalRepository(HealthcareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profesional>> GetAllAsync()
        {
            return await _context.Profesionales.ToListAsync();
        }

        public async Task<Profesional?> GetByIdAsync(int id)
        {
            return await _context.Profesionales.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Profesional profesional)
        {
            await _context.Profesionales.AddAsync(profesional);
        }

        public void Update(Profesional profesional)
        {
            _context.Profesionales.Update(profesional);
        }

        public void Delete(Profesional profesional)
        {
            profesional.IsDeleted = true;
            _context.Profesionales.Update(profesional);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}