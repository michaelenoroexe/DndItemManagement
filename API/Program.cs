using API;
using API.ActionFilters;
using API.Extensions;
using API.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Exceptions;
using Service.Contracts;

Log.Logger = new LoggerConfiguration()
    .Enrich.WithExceptionDetails()
    .Enrich.WithProperty("Source", "API")
    .WriteTo.Http("http://logstash:8080", null)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Logging.AddSerilog(Log.Logger);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSingleton<IHasher, Hasher>();
builder.Services.AddScoped<ValidationFilterAttribute>();

builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

builder.Services.ConfigureSqlContext();

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSignalRSwaggerGen();
});

var app = builder.Build();

app.ConfigureExceptionHandler();

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

app.MapHub<ItemHub>("api/hubs/itemHub");
app.MapHub<RoomHub>("api/hubs/roomHub");

app.Run();
