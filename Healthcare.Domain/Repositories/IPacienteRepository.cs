using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface IPacienteRepository
    {
        Task<IEnumerable<Paciente>> GetAllAsync();
        Task<Paciente?> GetByIdAsync(int id);
        Task AddAsync(Paciente paciente);
        void Update(Paciente paciente);
        void Delete(Paciente paciente);
        Task SaveChangesAsync();
        Task<bool> ExistNumeroHistoriaClinicaAsync(string id);
        Task<bool> ExistEmailAsync(string email);
    }
}