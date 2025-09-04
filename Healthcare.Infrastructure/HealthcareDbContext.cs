using Microsoft.EntityFrameworkCore;
using Healthcare.Domain.Entities;

namespace Healthcare.Infrastructure
{
    public class HealthcareDbContext : DbContext
    {
        public HealthcareDbContext(DbContextOptions<HealthcareDbContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Prescripcion> Prescripciones { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Alergia> Alergias { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Anamnesis> Anamnesis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Cita>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Prescripcion>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Consulta>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Alergia>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Medicamento>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Profesional>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Anamnesis>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Usuario>().HasQueryFilter(u => !u.IsDeleted);
            
        }
    }
}
