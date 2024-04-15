namespace ClassLibrary1ProjectBackendLearning.Core.DTOs;

public class UserDto : IdContainer
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}