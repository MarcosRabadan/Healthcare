using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface ICitaRepository
    {
        IQueryable<Cita> GetAll();
        Task<Cita?> GetByIdAsync(int id);
        Task AddAsync(Cita cita);
        void Update(Cita cita);
        void Delete(Cita cita);
    }
}