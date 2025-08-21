using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface IAlergiaRepository
    {
        Task AddAsync(Alergia alergia);
        Task SaveChangesAsync();
    }
}