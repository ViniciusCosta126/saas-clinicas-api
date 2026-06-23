using Xunit;
using SaasClinicas.Api.Services;
namespace SaasClinicas.Tests.Services;

public class PasswordHashServiceTests
{
    private readonly PasswordHashService _service;

    public PasswordHashServiceTests()
    {
        _service = new PasswordHashService();
    }

    [Fact]
    public void HashPassword_WithValidPassword_ReturnsHashedPassword()
    {
        var password = "Senha123";

        var hashed = _service.HashPassword(password);

        Assert.NotNull(hashed);
        Assert.NotEqual(password,hashed);
    }

    [Fact]

    public void VerifyPassword_WithCorrectPassword_ReturnsTrue()
    {
        var password = "Senha123";

        var hashed = _service.HashPassword(password);

        var result = _service.VerifyPassword(password,hashed);

        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_WithWrong_Password_ReturnsFalse()
    {
        var password = "Senha123";
        var wrongPassword = "teste123";

        var hashed = _service.HashPassword(password);

        var result = _service.VerifyPassword(wrongPassword,hashed);

        Assert.False(result);
    }

    [Fact]
    public void HashPassword_SamePassword_ReturnDifferentHashes()
    {
        var password = "Senha123";

        var hash1 = _service.HashPassword(password);
        var hash2 = _service.HashPassword(password);

        Assert.NotEqual(hash1,hash2);
    }

    [Fact]
    public void HashPassword_WithEmptyPassword_ReturnsHash()
    {
        var password = "";

        var hashed = _service.HashPassword(password);
        
        Assert.NotNull(hashed);
        Assert.NotEmpty(hashed);
    }
}