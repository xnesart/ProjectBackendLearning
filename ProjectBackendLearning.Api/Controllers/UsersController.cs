using ClassLibrary1ProjectBackendLearning.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using ProjectBackendLearning.Bll.Services;

namespace ProjectBackendLearning.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : Controller
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet("getUsers")]
    public List<UserDto> GetUsers()
    {
        return _usersService.GetUsers();
    }

    [HttpGet("getUser")]
    public UserDto GetUserById()
    {
        return _usersService.GetUserById(Guid.NewGuid());
    }
}