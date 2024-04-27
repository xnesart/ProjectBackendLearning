using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

    [Authorize]
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

        return Ok(_usersService.CreateUser(request));
    }

    [HttpPost("login")]
    public ActionResult<AuthenticationResponse> Login([FromBody] LoginUserRequest user)
    {
        if (user is null)
        {
            return BadRequest("Invalid client request");
        }

        if (user.UserName == "johndoe" && user.Password == "def@123")
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345fffffa43534534523dsf"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "ProjectBackendLearning",
                audience: "UI",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthenticationResponse { Token = tokenString });
        }

        return Unauthorized();
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

        return Ok();
    }

    [HttpGet("{userId}/devices")]
    public DeviceDto GetDeviceByUserId(Guid userId)
    {
        return _devicesService.GetDeviceByUserId(Guid.NewGuid());
    }
}