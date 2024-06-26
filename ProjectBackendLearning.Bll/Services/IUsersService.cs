using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Models.Requests;
using ProjectBackendLearning.Core.Models.Responses;

namespace ProjectBackendLearning.Bll.Services;

public interface IUsersService
{
    public List<UserDto> GetUsers();
    public UserDto GetUserById(Guid id);
    public void DeleteUserById(Guid id);
    public Guid CreateUser(CreateUserRequest request);
    public Guid UpdateUser(UpdateUserRequest request);
    public void UpdateUser(UserDto user);
    public AuthenticationResponse Login(LoginUserRequest request);
}