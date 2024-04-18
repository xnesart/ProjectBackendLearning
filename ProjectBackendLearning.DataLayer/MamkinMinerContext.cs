using ClassLibrary1ProjectBackendLearning.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ProjectBackendLearning.DataLayer;

public class MamkinMinerContext : DbContext
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<DeviceDto> Devices { get; set; }

    public MamkinMinerContext(DbContextOptions<MamkinMinerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeviceDto>()
            .HasOne(d => d.Owner)
            .WithMany(u => u.Devices);
    }
}