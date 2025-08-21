using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface IAlergiaRepository
    {
        Task<Alergia?> GetByIdAsync(int id);
        Task AddAsync(Alergia alergia);
        void Update(Alergia alergia);
        void Delete(Alergia alergia);
        Task SaveChangesAsync();
    }
}