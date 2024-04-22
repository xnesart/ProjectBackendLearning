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
        return _usersService.GetUserById(Guid.NewGuid());
    }

    [HttpPost("create")]
    public ActionResult<Guid> CreateUser(string name, string email, string password, int age)
    {
        if (name != null && email != null && password != null)
        {
            return Ok(_usersService.CreateUser(name, email, password, age));
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public ActionResult UpdateUser([FromRoute] Guid id, [FromBody] object request)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        try
        {
            _usersService.DeleteUserById(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return NoContent();
    }

    //api users/42/devices
    [HttpGet("{userId}/devices")]
    public DeviceDto GetDeviceByUserId(Guid id)
    {
        return _devicesService.GetDeviceByUserId(Guid.NewGuid());
    }
}