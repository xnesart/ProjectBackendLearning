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

    [HttpGet("{id}")]
    public UserDto GetUserById(Guid id)
    {
        return _usersService.GetUserById(Guid.NewGuid());
    }

    [HttpPost]
    public Guid CreateUser(object request)
    {
        return Guid.NewGuid();
    }

    [HttpPut("{id}")]
    public Guid UpdateUser([FromRoute] Guid id, [FromBody] object request)
    {
        return Guid.NewGuid();
    }  
    
    [HttpDelete("{id}")]
    public void DeleteUserById(Guid id)
    {
        
    }
}