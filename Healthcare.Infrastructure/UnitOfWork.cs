using Healthcare.Domain.Repositories;

namespace Healthcare.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HealthcareDbContext _context;

        public IPacienteRepository Pacientes { get; }
        public IPrescripcionRepository Prescripciones { get; }
        public IMedicamentoRepository Medicamentos { get; }
        public IAlergiaRepository Alergias { get; }
        public IConsultaRepository Consultas { get; }
        public ICitaRepository Citas { get; }
        public IAnamnesisRepository Anamnesis { get; }
        public IProfesionalRepository Profesionales { get; }

        public UnitOfWork(
            HealthcareDbContext context,
            IPacienteRepository pacientes,
            IPrescripcionRepository prescripciones,
            IMedicamentoRepository medicamentos,
            IAlergiaRepository alergias,
            IConsultaRepository consultas,
            ICitaRepository citas,
            IAnamnesisRepository anamnesis,
            IProfesionalRepository profesionales)
        {
            _context = context;
            Pacientes = pacientes;
            Prescripciones = prescripciones;
            Medicamentos = medicamentos;
            Alergias = alergias;
            Consultas = consultas;
            Citas = citas;
            Anamnesis = anamnesis;
            Profesionales = profesionales;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
