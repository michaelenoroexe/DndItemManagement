using DataAccess.Interfaces;
using FileAccessor;
using WebApp.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvcCore();

builder.Services.AddSignalR();

builder.Services.AddSingleton<IDataAccessor>(AccessorFactory.GetAccessor());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapHub<ItemManagerHub>("/hub");

app.UseRouting();

app.MapControllers();

app.Run();
