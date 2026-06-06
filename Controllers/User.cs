using Microsoft.AspNetCore.Mvc;
using SaasClinicas.APi.Models;

namespace SaasClinicas.APi.Controllers;

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