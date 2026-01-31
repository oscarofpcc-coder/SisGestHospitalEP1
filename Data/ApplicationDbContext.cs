using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SisGestionCitasMedicas.Models;
using System.Numerics;

namespace SisGestionCitasMedicas.Data
{
    public class HospitalDbContext : IdentityDbContext<ApplicationUser>
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes => Set<Paciente>();
        public DbSet<Doctor> Doctores => Set<Doctor>();
        public DbSet<Cita> Citas => Set<Cita>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // ✅ obligatorio

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Citas)
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Doctor)
                .WithMany(d => d.Citas)
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
