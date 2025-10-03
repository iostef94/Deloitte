using DeloitteIntegration.Infrastructure.DependencyInjection;
using DeloitteIntegration.Application.Interfaces;
using DeloitteIntegration.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Application layer
builder.Services.AddScoped<ICityService, CityService>();

// Add Infrastructure layer
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
