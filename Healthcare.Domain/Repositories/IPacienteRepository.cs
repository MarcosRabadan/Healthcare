using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface IPacienteRepository
    {
        IQueryable<Paciente> GetAll();
        Task<Paciente?> GetByIdAsync(int id);
        Task AddAsync(Paciente paciente);
        void Update(Paciente paciente);
        void Delete(Paciente paciente);
        Task<bool> ExistNumeroHistoriaClinicaAsync(string id);
        Task<bool> ExistEmailAsync(string email);
    }
}