using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface IAnamnesisRepository
    {
        Task<IEnumerable<Anamnesis>> GetAllAsync();
        Task<Anamnesis?> GetByIdAsync(int id);
        Task AddAsync(Anamnesis anamnesis);
        void Update(Anamnesis anamnesis);
        void Delete(Anamnesis anamnesis);
    }
}