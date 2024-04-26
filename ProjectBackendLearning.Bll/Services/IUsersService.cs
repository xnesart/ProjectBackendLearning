
using ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.Bll.Services;

public interface IUsersService
{
    public List<UserDto> GetUsers();
    public UserDto GetUserById(Guid id);
    public void DeleteUserById(Guid id);
    public Guid CreateUser(UserDto user);
    public Guid UpdateUser(UserDto user);
}