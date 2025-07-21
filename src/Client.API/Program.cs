using Client.Application.Commands;
using Client.Application.Handlers;
using Client.Domain.Interfaces;
using Client.Infrastructure.Extensions;
using Client.Infrastructure.Persistence;
using Client.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
var connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDbContext<ClientDbContext>(options =>
    options.UseSqlServer(connectionString));

// Reposit�rios
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// MediatR
builder.Services.AddMediatR(typeof(CreateClientCommandHandler).Assembly);

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

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.Run();
