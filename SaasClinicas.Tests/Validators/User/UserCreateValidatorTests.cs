using SaasClinicas.Api.Data;
using SaasClinicas.Api.Dtos.Users;
using SaasClinicas.Api.Enums;
using SaasClinicas.Api.Validators.Users;

namespace SaasClinicas.Tests.Validators.User;

public class UserCreateValidatorTests : IClassFixture<TestDbFixture>, IDisposable
{
    private readonly ApplicationDbContext _context;

    private readonly UserCreateValidator _userValidator;
    private readonly TestDbFixture _fixture;
    public UserCreateValidatorTests(TestDbFixture fixture)
    {
        _context = fixture.Context;
        _fixture = fixture;

        _userValidator = new UserCreateValidator(_context);
    }
    public void Dispose()
    {
        _fixture.ClearDatabase();
    }

    [Fact]
    public async Task UserCreateValidator_WithValidData_ReturnsValid()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "11988887777",
            Cpf = "41546867805",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task UserCreateValidator_WithEmptyName_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "",
            Email = "alexandre.silva@example.com",
            Phone = "11988887777",
            Cpf = "41546867805",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public async Task UserCreateValidator_WithNameTooShort_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "12",
            Email = "alexandre.silva@example.com",
            Phone = "11988887777",
            Cpf = "41546867805",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public async Task UserCreateValidator_WithNameTooLong_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "%Wq9#2vB%Wq9#2vB&x7L5kR1@z$4Pm8N!c0Jg9Y3^tL2#pX6mQ1&dF9$zR4^vB7#mN2&xK5@zL9%pQ2#vB7&xL5kR1@z$4Pm8N!c0Jg9Y3^tL2#pX6mQ1&dF9$zR4^vB7#mN2&xK5@zL9%pQ2#vB7&xL5kR1@z$4Pm8N!c0Jg9Y3^tL2#pX6mQ1&dF9$zR4^vB7#mN2&xK5@zL9%pQ2#vB7&xL5kR21390301284901284901284192843123111",
            Email = "alexandre.silva@example.com",
            Phone = "11988887777",
            Cpf = "41546867805",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    [Fact]
    public async Task UserCreateValidator_WithEmptyEmail_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "",
            Phone = "11988887777",
            Cpf = "41546867805",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public async Task UserCreateValidator_WithInvalidEmailFormat_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silvaexample.com",
            Phone = "11988887777",
            Cpf = "41546867805",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public async Task CreateUserValidator_WithDuplicateEmail_ReturnsValidationError()
    {
        var existingUser = new Api.Models.User
        {
            Id = 1,
            Name = "Paciente Antigo",
            Email = "viniciusCosta@gmail.com",
            Cpf = "12345678901",
            Phone = "16999999999",
            ClinicId = 1,
        };

        _context.Users.Add(existingUser);
        await _context.SaveChangesAsync();

        var duplicateUser = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "viniciusCosta@gmail.com",
            Phone = "11988887777",
            Cpf = "41546867805",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(duplicateUser);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public async Task CreateUserValidator_WithEmptyPhone_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "",
            Cpf = "41546867805",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Phone");
    }

    [Fact]
    public async Task CreateUserValidator_WithPhoneTooShort_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "169976400",
            Cpf = "41546867805",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Phone");
    }

    [Fact]
    public async Task CreateUserValidator_WithPhoneTooLong_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "1199999888877",
            Cpf = "41546867805",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Phone");
    }


    [Fact]
    public async Task CreateUserValidator_WithFormattedPhone_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "(11) 99999-8888",
            Cpf = "41546867805",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Phone");
    }

    [Fact]
    public async Task CreateUserValidator_WithEmptyCpf_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "16997640015",
            Cpf = "",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Cpf");
    }

    [Fact]
    public async Task CreateUserValidator_WithCpfContainingLettersOrFormatting_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "16997640015",
            Cpf = "1234567891a",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Cpf");
    }

    [Fact]
    public async Task CreateUserValidator_WithDuplicateCpf_ReturnsValidationError()
    {
        var existingUser = new Api.Models.User
        {
            Id = 1,
            Name = "Paciente Antigo",
            Email = "viniciusCosta@gmail.com",
            Cpf = "12345678901",
            Phone = "16999999999",
            Role = UserRole.Admin,
            ClinicId = 1,
        };

        _context.Users.Add(existingUser);
        await _context.SaveChangesAsync();

        var duplicateUser = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "11988887777",
            Cpf = "12345678901",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(duplicateUser);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Cpf");
    }

    [Fact]
    public async Task CreateUserValidator_WithEmptyClinicId_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "16997640015",
            Cpf = "12345678911",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 0
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "ClinicId");
    }

    [Fact]
    public async Task CreateUserValidator_WithNonExistentClinicId_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "16997640015",
            Cpf = "12345678911",
            Password = "SenhaSegura2026",
            Role = UserRole.Admin,
            ClinicId = 121312
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "ClinicId");
    }

    [Fact]
    public async Task CreateUserValidator_WithEmptyPassword_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "16997640015",
            Cpf = "12345678911",
            Password = "",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }

    [Fact]
    public async Task CreateUserValidator_WithPasswordTooShort_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "16997640015",
            Cpf = "12345678911",
            Password = "1234567",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }

    [Fact]
    public async Task CreateUserValidator_WithPasswordTooLong_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "16997640015",
            Cpf = "12345678911",
            Password = "12345671234567123456712345671234567",
            Role = UserRole.Admin,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }


    [Fact]
    public async Task CreateUserValidator_WithInvalidEnumValue_ReturnsValidationError()
    {
        var validUserDto = new UserCreateDto
        {
            Name = "Alexandre Silva",
            Email = "alexandre.silva@example.com",
            Phone = "16997640015",
            Cpf = "12345678911",
            Password = "12345678",
            Role = (UserRole)99,
            ClinicId = 1
        };

        var result = await _userValidator.ValidateAsync(validUserDto);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Role");
    }
}