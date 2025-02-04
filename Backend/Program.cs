using Backend.Services;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Backend.Repositories;
using Backend.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection(nameof(AppSettings)));

builder.Services.AddControllers();

// Repositories
builder.Services.AddScoped<IBackendRepository, BackendRepository>();

// Services
builder.Services.AddTransient<IBackendService, BackendService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCaching();

// builder.Services.AddMemoryCache();

// builder.Services.AddAuthentication().AddJwtBearer();
// builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

// app.UseAuthentication();

// app.UseAuthorization();


app.MapControllers();

app.Run();


// TODO: Exception handling middleware (global exception handler?)

// TODO: If frontend on different domains, add CORS

// TODO: Add OpenAPI docs

// TODO: Authentication and Authorization

// TODO: Add server side caching to endpoint