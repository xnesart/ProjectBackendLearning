using ProjectBackendLearning.Bll;
using ProjectBackendLearning.Configuration;
using ProjectBackendLearning.DataLayer;
using ProjectBackendLearning.Extensions;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();
// Add services to the container.

    builder.Services.ConfigureApiServices(builder.Configuration);
    builder.Services.ConfigureBllServices();
    builder.Services.ConfigureDalServices();

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
