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

// ===== APLICA AS MIGRATIONS COM RETRY =====
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClientDbContext>();

    var maxRetries = 10;
    var delay = TimeSpan.FromSeconds(5);

    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            db.Database.Migrate();
            Console.WriteLine("Migrations aplicadas com sucesso.");
            break; // sucesso
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Tentativa {i + 1} de aplicar migrations falhou: {ex.Message}");

            if (i == maxRetries - 1)
                throw; // lançar erro após número máximo de tentativas

            Thread.Sleep(delay);
        }
    }
}
// ===========================================


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
