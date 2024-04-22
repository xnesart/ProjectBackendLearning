using Microsoft.AspNetCore.Mvc;
using ProjectBackendLearning.Bll.Services;
using ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController : Controller
{
    private readonly IUsersService _usersService;
    private readonly IDevicesService _devicesService;

    public UsersController(IUsersService usersService, IDevicesService devicesService)
    {
        _usersService = usersService;
        _devicesService = devicesService;
    }

    [HttpGet("getUsers")]
    public ActionResult<List<UserDto>> GetUsers()
    {
        return Ok(_usersService.GetUsers());
    }

    [HttpGet("{id}")]
    public UserDto GetUserById(Guid id)
    {
        return _usersService.GetUserById(id);
    }

    [HttpPost("create")]
    public ActionResult<Guid> CreateUser(string name, string email, string password, int age)
    {
        if (name != null && email != null && password != null && age != null)
        {
            return Ok(_usersService.CreateUser(name, email, password, age));
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUser([FromRoute] Guid id, string name, string email, string password, int age)
    {
        var user = _usersService.GetUserById(id);
        
        if (user is null) 
        {
            return NoContent();
        }

        if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email) &&
            string.IsNullOrEmpty(password) && age == null)
        {
            return BadRequest();
        }
        
        if (!string.IsNullOrEmpty(name))
        {
            user.UserName = name;
        }

        if (!string.IsNullOrEmpty(email))
        {
            user.Email = email;
        }

        if (!string.IsNullOrEmpty(password))
        {
            user.Password = password;
        }

        if (age != null)
        {
            user.Age = age;
        }
        
        _usersService.UpdateUser(user);
        
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public ActionResult DeleteUserById(Guid id)
    {
        _usersService.DeleteUserById(id);
        
        return Ok();
    }
    
    [HttpGet("{userId}/devices")]
    public DeviceDto GetDeviceByUserId(Guid userId)
    {
        return _devicesService.GetDeviceByUserId(Guid.NewGuid());
    }
}