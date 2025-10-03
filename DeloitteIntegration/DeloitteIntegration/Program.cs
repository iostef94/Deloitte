using DeloitteIntegration.Infrastructure.DependencyInjection;
using DeloitteIntegration.Application.Interfaces;
using DeloitteIntegration.Application.Services;
using DeloitteIntegration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Application layer
builder.Services.AddScoped<ICityService, CityService>();

// Add Infrastructure layer (includes DbContext)
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// ✅ Automatically apply pending migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        // Apply migrations automatically
        dbContext.Database.Migrate();

        Console.WriteLine("✅ Database migration applied successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error applying migrations: {ex.Message}");
    }
}

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
