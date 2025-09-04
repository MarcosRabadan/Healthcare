using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Infrastructure.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly HealthcareDbContext _context;

        public ConsultaRepository(HealthcareDbContext context)
        {
            _context = context;
        }

        public IQueryable<Consulta> GetAll()
        {
            return _context.Consultas
                .Include(c => c.Paciente)
                .Include(c => c.Profesional)
                .AsQueryable();
        }

        public async Task<Consulta?> GetByIdAsync(int id)
        {
            return await _context.Consultas
                .Include(c => c.Paciente)
                .Include(c => c.Profesional)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Consulta consulta)
        {
            await _context.Consultas.AddAsync(consulta);
        }

        public void Update(Consulta consulta)
        {
            _context.Consultas.Update(consulta);
        }

        public void Delete(Consulta consulta)
        {
            consulta.IsDeleted = true;
            _context.Consultas.Update(consulta);
        }
    }
}