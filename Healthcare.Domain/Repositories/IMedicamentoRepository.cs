using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface IMedicamentoRepository
    {
        Task<IEnumerable<Medicamento>> GetAllAsync();
        Task<Medicamento?> GetByIdAsync(int id);
        Task AddAsync(Medicamento medicamento);
        void Update(Medicamento medicamento);
        void Delete(Medicamento medicamento);
    }
}