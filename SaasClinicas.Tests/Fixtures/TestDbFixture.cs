using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Models;

public class TestDbFixture : IDisposable
{
    public ApplicationDbContext Context { get; private set; }

    public TestDbFixture()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Context = new ApplicationDbContext(options);

        Seed();
    }

    private void Seed()
    {
        Context.Clinics.Add(new Clinic
        {
            Id = 1,
            ClinicName = "Clinica Teste",
            Email = "Teste@gmail.com",
            Phone = "16997640015",
            ResponsibleName = "Teste agasda"
        });

        Context.SaveChanges();
        Context.ChangeTracker.Clear();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }

    public void ClearDatabase()
    {

        Context.ClinicPatients.RemoveRange(Context.ClinicPatients);
        Context.Patients.RemoveRange(Context.Patients);
        Context.Users.RemoveRange(Context.Users);
        Context.SaveChanges();
        Context.ChangeTracker.Clear();
    }
}