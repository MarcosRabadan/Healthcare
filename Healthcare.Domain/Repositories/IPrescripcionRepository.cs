using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface IPrescripcionRepository
    {
        Task<IEnumerable<Prescripcion>> GetAllAsync();
        Task<Prescripcion?> GetByIdAsync(int id);
        Task AddAsync(Prescripcion prescripcion);
        void Update(Prescripcion prescripcion);
        void Delete(Prescripcion prescripcion);
    }
}