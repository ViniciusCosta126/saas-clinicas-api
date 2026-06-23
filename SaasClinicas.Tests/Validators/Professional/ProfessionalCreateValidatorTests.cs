using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Professionals;
using SaasClinicas.Api.Validators.Professionals;

namespace SaasClinicas.Tests.Validators.Professional;

public class ProfessionalCreateValidatorTests : IClassFixture<TestDbFixture>, IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ProfessionalCreateValidator _validator;
    private readonly TestDbFixture _fixture;

    public ProfessionalCreateValidatorTests(TestDbFixture fixture)
    {
        _context = fixture.Context;
        _fixture = fixture;
        _validator = new ProfessionalCreateValidator(_context);
    }

    public void Dispose()
    {
        _fixture.ClearDatabase();
    }

    [Fact]
    public async Task ProfessionalCreateValidator_WithValidData_ReturnsValid()
    {
        var professional = new ProfessionalCreateDto
        {
            Name = "Dr. João Silva",
            Email = "joao.silva@example.com",
            Specialty = "Cardiologia",
            SessionPrice = 150.00m,
            ClinicId = 1
        };

        var result = await _validator.ValidateAsync(professional);

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ProfessionalCreateValidator_WithEmptyName_ReturnsValidationError()
    {
        var professional = new ProfessionalCreateDto
        {
            Name = "",
            Email = "joao.silva@example.com",
            Specialty = "Cardiologia",
            SessionPrice = 150.00m,
            ClinicId = 1
        };

        var result = await _validator.ValidateAsync(professional);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public async Task ProfessionalCreateValidator_WithNameTooShort_ReturnsValidationError()
    {
        var professional = new ProfessionalCreateDto
        {
            Name = "Dr",
            Email = "joao.silva@example.com",
            Specialty = "Cardiologia",
            SessionPrice = 150.00m,
            ClinicId = 1
        };

        var result = await _validator.ValidateAsync(professional);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public async Task ProfessionalCreateValidator_WithInvalidEmailFormat_ReturnsValidationError()
    {
        var professional = new ProfessionalCreateDto
        {
            Name = "Dr. João Silva",
            Email = "invalid-email",
            Specialty = "Cardiologia",
            SessionPrice = 150.00m,
            ClinicId = 1
        };

        var result = await _validator.ValidateAsync(professional);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public async Task ProfessionalCreateValidator_WithDuplicateEmail_ReturnsValidationError()
    {
        var existingProfessional = new Api.Models.Professional
        {
            Id = 1,
            Name = "Dr. Antigo",
            Email = "joao.silva@example.com",
            Specialty = "Cardiologia",
            SessionPrice = 150.00m,
            ClinicId = 1
        };

        _context.Professionals.Add(existingProfessional);
        await _context.SaveChangesAsync();

        var professional = new ProfessionalCreateDto
        {
            Name = "Dr. João Silva",
            Email = "joao.silva@example.com",
            Specialty = "Cardiologia",
            SessionPrice = 150.00m,
            ClinicId = 1
        };

        var result = await _validator.ValidateAsync(professional);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public async Task ProfessionalCreateValidator_WithZeroSessionPrice_ReturnsValidationError()
    {
        var professional = new ProfessionalCreateDto
        {
            Name = "Dr. João Silva",
            Email = "joao.silva@example.com",
            Specialty = "Cardiologia",
            SessionPrice = 0,
            ClinicId = 1
        };

        var result = await _validator.ValidateAsync(professional);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "SessionPrice");
    }

    [Fact]
    public async Task ProfessionalCreateValidator_WithNegativeSessionPrice_ReturnsValidationError()
    {
        var professional = new ProfessionalCreateDto
        {
            Name = "Dr. João Silva",
            Email = "joao.silva@example.com",
            Specialty = "Cardiologia",
            SessionPrice = -50,
            ClinicId = 1
        };

        var result = await _validator.ValidateAsync(professional);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "SessionPrice");
    }

    [Fact]
    public async Task ProfessionalCreateValidator_WithInvalidClinicId_ReturnsValidationError()
    {
        var professional = new ProfessionalCreateDto
        {
            Name = "Dr. João Silva",
            Email = "joao.silva@example.com",
            Specialty = "Cardiologia",
            SessionPrice = 150.00m,
            ClinicId = 999
        };

        var result = await _validator.ValidateAsync(professional);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "ClinicId");
    }
}
