using Microsoft.EntityFrameworkCore;
using ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.DataLayer;

public class BackMinerContext : DbContext
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<DeviceDto> Devices { get; set; }

    public BackMinerContext(DbContextOptions<BackMinerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeviceDto>()
            .HasOne(d => d.Owner)
            .WithMany(u => u.Devices);
    }
}