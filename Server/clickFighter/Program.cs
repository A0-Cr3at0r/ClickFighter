using clickFighter.Infrastructure;
using clickFighter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the database context to the service container
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "The DefaultConnection connection string cannot be found.");

builder.Services.AddInfrastructure(connectionString);

var app = builder.Build();

// Add a health check endpoint for the database connection
app.MapGet("/api/health/database", async (AppDbContext dbContext) =>
{
    var canConnect = await dbContext.Database.CanConnectAsync();

    return canConnect
        ? Results.Ok(new { status = "Connected" })
        : Results.Problem("Impossible de se connecter à la base de données.");
});

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
