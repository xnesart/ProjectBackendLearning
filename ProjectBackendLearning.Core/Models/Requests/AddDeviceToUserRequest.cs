using ProjectBackendLearning.Core.Enums;

namespace ProjectBackendLearning.Core.Models.Requests;

public class AddDeviceToUserRequest
{
    public string Name { get; set; }
    public DeviceType DeviceType { get; set; }
    public string Address { get; set; }
}