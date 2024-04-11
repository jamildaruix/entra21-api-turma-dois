using Microsoft.EntityFrameworkCore;
using minha_primeira_api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Visual Studio
//Add-Migration InitialCreate 
//Update-Database

// Comando caso precisei instalar `dotnet tool install --global dotnet-ef`
//dotnet ef migrations add InitialCreate 
//dotnet ef database update

var connectionString = "Server=localhost;Database=Todo;Trusted_Connection=True;TrustServerCertificate=True;";

builder.Services.AddDbContext<TodoContextDB>(options => 
{
    options.UseSqlServer(connectionString);
});

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
