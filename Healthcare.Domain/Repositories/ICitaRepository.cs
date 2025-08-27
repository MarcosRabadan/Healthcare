using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface ICitaRepository
    {
        Task<IEnumerable<Cita>> GetAllAsync();
        Task<Cita?> GetByIdAsync(int id);
        Task AddAsync(Cita cita);
        void Update(Cita cita);
        void Delete(Cita cita);
    }
}