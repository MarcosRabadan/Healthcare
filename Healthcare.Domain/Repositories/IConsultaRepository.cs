using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface IConsultaRepository
    {
        Task<IEnumerable<Consulta>> GetAllAsync();
        Task<Consulta?> GetByIdAsync(int id);
        Task AddAsync(Consulta consulta);
        void Update(Consulta consulta);
        void Delete(Consulta consulta);
    }
}