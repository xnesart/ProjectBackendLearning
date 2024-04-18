using Microsoft.EntityFrameworkCore;
using ProjectBackendLearning.Bll.Services;
using ProjectBackendLearning.DataLayer;
using ProjectBackendLearning.DataLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddDbContext<MamkinMinerContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("MypConnection"))
        .UseSnakeCaseNamingConvention());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
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
