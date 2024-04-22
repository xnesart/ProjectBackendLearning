using ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.DataLayer.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    public UsersRepository(MamkinMinerContext context) : base(context)
    {
    }

    public Guid CreateUser(UserDto user)
    {
        _ctx.Users.Add(user);
        _ctx.SaveChanges();
        
        return user.Id;
    }

    public void DeleteUser(UserDto user)
    {
        _ctx.Users.Remove(user);
        _ctx.SaveChanges();
    }

    public List<UserDto> GetUsers()
    {
        return _ctx.Users.ToList();
    }

    public UserDto GetUserById(Guid id) => _ctx.Users.FirstOrDefault(u => u.Id == id);
}