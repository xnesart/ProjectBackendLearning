using ProjectBackendLearning.Core.DTOs;
using ProjectBackendLearning.Core.Models.Requests;

namespace ProjectBackendLearning.DataLayer.Repositories;

public class DevicesRepository : BaseRepository, IDevicesRepository
{
    public DevicesRepository(BackMinerContext context) : base(context)
    {
    }

    public void AddDeviceToUser( DeviceDto device)
    {
        _ctx.Devices.Add(device);
        _ctx.SaveChanges();
    }

    public void SetDeviceStatus(DeviceDto device)
    {
        _ctx.Devices.Update(device);
        _ctx.SaveChanges();
    }
    public DeviceDto GetDeviceById(Guid id) => _ctx.Devices.FirstOrDefault(d => d.Id == id);
    public DeviceDto GetDeviceByUserId(Guid userId) => _ctx.Devices.FirstOrDefault(d => d.Owner.Id == userId);
}