
using ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.DataLayer.Repositories;

public interface IUsersRepository
{
    List<UserDto> GetUsers();
    UserDto GetUserById(Guid id);
    public Guid CreateUser(UserDto user);
    public void DeleteUser(UserDto user);
    public Guid UpdateUser(UserDto user);
    public UserDto GetUserByUserName(string name);
}