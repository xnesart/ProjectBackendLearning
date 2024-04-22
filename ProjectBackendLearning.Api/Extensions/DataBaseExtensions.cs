using Microsoft.EntityFrameworkCore;
using ProjectBackendLearning.DataLayer;

namespace ProjectBackendLearning.Extensions;

public static class DataBaseExtensions
{
    public static void ConfigureDataBase(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddDbContext<MamkinMinerContext>(
            options => options
                .UseNpgsql(configurationManager.GetConnectionString("MypConnection"))
                .UseSnakeCaseNamingConvention());
    }
}