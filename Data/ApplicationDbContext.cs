using Microsoft.EntityFrameworkCore;
using SaasClinicas.APi.Models;

namespace SaasClinicas.APi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Clinic> Clinics => Set<Clinic>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<ClinicPatient> ClinicPatients => Set<ClinicPatient>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClinicPatient>().HasKey(cp => new { cp.ClinicId, cp.PatientId });
        base.OnModelCreating(modelBuilder);
    }

}