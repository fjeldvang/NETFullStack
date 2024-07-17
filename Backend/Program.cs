using Backend.Services;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Repository
builder.Services.AddTransient<IBackendRepository, BackendRepository>();

// Service
builder.Services.AddTransient<IBackendService, BackendService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCaching();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

// app.UseAuthorization();

app.MapControllers();

app.Run();
