using Microsoft.EntityFrameworkCore;
using ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.DataLayer;

public interface IBackMinerContext
{
    DbSet<UserDto> Users { get; set; }
    DbSet<DeviceDto> Devices { get; set; }
}