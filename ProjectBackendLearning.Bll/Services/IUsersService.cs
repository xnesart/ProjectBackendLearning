
using ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.Bll.Services;

public interface IUsersService
{
    public List<UserDto> GetUsers();
    public UserDto GetUserById(Guid id);
    public void DeleteUserById(Guid id);
    public Guid CreateUser(string name, string email, string password, int age);
}