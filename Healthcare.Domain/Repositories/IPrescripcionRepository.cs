using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface IPrescripcionRepository
    {
        IQueryable<Prescripcion> GetAll();
        Task<Prescripcion?> GetByIdAsync(int id);
        Task AddAsync(Prescripcion prescripcion);
        void Update(Prescripcion prescripcion);
        void Delete(Prescripcion prescripcion);
    }
}