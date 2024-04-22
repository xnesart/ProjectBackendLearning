using ClassLibrary1ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.Bll.Services;

public interface IUsersService
{
    public List<UserDto> GetUsers();
    public UserDto GetUserById(Guid id);
    void DeteleUserById(Guid id);
}