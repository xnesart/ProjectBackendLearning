using Microsoft.EntityFrameworkCore;
using ProjectBackendLearning.Core.DTOs;

namespace ProjectBackendLearning.DataLayer;

public class BackMinerContext : DbContext
{
    public virtual DbSet<UserDto> Users { get; set; }
    public virtual DbSet<DeviceDto> Devices { get; set; }

    public BackMinerContext(DbContextOptions<BackMinerContext> options) : base(options)
    {
    }

    public BackMinerContext()
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeviceDto>()
            .HasOne(d => d.Owner)
            .WithMany(u => u.Devices);
    }
}