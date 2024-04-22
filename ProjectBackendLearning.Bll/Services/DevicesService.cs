using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.DataLayer.Repositories;

namespace ProjectBackendLearning.Bll.Services;

public class DevicesService : IDevicesService
{
    private readonly IDevicesRepository _devicesRepository;

    public DevicesService(IDevicesRepository devicesRepository)
    {
        _devicesRepository = devicesRepository;
    }
    
    public DeviceDto GetDeviceById(Guid id) => _devicesRepository.GetDeviceById(id);
    public DeviceDto GetDeviceByUserId(Guid userId) => _devicesRepository.GetDeviceByUserId(userId);
}