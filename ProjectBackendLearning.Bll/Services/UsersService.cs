using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Exceptions;
using ProjectBackendLearning.Core.Models.Requests;
using ProjectBackendLearning.Core.Models.Responses;
using ProjectBackendLearning.DataLayer.Repositories;
using Serilog;
using ValidationException = ProjectBackendLearning.Core.Exceptions.ValidationException;

namespace ProjectBackendLearning.Bll.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<UsersService>();
    private readonly IMapper _mapper;
    private readonly IValidator<CreateUserRequest> _userValidator;
    private readonly IValidator<UpdateUserRequest> _userUpdateValidator;
    private const string _pepper = "son";
    private const int _iteration = 3;

    public UsersService(IUsersRepository usersRepository, IMapper mapper, IValidator<CreateUserRequest> userValidator,
        IValidator<UpdateUserRequest> userUpdateValidator)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _userValidator = userValidator;
        _userUpdateValidator = userUpdateValidator;
    }

    public AuthenticationResponse Login(LoginUserRequest request)
    {
        _logger.Debug($"Ищем пользователя в базе с параметрами: {request.UserName}");
        var user = _usersRepository.GetUserByUserName(request.UserName);

        if (user == null) throw new NotFoundException("Username or password did not match.");

        _logger.Debug($"Сравниваем пароль пользователя: {request.UserName}");
        if (!ComparePassword(request, user)) throw new NotFoundException("Username or password did not match.");

        var secretKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345fffffa43534534523dsf"));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            issuer: "ProjectBackendLearning",
            audience: "UI",
            claims: new List<Claim>(),
            expires: DateTime.Now.AddDays(1),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        _logger.Debug($"Выдаем токен пользователю: {request.UserName}");

        return new AuthenticationResponse { Token = tokenString };
    }

    private static bool ComparePassword(LoginUserRequest request, UserDto user)
    {
        var passwordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, _pepper, _iteration);
        if (user.PasswordHash != passwordHash)
        {
            Log.Debug($"Пароль пользователя не совпадает: {request.UserName}");
            throw new AuthenticationException("Username or password did not match.");
        }

        return true;
    }

    public Guid CreateUser(CreateUserRequest request)
    {
        var validationResult = _userValidator.Validate(request);

        if (validationResult.IsValid)
        {
            var user = _mapper.Map<UserDto>(request);
            // user.Id = Guid.NewGuid();
            user.PasswordSalt = PasswordHasher.GenerateSalt();
            user.PasswordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, _pepper, _iteration);

            return _usersRepository.CreateUser(user);
        }

        string exceptions = string.Join(Environment.NewLine, validationResult.Errors);

        throw new ValidationException(exceptions);
    }

    public Guid UpdateUser(UpdateUserRequest request)
    {
        var validationResult = _userUpdateValidator.Validate(request);

        if (validationResult.IsValid)
        {
            var user = _usersRepository.GetUserById(request.Id);

            if (user == null)
            {
                _logger.Error($"Пользователь не найден: {request.UserName}");
                throw new NotFoundException("Пользователь не найден");
            }

            user.Age = request.Age;
            //user.UserName = request.UserName;
            user.Email = request.Email;

            _logger.Information($"Идем в репозиторий обновлять пользователя: {request.UserName}");

            return _usersRepository.UpdateUser(user);
        }

        string exceptions = string.Join(Environment.NewLine, validationResult.Errors);
        throw new ValidationException(exceptions);
    }

    public void UpdateUser(UserDto user)
    {
        _logger.Information($"Идем в репозиторий обновлять пользователя с девайсом: {user.UserName}");

        _usersRepository.UpdateUser(user);
    }

    public List<UserDto> GetUsers()
    {
        _logger.Information("Зовем метод репозитория");
        return _usersRepository.GetUsers();
    }

    public UserDto GetUserById(Guid id)
    {
        return _usersRepository.GetUserById(id);
    }

    public void DeleteUserById(Guid id)
    {
        var user = _usersRepository.GetUserById(id);

        if (user is null)
        {
            throw new NotFoundException($"Юзер с id {id} не найден");
        }

        _usersRepository.DeleteUser(user);
    }
}