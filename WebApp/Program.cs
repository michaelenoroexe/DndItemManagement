using DataAccess.Interfaces;
using FileAccessor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvcCore();

builder.Services.AddSingleton<IDataAccessor>(AccessorFactory.GetAccessor());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

app.Run();
