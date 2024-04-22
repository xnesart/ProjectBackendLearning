using Microsoft.Extensions.DependencyInjection;
using ProjectBackendLearning.DataLayer.Repositories;

namespace ProjectBackendLearning.DataLayer;

public static class ConfigureServices
{
    public static void ConfigureDalServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IDevicesRepository, DevicesRepository>();
    }
}