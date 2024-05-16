using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Models.Requests;

namespace ProjectBackendLearning.Bll.Services;

public interface IDevicesService
{
    DeviceDto GetDeviceById(Guid id);
    DeviceDto GetDeviceByUserId(Guid userId);
    void AddDeviceToUser(Guid id, AddDeviceToUserRequest request);
    void SetDeviceStatus(Guid deviceId, bool value);
    List<DeviceDto> GetDevicesWithStatus();

}