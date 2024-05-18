using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectBackendLearning.Bll.Services;
using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Models.Requests;
using ProjectBackendLearning.Core.Models.Responses;
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

    // [Authorize]
    [HttpGet("getUsers")]
    public ActionResult<List<UserDto>> GetUsers()
    {
        _logger.Information("Получаем список всех пользователей");
        
        return Ok(_usersService.GetUsers());
    }

    // [HttpGet("{id}")]
    // public ActionResult<UserResponse> GetUserById(Guid id)
    // {
    //     _logger.Information($"Получаем пользователя по id {id}");
    //
    //     var user = _usersService.GetUserById(id);
    //     UserResponse userForReturn = new UserResponse()
    //     {
    //         Id = user.Id,
    //         Age = user.Age,
    //         Email = user.Email,
    //         UserName = user.UserName
    //     };
    //     
    //     return Ok(userForReturn);
    // }

    [HttpPost("create")]
    public ActionResult<Guid> CreateUser([FromBody] CreateUserRequest request)
    {
        _logger.Information($"Запрос создать пользователя с параметрами: {request.UserName} {request.Age} {request.Email}");

        return Ok(_usersService.CreateUser(request));
    }

    [HttpPost("login")]
    public ActionResult<AuthenticationResponse> Login([FromBody] LoginUserRequest request)
    {
        _logger.Information($"Запрос авторизации пользователя с параметрами: {request.UserName}");

        if (request is null)
        {
            return BadRequest("Invalid client request");
        }

        return Ok(_usersService.Login(request));
    }

    [HttpPut]
    public ActionResult<Guid> UpdateUser([FromBody] UpdateUserRequest request)
    {
        if (request != null)
        {
            return Ok(_usersService.UpdateUser(request));
        }

        return BadRequest();
    }

    [HttpDelete("{id:guid}")]
    public ActionResult DeleteUserById(Guid id)
    {
        _usersService.DeleteUserById(id);

        return NotFound();
    }

    [HttpGet("{userId}/devices")]
    public ActionResult<List<DeviceDto>> GetDevicesByUserId(Guid userId)
    {
        return Ok(_devicesService.GetDevicesByUserId(userId));
    }
}