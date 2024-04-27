using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProjectBackendLearning.Bll.Services;
using ProjectBackendLearning.Core.Models.Requests;
using ProjectBackendLearning.Core.Validation;

namespace ProjectBackendLearning.Bll;

public static class ConfigureServices
{
    public static void ConfigureBllServices(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateUserRequest>, UserRequestValidator>();
        services.AddScoped<IValidator<UpdateUserRequest>, UserUpdateValidator>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IDevicesService, DevicesService>();
    }
}