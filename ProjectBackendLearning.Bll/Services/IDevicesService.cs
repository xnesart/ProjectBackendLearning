using ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.Bll.Services;

public interface IDevicesService
{
    DeviceDto GetDeviceById(Guid id);
    DeviceDto GetDeviceByUserId(Guid userId);
}