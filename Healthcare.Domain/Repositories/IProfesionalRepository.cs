using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface IProfesionalRepository
    {
        Task<IEnumerable<Profesional>> GetAllAsync();
        Task<Profesional?> GetByIdAsync(int id);
        Task AddAsync(Profesional profesional);
        void Update(Profesional profesional);
        void Delete(Profesional profesional);
    }
}