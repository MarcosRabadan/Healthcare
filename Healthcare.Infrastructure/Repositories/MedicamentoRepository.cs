using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Infrastructure.Repositories
{
    public class MedicamentoRepository : IMedicamentoRepository
    {
        private readonly HealthcareDbContext _context;

        public MedicamentoRepository(HealthcareDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medicamento>> GetAllAsync()
        {
            return await _context.Medicamentos.ToListAsync();
        }

        public async Task<Medicamento?> GetByIdAsync(int id)
        {
            return await _context.Medicamentos.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Medicamento medicamento)
        {
            await _context.Medicamentos.AddAsync(medicamento);
        }

        public void Update(Medicamento medicamento)
        {
            _context.Medicamentos.Update(medicamento);
        }

        public void Delete(Medicamento medicamento)
        {
            medicamento.IsDeleted = true;
            _context.Medicamentos.Update(medicamento);
        }
    }
}