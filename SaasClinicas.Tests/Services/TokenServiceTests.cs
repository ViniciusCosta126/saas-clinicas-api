using Microsoft.Extensions.Configuration;
using Moq;
using SaasClinicas.Api.Enums;
using SaasClinicas.Api.Models;
using SaasClinicas.Api.Services;

namespace SaasClinicas.Tests.Services;


public class TokenServiceTests
{
    private readonly ITokenService _tokenService;
    private readonly User _testUser;

    public TokenServiceTests()
    {
        var mockConfig = new Mock<IConfiguration>();

        mockConfig.Setup(x => x["Jwt:key"]).Returns("wt3O3Pbkk09LC9fvgclOxC54E0G0XRni");
        mockConfig.Setup(x => x["Jwt:Issuer"]).Returns("SaasClinicas");
        mockConfig.Setup(x => x["Jwt:Audience"]).Returns("SaasClinicasUsers");

        _tokenService = new TokenService(mockConfig.Object);
        _testUser = new User
        {
            Id = 1,
            Name = "João Silva",
            Email = "joao@test.com",
            Phone = "11999999999",
            Cpf = "12345678901",
            Password = "hash-da-senha",
            ClinicId = 1,
            Role = UserRole.Admin
        };
    }

    [Fact]
    public void CreateToken_WithValidUser_ReturnsToken()
    {
        var token = _tokenService.CreateToken(_testUser);

        Assert.NotNull(token);
        Assert.NotEmpty(token);
        Assert.IsType<string>(token);
    }

    [Fact]

    public void CreateToken_WithValidUser_IsNotEmpty()
    {
        var token = _tokenService.CreateToken(_testUser);

        Assert.True(token.Length > 0);
    }

    [Fact]
    public void CreateToken_WithDifferentUsers_ReturnDifferentsTokens()
    {
        var user2 = new User
        {
            Id = 2,
            Name = "Maria Santos",
            Email = "maria@test.com",
            Phone = "11988888888",
            Cpf = "98765432101",
            Password = "hash-da-senha",
            ClinicId = 2,
            Role = UserRole.Professional
        };

        var token1 = _tokenService.CreateToken(_testUser);
        var token2 = _tokenService.CreateToken(user2);

        Assert.NotEqual(token1, token2);
    }
}