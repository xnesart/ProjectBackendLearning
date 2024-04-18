using ClassLibrary1ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.DataLayer.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    public UsersRepository(MamkinMinerContext context) : base(context)
    {
    }

    public List<UserDto> GetUsers()
    {
        return _ctx.Users.ToList();
    }

    public UserDto GetUserById(Guid id)
    {
        return new()
        {
            Id = id,
            Email = "some@mail.com",
            Password = "somepassword",
            UserName = "userZver"
        };
    }
}