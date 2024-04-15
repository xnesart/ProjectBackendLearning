using ClassLibrary1ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.DataLayer.Repositories;

public class UsersRepository : BaseRepository, IUsersRepository
{
    public UsersRepository() : base("")
    {
    }

    public List<UserDto> GetUsers()
    {
        return new List<UserDto>()
        {
            new UserDto()
            {
                Email = "mailByGod@gmail.com",
                Id = Guid.NewGuid(),
                Name = "userJestkiy",
                Password = "1111"
            },
            new UserDto()
            {
                Email = "mail228@gmail.com",
                Id = Guid.NewGuid(),
                Name = "userNeJestkiy",
                Password = "2222"
            },
        };
    }

    public UserDto GetUserById(Guid id)
    {
        return new()
        {
            Id = id,
            Email = "some@mail.com",
            Password = "somepassword",
            Name = "userZver"
        };
    }
}