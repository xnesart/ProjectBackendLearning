
using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Models.Requests;

namespace ProjectBackendLearning.DataLayer.Repositories;

public interface IDevicesRepository
{
    DeviceDto GetDeviceById(Guid id);
    List<DeviceDto> GetDevicesByUserId(Guid userId);
    public void AddDeviceToUser(DeviceDto device);
    public void SetDeviceStatus(DeviceDto device);
    public List<DeviceDto> GetDevicesWhereStatusIsNotNull();

}