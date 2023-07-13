using Administration;
using Administration.Hubs;
using Administration.Service.Contracts;
using Administration.Extensions;
using Serilog;
using Serilog.Exceptions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Administration.Service;

Log.Logger = new LoggerConfiguration()
    .Enrich.WithExceptionDetails()
    .Enrich.WithProperty("Source", "Admin")
    .WriteTo.Http("http://logstash:8080", null)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog(Log.Logger);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IHasher, Hasher>();

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

builder.Services.ConfigureSqlContext();

builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.InstantiateDB();

app.ConfigureExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<RoomHub>("api/hub");

app.Run();
