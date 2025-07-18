using Client.Application.Commands;
using Client.Application.Handlers;
using Client.Domain.Interfaces;
using Client.Infrastructure.Persistence;
using Client.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Forçar escuta na porta 80
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

// Add services to the container
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ClientDbContext>(options =>
    options.UseSqlServer(connectionString));

// MediatR
builder.Services.AddMediatR(typeof(CreateClientCommandHandler).Assembly);

// Repositórios
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware HTTP
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Client.API v1");
    c.RoutePrefix = "swagger"; // acesso em /swagger
});


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
