using ClassLibrary1ProjectBackendLearning.Core.DTOs;
using ClassLibrary1ProjectBackendLearning.Core.Exceptions;
using ProjectBackendLearning.DataLayer.Repositories;

namespace ProjectBackendLearning.Bll.Services;

public class UsersService:IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public List<UserDto> GetUsers()
    {
        return _usersRepository.GetUsers();
    }

    public UserDto GetUserById(Guid id)
    {
        return _usersRepository.GetUserById(id);
    }

    public void DeteleUserById(Guid id)
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