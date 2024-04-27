using ProjectBackendLearning.Bll;
using ProjectBackendLearning.Configuration;
using ProjectBackendLearning.DataLayer;
using ProjectBackendLearning.Extensions;
using ProjectBackendLearning.Core.Models.Requests;
using Serilog;
using ProjectBackendLearning.Core.Models.Responses;


try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();
// Add services to the container.

    // builder.Services.AddFluentValidationAutoValidation()
    //     .AddFluentValidationClientsideAdapters()
    //     .AddValidatorsFromAssemblyContaining<UserRequestValidator>();
    builder.Services.ConfigureApiServices(builder.Configuration);
    builder.Services.ConfigureBllServices();
    builder.Services.ConfigureDalServices();
    builder.Services.AddAutoMapper(typeof(RequestMapperProfile), typeof(ResponseMapperProfile));

    builder.Host.UseSerilog();
    var app = builder.Build();

    app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
    if (!app.Environment.IsProduction())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    Log.Information("Running app");
    app.Run();
}

catch (Exception ex)
{
    Log.Fatal(ex.Message);
}
finally
{
    Log.Information("App stopped");
    Log.CloseAndFlush();
}