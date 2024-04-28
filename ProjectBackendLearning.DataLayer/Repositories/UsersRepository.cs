using ProjectBackendLearning.Core.DTOs;
using Serilog;

namespace ProjectBackendLearning.DataLayer.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    private readonly ILogger _logger = Log.ForContext<UsersRepository>();

    public UsersRepository(BackMinerContext context) : base(context)
    {
    }

    public Guid CreateUser(UserDto user)
    {
        _logger.Debug($"Создаем пользователя в репозитории: {user.UserName}");

        _ctx.Users.Add(user);
        _ctx.SaveChanges();

        _logger.Debug($"Успешно. Возвращаем Id пользователя: {user.UserName}");
        
        return user.Id;
    }

    public void DeleteUser(UserDto user)
    {
        _logger.Debug($"Удаляем пользователя из базы: {user.UserName}");

        _ctx.Users.Remove(user);
        _ctx.SaveChanges();
    }

    public Guid UpdateUser(UserDto user)
    {
        _logger.Debug($"Обновляем пользователя в репозитории: {user.UserName}");
        _ctx.Users.Update(user);
        _ctx.SaveChanges();

        _logger.Debug($"Успешно. Возвращаем Id пользователя: {user.UserName}");
        
        return user.Id;
    }

    public List<UserDto> GetUsers()
    {
        _logger.Information("Идем в базу данных запрашивать список всех пользователей");
        
        return _ctx.Users.ToList();
    }

    public UserDto GetUserById(Guid id)
    {
        _logger.Information($"Ищем в базе данных пользователя с id {id}");
        
        return _ctx.Users.FirstOrDefault(u => u.Id == id);
    }

    public UserDto GetUserByUserName(string name)
    {
        _logger.Information($"Ищем в базе данных пользователя с username {name}");
        
        return _ctx.Users.FirstOrDefault(u => u.UserName == name);
    }
}