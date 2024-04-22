using Microsoft.Extensions.DependencyInjection;
using ProjectBackendLearning.Bll.Services;

namespace ProjectBackendLearning.Bll;

public static class ConfigureServices
{
    public static void ConfigureBllServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IDevicesService, DevicesService>();
    }
}