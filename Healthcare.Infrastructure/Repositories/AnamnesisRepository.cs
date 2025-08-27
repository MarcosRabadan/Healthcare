using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Infrastructure.Repositories
{
    public class AnamnesisRepository : IAnamnesisRepository
    {
        private readonly HealthcareDbContext _context;

        public AnamnesisRepository(HealthcareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Anamnesis>> GetAllAsync()
        {
            return await _context.Anamnesis
                .Include(a => a.Consulta)
                .ToListAsync();
        }

        public async Task<Anamnesis?> GetByIdAsync(int id)
        {
            return await _context.Anamnesis
                .Include(a => a.Consulta)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Anamnesis anamnesis)
        {
            await _context.Anamnesis.AddAsync(anamnesis);
        }

        public void Update(Anamnesis anamnesis)
        {
            _context.Anamnesis.Update(anamnesis);
        }

        public void Delete(Anamnesis anamnesis)
        {
            anamnesis.IsDeleted = true;
            _context.Anamnesis.Update(anamnesis);
        }
    }
}