using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Healthcare.Infrastructure.Repositories
{
    public class AlergiaRepository : IAlergiaRepository
    {
        private readonly HealthcareDbContext _context;

        public AlergiaRepository(HealthcareDbContext context)
        {
            _context = context;
        }

        public async Task<Alergia?> GetByIdAsync(int id)
        {
            return await _context.Alergias
                .Include(a => a.Paciente)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Alergia alergia)
        {
            await _context.Alergias.AddAsync(alergia);
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}