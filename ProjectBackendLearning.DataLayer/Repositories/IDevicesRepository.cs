
using ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.DataLayer.Repositories;

public interface IDevicesRepository
{
    DeviceDto GetDeviceById(Guid id);
    DeviceDto GetDeviceByUserId(Guid userId);
}