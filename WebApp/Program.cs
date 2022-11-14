var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvcCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

//app.UseMvc();
app.MapControllers();

app.Run();
