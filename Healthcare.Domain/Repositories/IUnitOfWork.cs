using Healthcare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IPacienteRepository Pacientes { get; }
        IPrescripcionRepository Prescripciones { get; }
        IMedicamentoRepository Medicamentos { get; }
        IAlergiaRepository Alergias { get; }
        IConsultaRepository Consultas { get; }
        ICitaRepository Citas { get; }
        IAnamnesisRepository Anamnesis { get; }
        IProfesionalRepository Profesionales { get; }
        Task<int> SaveChangesAsync();
    }
}