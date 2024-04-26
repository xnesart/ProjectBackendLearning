namespace ProjectBackendLearning.Models.Responses;

public class UserWithDevicesResponse:UserResponse
{
    public List<DeviceResponse> Devices { get; set; }
}