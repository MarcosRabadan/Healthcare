using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly HealthcareDbContext _context;

        public UsuarioRepository(HealthcareDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            return await _context.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.Username == username && !u.IsDeleted);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Set<Usuario>()
                .Where(u => !u.IsDeleted)
                .ToListAsync();
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Set<Usuario>().AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Set<Usuario>().Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Set<Usuario>().FindAsync(id);
            if (usuario != null && !usuario.IsDeleted)
            {
                usuario.IsDeleted = true;
                _context.Set<Usuario>().Update(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public Task<Usuario?> GetByEmailAsync(string email)
        {
            return _context.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);

        }
    }
}