using AutoMapper;
using FluentValidation;
using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Exceptions;
using ProjectBackendLearning.Core.Models.Requests;
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


    public UsersService(IUsersRepository usersRepository, IMapper mapper, IValidator<CreateUserRequest> userValidator,
        IValidator<UpdateUserRequest> userUpdateValidator)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _userValidator = userValidator;
        _userUpdateValidator = userUpdateValidator;
    }


    public Guid CreateUser(CreateUserRequest request)
    {
        var validationResult = _userValidator.Validate(request);
        if (validationResult.IsValid)
        {
            var user = _mapper.Map<UserDto>(request);
            user.Id = Guid.NewGuid();

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
            if (user != null)
            {
                user.Age = request.Age;
                user.UserName = request.UserName;
                user.Email = request.Email;
                
                return _usersRepository.UpdateUser(user);
            }

            throw new NotFoundException("Пользователь не найден");
        }

        string exceptions = string.Join(Environment.NewLine, validationResult.Errors);
        throw new ValidationException(exceptions);
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