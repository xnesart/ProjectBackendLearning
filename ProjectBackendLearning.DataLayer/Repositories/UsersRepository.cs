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

    public UserDto GetUserById(Guid id) => _ctx.Users.FirstOrDefault(u => u.Id == id);
}