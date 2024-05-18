using Microsoft.EntityFrameworkCore;
using ProjectBackendLearning.Core.DTOs;

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
    public DeviceDto GetDeviceById(Guid id) => _ctx.Devices.Include(u=>u.Owner).FirstOrDefault(d => d.Id == id);
    public List<DeviceDto> GetDevicesByUserId(Guid userId) => _ctx.Devices.Where(d => d.Owner.Id == userId).ToList();

    public List<DeviceDto> GetDevicesWhereStatusIsNotNull()
    {
        var devices= _ctx.Devices.Where(d=>d.Status != null).ToList();
        return devices;
    }

}