using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface IAlergiaRepository
    {
        Task<Alergia?> GetByIdAsync(int id);
        IQueryable<Alergia> GetAll();
        Task AddAsync(Alergia alergia);
        void Update(Alergia alergia);
        void Delete(Alergia alergia);
    }
}