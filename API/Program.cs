using API.Extensions;
using API.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureSqlContext();

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers()
    .AddApplicationPart(typeof(API.Presentation.AssemblyReference).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(opt =>
    {
        opt.RouteTemplate = "api/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("v1/swagger.json", "v1");
        opt.RoutePrefix = "api";
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ItemHub>("api/itemHub");

app.Run();
