using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
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