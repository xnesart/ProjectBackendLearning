
using ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.DataLayer.Repositories;

public class DevicesRepository : BaseRepository, IDevicesRepository
{
    public DevicesRepository(MamkinMinerContext context) : base(context)
    {
    }

    public DeviceDto GetDeviceById(Guid id) => _ctx.Devices.FirstOrDefault(d => d.Id == id);
    public DeviceDto GetDeviceByUserId(Guid userId) => _ctx.Devices.FirstOrDefault(d => d.Owner.Id == userId);
}