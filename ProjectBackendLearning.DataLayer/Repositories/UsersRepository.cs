using ProjectBackendLearning.Core.DTOs;
using Serilog;

namespace ProjectBackendLearning.DataLayer.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    private readonly ILogger _logger = Log.ForContext<UsersRepository>();

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

    public Guid UpdateUser(UserDto user)
    {
        _ctx.Users.Update(user);
        _ctx.SaveChanges();
        
        return user.Id;
    }

    public List<UserDto> GetUsers()
    {
        _logger.Information("Идем в базу данных запрашивать список всех пользователей");
        return _ctx.Users.ToList();
    }

    public UserDto GetUserById(Guid id) => _ctx.Users.FirstOrDefault(u => u.Id == id);
}