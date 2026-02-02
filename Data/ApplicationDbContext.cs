using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SisGestionCitasMedicas.Models;
using System.Numerics;

namespace SisGestionCitasMedicas.Data
{
    public class HospitalDbContext : IdentityDbContext<ApplicationUser>
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        public DbSet<Empresa> Empresas => Set<Empresa>();
        public DbSet<Paciente> Pacientes => Set<Paciente>();
        public DbSet<Doctor> Doctores => Set<Doctor>();
        public DbSet<Cita> Citas => Set<Cita>();

        public DbSet<Tabla> Tablas => Set<Tabla>();
        public DbSet<Catalogo> Catalogos => Set<Catalogo>();

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



            modelBuilder.Entity<Paciente>()
               .HasOne(p => p.Empresas)
               .WithMany(e => e.Pacientes)
               .HasForeignKey(p => p.EmpresaId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Empresas)
                .WithMany(e => e.Doctores)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Empresas)
                .WithMany(e => e.Citas)
                .HasForeignKey(c => c.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Catalogo>()
                .HasOne(c => c.Tabla)          // Navegación hacia Tabla
                .WithMany(t => t.Catalogos)   // Tabla tiene muchos Catalogos
                .HasForeignKey(c => c.TablaId) // FK en Catalogo
                .OnDelete(DeleteBehavior.Restrict); // Evita borrado en cascada



        }
    }
}
