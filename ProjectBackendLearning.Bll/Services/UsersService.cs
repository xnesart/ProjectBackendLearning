using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Exceptions;
using ProjectBackendLearning.DataLayer.Repositories;

namespace ProjectBackendLearning.Bll.Services;

public class UsersService:IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Guid CreateUser(string name, string email, string password, int age)
    {
        UserDto user = new UserDto()
        {
            Age = age,
            Email = email,
            Devices = new List<DeviceDto>(),
            Password = password,
            UserName = name,
            Id = Guid.NewGuid()
        };
        
       return _usersRepository.CreateUser(user);
    }
    public List<UserDto> GetUsers()
    {
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
        else
        {
            
        }
    }
}