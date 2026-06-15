using Microsoft.AspNetCore.Mvc;
using SaasClinicas.Api.Models;

namespace SaasClinicas.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private static List<User> users = new List<User>();

    [HttpGet]
    public IEnumerable<User> GetUsers()
    {
        return users;
    }
}