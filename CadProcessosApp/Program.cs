using CadProcessosApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("ProcessoConnDb");

builder.Services.AddDbContextFactory<ProcessoDbContext>(options =>
    options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "paginacao",
    pattern: "{controller=Processo}/p/{paginaAtual:int}",
    defaults: new { action = "Index" });
app.MapControllerRoute(name: "Default",
    pattern: "{controller=Processo}/{action=Index}/{id?}");

app.Run();
