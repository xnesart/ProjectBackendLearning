namespace ProjectBackendLearning.Core.Models.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}