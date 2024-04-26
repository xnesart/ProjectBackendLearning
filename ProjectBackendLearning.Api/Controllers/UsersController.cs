using Microsoft.AspNetCore.Mvc;
using ProjectBackendLearning.Bll.Services;
using ProjectBackendLearning.Configuration;
using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Models.Requests;
using ProjectBackendLearning.Models.Responses;
using Serilog;

namespace ProjectBackendLearning.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController : Controller
{
    private readonly IUsersService _usersService;
    private readonly IDevicesService _devicesService;
    private readonly Serilog.ILogger _logger = Log.ForContext<UsersController>();

    public UsersController(IUsersService usersService, IDevicesService devicesService)
    {
        _usersService = usersService;
        _devicesService = devicesService;
    }

    [HttpGet("getUsers")]
    public ActionResult<List<UserDto>> GetUsers()
    {
        _logger.Information("Получаем список всех пользователей");
        return Ok(_usersService.GetUsers());
    }

    [HttpGet("{id}")]
    public ActionResult<UserResponse> GetUserById(Guid id)
    {
        _logger.Information($"Получаем пользователя по id {id}");

        var user = _usersService.GetUserById(id);
        UserResponse userForReturn = new UserResponse()
        {
            Id = user.Id,
            Age = user.Age,
            Email = user.Email,
            UserName = user.UserName
        };
        return Ok(userForReturn);
    }
    
    [HttpPost("create")]
    public ActionResult<Guid> CreateUser([FromBody] CreateUserRequest request)
    {
        _logger.Debug($"Запрос создать пользователя с параметрами: {request.UserName} {request.Age} {request.Email}");
        if (request.UserName != null && request.Password != null && request.Age != null && request.Email != null)
        {
            UserDto user = new UserDto()
                { Age = request.Age, Email = request.Email, Password = request.Password, UserName = request.UserName };
            return Ok(_usersService.CreateUser(user));
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public ActionResult<Guid> UpdateUser(Guid id, string name, string email, string password, int age)
    {
        var user = _usersService.GetUserById(id);

        if (user is null)
        {
            return NoContent();
        }

        if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email) &&
            string.IsNullOrEmpty(password) && age > 0)
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

        if (age > 0)
        {
            user.Age = age;
        }

        return Ok(_usersService.UpdateUser(user));
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