using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Patients;
using SaasClinicas.Api.Models;
using SaasClinicas.Api.Validators.Patients;

namespace SaasClinicas.Tests.Validators.Patient;


public class PatientCreateValidatorTests : IClassFixture<TestDbFixture>, IDisposable
{
    private readonly ApplicationDbContext _context;

    private readonly PatientCreateValidator _patientValidator;
    private readonly TestDbFixture _fixture;
    public PatientCreateValidatorTests(TestDbFixture fixture)
    {
        _context = fixture.Context;
        _fixture = fixture;

        _patientValidator = new PatientCreateValidator(_context);


    }
    public void Dispose()
    {
        _fixture.ClearDatabase();
    }

    [Fact]
    public async Task PatientCreateValidator_WithValidData_ReturnsValid()
    {
        var patient = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "41546867805",
            Email = "vinciuscosta@gmail.com",
            Phone = "16997640015",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(patient);

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task PatientCreateValidator_WithNameTooShort_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Ma",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "41546867805",
            Email = "vinciuscosta@gmail.com",
            Phone = "16997640015",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(patient);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public async Task PatientCreateValidator_WithEmptyName_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "41546867805",
            Email = "vinciuscosta@gmail.com",
            Phone = "16997640015",
            ClinicId = 1
        };
        var result = await _patientValidator.ValidateAsync(patient);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public async Task PatientCreateValidator_WithNameTooLong_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Aethelgard-Vaelithrax-ThaAethelgard-Vaelithrax-Thalassios-Hyperion-Kaelenor-Bellerophon-Xylophagos-Zephyrus-Andromache-Lysander-Polyphemus-Bucephalus-Cassiopeia-Ozymandias-Terpsichore-Melpomene-Calliope-Thalia-Euterpe-Erato-Polyhymnia-Urania-Clio-Helioslass32123123123123ios-Hyperion-Kaelenor-Bellerophon-Xylophagos-Zephyrus-Andromache-Lysander-Polyphemus-Bucephalus-Cassiopeia-Ozymandias-Terpsichore-Melpomene-Calliope-Thalia-Euterpe-Erato-Polyhymnia-Urania-Clio-Helios",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "41546867805",
            Email = "vinciuscosta@gmail.com",
            Phone = "16997640015",
            ClinicId = 1
        };
        var result = await _patientValidator.ValidateAsync(patient);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public async Task PatientCreateValidator_WithInvalidEmailFormat_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "41546867805",
            Email = "42141231",
            Phone = "16997640015",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(patient);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public async Task PatientCreateValidator_WithEmptyEmail_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "41546867805",
            Email = "",
            Phone = "16997640015",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(patient);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public async Task PatientCreateValidator_WithDuplicateEmail_ReturnsValidationError()
    {

        var existingPatient = new Api.Models.Patient
        {
            Id = 1,
            Name = "Paciente Antigo",
            Email = "viniciusCosta@gmail.com",
            Cpf = "12345678901",
            Phone = "16999999999",
            Birthday = new DateOnly(1990, 1, 1)
        };
        _context.Patients.Add(existingPatient);

        var clinicPatient = new ClinicPatient
        {
            ClinicId = 1,
            PatientId = 1
        };

        _context.ClinicPatients.Add(clinicPatient);
        await _context.SaveChangesAsync();

        var duplicatePatientDto = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "41546867805",
            Email = "viniciusCosta@gmail.com",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(duplicatePatientDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == "Email");
    }

    [Fact]
    public async Task PatientCreateValidator_WithEmptyPhone_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "41546867805",
            Email = "vinciuscosta@gmail.com",
            Phone = "",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(patient);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Phone");
    }

    [Fact]
    public async Task PatientCreateValidator_WithPhoneTooShort_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "41546867805",
            Email = "vinciuscosta@gmail.com",
            Phone = "169976400",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(patient);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Phone");
    }

    [Fact]
    public async Task PatientCreateValidator_WithPhoneTooLong_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "41546867805",
            Email = "vinciuscosta@gmail.com",
            Phone = "1699764001512",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(patient);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Phone");
    }

    [Fact]
    public async Task PatientCreateValidator_WithFormattedPhone_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "41546867805",
            Email = "vinciuscosta@gmail.com",
            Phone = "(16) 99764-0015",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(patient);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Phone");
    }

    [Fact]
    public async Task PatientCreateValidator_WithEmptyCpf_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "",
            Email = "vinciuscosta@gmail.com",
            Phone = "16997640015",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(patient);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Cpf");
    }

    [Fact]
    public async Task PatientCreateValidator_WithCpfContainingLetters_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "4154s867805",
            Email = "vinciuscosta@gmail.com",
            Phone = "16997640015",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(patient);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Cpf");
    }

    [Fact]
    public async Task PatientCreateValidator_WithFormattedCpf_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "415.468.678-05",
            Email = "vinciuscosta@gmail.com",
            Phone = "16997640015",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(patient);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Cpf");
    }

    [Fact]
    public async Task PatientCreateValidator_WithDuplicateCpf_ReturnsValidationError()
    {

        var existingPatient = new Api.Models.Patient
        {
            Id = 1,
            Name = "Paciente Antigo",
            Email = "viniciusC2osta@gmail.com",
            Cpf = "12345678901",
            Phone = "16999999999",
            Birthday = new DateOnly(1990, 1, 1)
        };
        _context.Patients.Add(existingPatient);

        var clinicPatient = new ClinicPatient
        {
            ClinicId = 1,
            PatientId = 1
        };

        _context.ClinicPatients.Add(clinicPatient);
        await _context.SaveChangesAsync();

        var duplicatePatientDto = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "12345678901",
            Phone = "16999999999",
            Email = "viniciusCosta@gmail.com",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(duplicatePatientDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == "Cpf");
    }

    [Fact]
    public async Task PatientCreateValidator_WithEmptyClinicId_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "41546867805",
            Email = "vinciuscosta@gmail.com",
            Phone = "16997640015",
            ClinicId = 0
        };

        var result = await _patientValidator.ValidateAsync(patient);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "ClinicId");
    }

    [Fact]
    public async Task PatientCreateValidator_WithNonExistentClinicId_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = new DateOnly(1997, 12, 16),
            Cpf = "41546867805",
            Email = "vinciuscosta@gmail.com",
            Phone = "16997640015",
            ClinicId = 888
        };

        var result = await _patientValidator.ValidateAsync(patient);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "ClinicId");
    }

    [Fact]
    public async Task PatientCreateValidator_WithEmptyBirthday_ReturnsValidationError()
    {
        var patient = new PatientCreateDto
        {
            Name = "Maria Santos",
            Birthday = default,
            Cpf = "41546867805",
            Email = "vinciuscosta@gmail.com",
            Phone = "16997640015",
            ClinicId = 1
        };

        var result = await _patientValidator.ValidateAsync(patient);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Birthday");
    }
}