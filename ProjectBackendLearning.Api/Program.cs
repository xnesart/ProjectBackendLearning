using ProjectBackendLearning.Bll;
using ProjectBackendLearning.Bll.Services;
using ProjectBackendLearning.DataLayer;
using ProjectBackendLearning.DataLayer.Repositories;
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();