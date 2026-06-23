using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Clinics;
using SaasClinicas.Api.Validators.Clinics;

namespace SaasClinicas.Tests.Validators.Clinic;

public class ClinicCreateValidatorTests : IClassFixture<TestDbFixture>, IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ClinicCreateValidator _validator;
    private readonly TestDbFixture _fixture;

    public ClinicCreateValidatorTests(TestDbFixture fixture)
    {
        _context = fixture.Context;
        _fixture = fixture;
        _validator = new ClinicCreateValidator(_context);
    }

    public void Dispose()
    {
        _fixture.ClearDatabase();
    }

    [Fact]
    public async Task ClinicCreateValidator_WithValidData_ReturnsValid()
    {
        // Clinic com Id=1 já existe no fixture
        var clinic = new ClinicCreateDto
        {
            ClinicName = "Clínica São Paulo",
            ResponsibleName = "Dr. João Silva",
            Email = "contato@clinica.com.br",
            Phone = "11999999999"
        };

        var result = await _validator.ValidateAsync(clinic);

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ClinicCreateValidator_WithEmptyClinicName_ReturnsValidationError()
    {
        var clinic = new ClinicCreateDto
        {
            ClinicName = "",
            ResponsibleName = "Dr. João Silva",
            Email = "contato@clinica.com",
            Phone = "11999999999"
        };

        var result = await _validator.ValidateAsync(clinic);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "ClinicName");
    }

    [Fact]
    public async Task ClinicCreateValidator_WithClinicNameTooShort_ReturnsValidationError()
    {
        var clinic = new ClinicCreateDto
        {
            ClinicName = "Cl",
            ResponsibleName = "Dr. João Silva",
            Email = "contato@clinica.com",
            Phone = "11999999999"
        };

        var result = await _validator.ValidateAsync(clinic);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "ClinicName");
    }

    [Fact]
    public async Task ClinicCreateValidator_WithClinicNameTooLong_ReturnsValidationError()
    {
        var clinic = new ClinicCreateDto
        {
            ClinicName = new string('A', 300),
            ResponsibleName = "Dr. João Silva",
            Email = "contato@clinica.com",
            Phone = "11999999999"
        };

        var result = await _validator.ValidateAsync(clinic);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "ClinicName");
    }

    [Fact]
    public async Task ClinicCreateValidator_WithEmptyResponsibleName_ReturnsValidationError()
    {
        var clinic = new ClinicCreateDto
        {
            ClinicName = "Clínica São Paulo",
            ResponsibleName = "",
            Email = "contato@clinica.com",
            Phone = "11999999999"
        };

        var result = await _validator.ValidateAsync(clinic);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "ResponsibleName");
    }

    [Fact]
    public async Task ClinicCreateValidator_WithResponsibleNameTooShort_ReturnsValidationError()
    {
        var clinic = new ClinicCreateDto
        {
            ClinicName = "Clínica São Paulo",
            ResponsibleName = "Dr",
            Email = "contato@clinica.com",
            Phone = "11999999999"
        };

        var result = await _validator.ValidateAsync(clinic);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "ResponsibleName");
    }

    [Fact]
    public async Task ClinicCreateValidator_WithEmptyEmail_ReturnsValidationError()
    {
        var clinic = new ClinicCreateDto
        {
            ClinicName = "Clínica São Paulo",
            ResponsibleName = "Dr. João Silva",
            Email = "",
            Phone = "11999999999"
        };

        var result = await _validator.ValidateAsync(clinic);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public async Task ClinicCreateValidator_WithInvalidEmailFormat_ReturnsValidationError()
    {
        var clinic = new ClinicCreateDto
        {
            ClinicName = "Clínica São Paulo",
            ResponsibleName = "Dr. João Silva",
            Email = "invalid-email",
            Phone = "11999999999"
        };

        var result = await _validator.ValidateAsync(clinic);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public async Task ClinicCreateValidator_WithDuplicateEmail_ReturnsValidationError()
    {
        // Fixture já tem clínica com email "Teste@gmail.com"
        var clinic = new ClinicCreateDto
        {
            ClinicName = "Clínica São Paulo",
            ResponsibleName = "Dr. João Silva",
            Email = "Teste@gmail.com",  // Email duplicado do fixture
            Phone = "11999999999"
        };

        var result = await _validator.ValidateAsync(clinic);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public async Task ClinicCreateValidator_WithEmptyPhone_ReturnsValidationError()
    {
        var clinic = new ClinicCreateDto
        {
            ClinicName = "Clínica São Paulo",
            ResponsibleName = "Dr. João Silva",
            Email = "contato@clinica.com",
            Phone = ""
        };

        var result = await _validator.ValidateAsync(clinic);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Phone");
    }

    [Fact]
    public async Task ClinicCreateValidator_WithPhoneTooShort_ReturnsValidationError()
    {
        var clinic = new ClinicCreateDto
        {
            ClinicName = "Clínica São Paulo",
            ResponsibleName = "Dr. João Silva",
            Email = "contato@clinica.com",
            Phone = "119999999"
        };

        var result = await _validator.ValidateAsync(clinic);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Phone");
    }

    [Fact]
    public async Task ClinicCreateValidator_WithPhoneTooLong_ReturnsValidationError()
    {
        var clinic = new ClinicCreateDto
        {
            ClinicName = "Clínica São Paulo",
            ResponsibleName = "Dr. João Silva",
            Email = "contato@clinica.com",
            Phone = "119999999999999"
        };

        var result = await _validator.ValidateAsync(clinic);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Phone");
    }

    [Fact]
    public async Task ClinicCreateValidator_WithFormattedPhone_ReturnsValidationError()
    {
        var clinic = new ClinicCreateDto
        {
            ClinicName = "Clínica São Paulo",
            ResponsibleName = "Dr. João Silva",
            Email = "contato@clinica.com",
            Phone = "(11) 99999-9999"
        };

        var result = await _validator.ValidateAsync(clinic);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Phone");
    }
}
