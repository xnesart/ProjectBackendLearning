namespace ProjectBackendLearning.Core.DTOs;

public class UserDto : IdContainer
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public int Age { get; set; }
    public virtual List<DeviceDto> Devices { get; set; }
}