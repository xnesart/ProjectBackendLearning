using ClassLibrary1ProjectBackendLearning.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using ProjectBackendLearning.Bll.Services;

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
    public ActionResult UpdateUser([FromRoute] Guid id, [FromBody] object request)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteUserById(Guid id)
    {
        try
        {
            _usersService.DeteleUserById(id);
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