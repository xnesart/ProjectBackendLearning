using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using ProjectBackendLearning.Bll;
using ProjectBackendLearning.Core.Exceptions;
using ProjectBackendLearning.DataLayer;
using ProjectBackendLearning.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureApiServices();
builder.Services.ConfigureBllServices();
builder.Services.ConfigureDataBase(builder.Configuration);
builder.Services.ConfigureDalServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (exceptionHandlerFeature != null)
        {
            var exception = exceptionHandlerFeature.Error;

            if (exception is NotFoundException notFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsJsonAsync(new { error = notFoundException.Message });
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = "Внутренняя ошибка сервера" });
            }
        }
    });
});

app.Run();