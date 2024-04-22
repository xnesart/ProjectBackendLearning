using ClassLibrary1ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.DataLayer.Repositories;

public interface IUsersRepository
{
    List<UserDto> GetUsers();
    UserDto GetUserById(Guid id);
}