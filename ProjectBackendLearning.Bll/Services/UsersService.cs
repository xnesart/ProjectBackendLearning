using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Exceptions;
using ProjectBackendLearning.DataLayer.Repositories;
using Serilog;

namespace ProjectBackendLearning.Bll.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger _logger = Log.ForContext<UsersService>();


    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Guid CreateUser(UserDto user)
    {
        user.Id = Guid.NewGuid();
        if (user.Age < 18 || user.Age > 200)
        {
            throw new ValidationException("Возраст указан неверно");
        }
        
        return _usersRepository.CreateUser(user);
    }

    public Guid UpdateUser(UserDto user)
    {
        return _usersRepository.UpdateUser(user);
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