using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Models;

namespace SaasClinicas.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Clinic> Clinics => Set<Clinic>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Professional> Professionals => Set<Professional>();
    public DbSet<ClinicPatient> ClinicPatients => Set<ClinicPatient>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClinicPatient>().HasKey(cp => new { cp.ClinicId, cp.PatientId });

        modelBuilder.Entity<Clinic>().HasMany(c => c.Users).WithOne(u => u.Clinic).HasForeignKey(u => u.ClinicId);

        modelBuilder.Entity<Clinic>().HasMany(c => c.Professionals).WithOne(p => p.Clinic).HasForeignKey(p => p.ClinicId);

        base.OnModelCreating(modelBuilder);
    }

}